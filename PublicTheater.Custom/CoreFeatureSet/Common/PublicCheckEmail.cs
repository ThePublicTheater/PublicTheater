using Adage.Tessitura;
using System.Data;
using System.Collections.Generic;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicCheckEmail
    {
        /// <summary>
        /// This method takes an email or login name and searches for it in Tessitura
        /// It returns the "LoginType" as the key and the "CustomerNumber" as the value
        /// </summary>
        /// <param name="emailAddresss"></param>
        /// <returns></returns>
        public static List<KeyValuePair<int, int>> GetLoginInfo(string emailAddresss)
        {
            // provide a garabage login name so it doesn't skew the results for now
            string loginName = "!@#$^%^&*(_)!@#$^%^&*(_)";

            string sqlParameters = string.Format("@emailAddress={0}&@custLogin={1}", emailAddresss, loginName);
            int storedProcId = Config.GetValue("LWP_SEARCH_LOGIN", 18);
            DataSet currentResults = TessituraAPI.Get().ExecuteLocalProcedure(TessSession.GetSessionKey(), storedProcId, sqlParameters);

            var finalResults = new List<KeyValuePair<int, int>>();
            if (currentResults != null && currentResults.Tables.Count > 0)
            {
                foreach (DataRow eachRow in currentResults.Tables[0].Rows)
                {
                    finalResults.Add(new KeyValuePair<int, int>((int)eachRow[1], (int)eachRow[0]));
                }
            }

            return finalResults;
        }
    }
}
