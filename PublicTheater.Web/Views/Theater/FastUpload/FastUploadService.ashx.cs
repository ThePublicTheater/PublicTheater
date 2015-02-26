using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.IO;
using EPiServer.Security;
using EPiServer.Web.Hosting;

namespace MakingWaves.FastUpload.FastUpload
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://com.makingwaves.fastupload/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class FastUploadService : IHttpHandler
    {
        public virtual void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            //Check that the user has editor access
            if (HasAccess())
            {
                var fName = request["fileName"];
                var toDirectory = request["toDirectory"];
                string _fileName = GetCachedFilename(context, fName);

                // Catch html5 fileName post
                if (!String.IsNullOrEmpty(fName))
                {
                    _fileName = fName;
                    return;
                }

                uploadFile(context, toDirectory, _fileName);
            }
            else
            {
                throw new UploadFailedException("Access to upload dialog denied. User not logged in as a EPiServer editor or admin.");
            }
        }

        protected virtual string GetCachedFilename(HttpContext context, string newFilename)
        {
            string cacheKey = "FastUpload_" + EPiServer.Security.PrincipalInfo.Current.Name;
            if (!string.IsNullOrEmpty(newFilename))
            {
                context.Cache[cacheKey] = newFilename;
                return string.Empty;
            }
            else
            {
                return (context.Cache[cacheKey] ?? string.Empty).ToString();
            }
        }

        protected virtual bool HasAccess()
        {
            return EPiServer.Security.PrincipalInfo.HasEditorAccess || EPiServer.Security.PrincipalInfo.HasAdminAccess;
        }

        protected virtual void uploadFile(HttpContext context, string toDirectory, string fName)
        {
            var request = context.Request;
            var response = context.Response;

            if (!toDirectory.EndsWith("/"))
                toDirectory += "/";

            UnifiedDirectory uploadDir = VerifyDirectory(context, toDirectory);
            var postedFile = context.Request.Files[0];
            var fileName = postedFile.FileName.Equals("blob") ? fName : postedFile.FileName;

            UnifiedFile tempFile = GetTempFile(uploadDir, fileName);
            string retMsg = UploadFileContents(context, toDirectory, tempFile, fileName);

            response.ContentType = "text/plain";
            response.Write(retMsg);
        }

        protected virtual string UploadFileContents(HttpContext context, string toDirectory, UnifiedFile tempFile, string fileName)
        {
            string retMsg = "Failed";
            string finalfilepath = toDirectory + fileName;

            HttpPostedFile postedFile = context.Request.Files[0];
            HttpRequest request = context.Request;
            FileMode fileMode = FileMode.Append;

            using (Stream fs = tempFile.Open(fileMode))
            {
                var buffer = new byte[postedFile.InputStream.Length];
                postedFile.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
                retMsg = "Success";
            }

            var chunk = request["chunk"] != null ? int.Parse(request["chunk"]) : 0;
            var chunks = !String.IsNullOrEmpty(request["chunks"]) ? int.Parse(request["chunks"]) : 1;

            // Done uploading last chunk
            if (chunk == chunks - 1)
            {
                UploadFinalFileChunk(toDirectory, tempFile, finalfilepath);
            }

            return retMsg;
        }

        protected virtual void UploadFinalFileChunk(string toDirectory, UnifiedFile tempFile, string finalfilepath)
        {
            IVersioningFile versioningFile;
            // If file is checked out. Check it in
            if (tempFile.TryAsVersioningFile(out versioningFile))
            {
                if (versioningFile.IsCheckedOut)
                    versioningFile.CheckIn("FastUpload");
            }

            if (HostingEnvironment.VirtualPathProvider.FileExists(finalfilepath) == false)
            {
                // create new output file
                //string currentDir = Path.GetDirectoryName(finalfilepath);
                string currentDir = VirtualPathUtility.GetDirectory(finalfilepath);
                UnifiedDirectory currentDirectory = HostingEnvironment.VirtualPathProvider.GetDirectory(currentDir) as UnifiedDirectory;
                currentDirectory.CreateFile(finalfilepath);
            }
            else
            {
                // versioned file handled later.  Here we need to re-name a file that cannot be versioned
                UnifiedFile existingFile = HostingEnvironment.VirtualPathProvider.GetFile(finalfilepath) as UnifiedFile;
                if (!(existingFile is IVersioningFile))
                {
                    // Handle duplicate filenames
                    string fileName = Path.GetFileNameWithoutExtension(finalfilepath) + "-" + existingFile.Changed.ToString("yyyyMMddHHmmss") +
                               Path.GetExtension(finalfilepath);

                    existingFile.MoveTo(toDirectory + fileName);
                }
            }

            var currentFile = HostingEnvironment.VirtualPathProvider.GetFile(finalfilepath) as UnifiedFile;
            using (Stream finalOutput = tempFile.Open())
            {
                WriteToFile(finalOutput, currentFile, "FinalUpload");
            }

            //Empty temp folder
            foreach (UnifiedFile file in tempFile.Parent.GetFiles())
            {
                try { file.Delete(); }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }

        /// <summary>
        /// Saves the source stream to a unified file and adds a comment if the file implrments IVersioningFile.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="targetFile">The target file.</param>
        /// <param name="comment">A comment to add when checking in a versioning file.</param>
        public static void WriteToFile(Stream sourceStream, UnifiedFile targetFile, string comment)
        {
            // It may be a versioning file
            IVersioningFile versioningFile = targetFile as IVersioningFile;
            if (versioningFile != null)
            {
                if (!versioningFile.IsCheckedOut)
                {
                    versioningFile.CheckOut();
                }
            }

            // Copy the source stream to the target file stream.
            using (Stream writeStream = targetFile.Open(FileMode.Create, FileAccess.Write))
            {
                EPiServer.BaseLibrary.IO.StreamConsumer.CopyToEnd(sourceStream, writeStream);
            }

            // If versioning, then check in with the supplied comment.
            if (versioningFile != null)
            {
                versioningFile.CheckIn(comment ?? String.Empty);
            }
        }

        protected virtual UnifiedFile GetTempFile(UnifiedDirectory uploadDir, string fileName)
        {
            var tempfilepath = "/" + uploadDir.VirtualPath + fileName;

            UnifiedFile tempFile = null;
            if (HostingEnvironment.VirtualPathProvider.FileExists(tempfilepath))
                tempFile = HostingEnvironment.VirtualPathProvider.GetFile(tempfilepath) as UnifiedFile;

            if (tempFile == null)
                tempFile = uploadDir.CreateFile(fileName);


            // Checkout file
            IVersioningFile versioningFile;
            if (tempFile.TryAsVersioningFile(out versioningFile))
            {
                if (!versioningFile.IsCheckedOut)
                    versioningFile.CheckOut();
            }

            return tempFile;
        }

        protected virtual UnifiedDirectory VerifyDirectory(HttpContext context, string toDirectory)
        {
            //Check if the to directory exists 
            UnifiedDirectory toDir = HostingEnvironment.VirtualPathProvider.GetDirectory(toDirectory) as UnifiedDirectory;
            if (toDir == null)
                throw new UploadFailedException("Upload failed. To directory argument not a UnifiedDirectory");

            //Check if user has write access to the toDirectory
            if (!toDir.QueryDistinctAccess(AccessLevel.Create))
                throw new UploadFailedException("Upload failed. User does not have create access to destination VPP directory.");

            //Get temp folder
            UnifiedDirectory uploadDir = GetTempUploadDirectory();

            //Check if use has write access to temp folder
            if (!uploadDir.QueryDistinctAccess(AccessLevel.Create))
                throw new UploadFailedException("Upload failed. User does not have create access to temp VPP directory.");
            return uploadDir;
        }




        protected virtual UnifiedDirectory GetTempUploadDirectory()
        {
            const string GlobalDirName = "/Global/";
            const string TempDirName = "temp";
            const string UploadDirName = "FastUpload";

            UnifiedDirectory GlobalDir = HostingEnvironment.VirtualPathProvider.GetDirectory(GlobalDirName) as EPiServer.Web.Hosting.UnifiedDirectory;

            if (GlobalDir == null)
                return null;
            GlobalDir.BypassAccessCheck = true;

            if (!HostingEnvironment.VirtualPathProvider.DirectoryExists(GlobalDirName + TempDirName + "/"))
            {
                GlobalDir.CreateSubdirectory(TempDirName);
            }
            UnifiedDirectory TempDir = HostingEnvironment.VirtualPathProvider.GetDirectory(GlobalDirName + "/" + TempDirName + "/") as EPiServer.Web.Hosting.UnifiedDirectory;
            TempDir.BypassAccessCheck = true;

            if (!HostingEnvironment.VirtualPathProvider.DirectoryExists(GlobalDirName + "/" + TempDirName + "/" + UploadDirName + "/"))
            {
                TempDir.CreateSubdirectory(UploadDirName);
            }
            UnifiedDirectory UploadDir = HostingEnvironment.VirtualPathProvider.GetDirectory(GlobalDirName + "/" + TempDirName + "/" + UploadDirName) as EPiServer.Web.Hosting.UnifiedDirectory;
            UploadDir.BypassAccessCheck = true;

            return UploadDir;
        }

        [Serializable]
        public class UploadFailedException : System.Exception
        {

            public UploadFailedException()
            {
            }

            public UploadFailedException(string message)
                : base(message)
            {
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
