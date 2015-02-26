using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using EPiServer;
using EPiServer.Core;
using EPiServer.XForms;
using EPiServer.XForms.WebControls;

namespace PublicTheater.Web
{
    public class global : EPiServer.Global
    {
        /// <summary>
        /// Handles application level errors for TheaterTemplate Web
        /// </summary>
        protected virtual void Application_Error(object sender, EventArgs e)
        {
            Adage.Tessitura.Common.ErrorHandlers.Get().GlobalErrorHandler();
        }

        protected virtual void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            Adage.Tessitura.Common.ErrorHandlers.Get().Global_App_PreRequestHandlerExecute_CheckForMaintenance();
        }

        protected void Application_Start(Object sender, EventArgs e)
        {
            XFormControl.ControlSetup += new EventHandler(XForm_ControlSetup);
        }

        public void XForm_ControlSetup(object sender, EventArgs e)
        {
            XFormControl control = (XFormControl)sender;

            control.ControlsCreated += new EventHandler(XForm_ControlsCreated);
            //FormControl.FormDefinition = Form;
            control.BeforeSubmitPostedData += new SaveFormDataEventHandler(FormControl_BeforeSubmitPostedData);
            control.AfterSubmitPostedData += new SaveFormDataEventHandler(FormControl_AfterSubmitPostedData);
        }

        public void XForm_ControlsCreated(object sender, EventArgs e)
        {
            XFormControl formControl = (XFormControl)sender;

            // If thismethod does exist, Add this statement
            this.CleanupXFormHtmlMarkup(formControl);
        }

        private bool _isEmailActivated = false;
        public void FormControl_BeforeSubmitPostedData(object sender, SaveFormDataEventArgs e)
        {

            if (!string.IsNullOrEmpty(e.FormData.GetValue("SendTo")))
            {
                e.FormData.MailTo = e.FormData.GetValue("SendTo");
            }
            //Replace standard email handling with custom method if email action is selected for form.
            if ((e.FormData.ChannelOptions & ChannelOptions.Email) == ChannelOptions.Email)
            {
                _isEmailActivated = true;
                //We still might want to save the form to the database to just remove the email option
                e.FormData.ChannelOptions &= ~ChannelOptions.Email;
            }
        }
        public void FormControl_AfterSubmitPostedData(object sender, SaveFormDataEventArgs e)
        {

            XFormControl control = (XFormControl)sender;

            if (_isEmailActivated)
            {

                try
                {
                    SendFormattedEmail(control, e);
                }
                catch (Exception)
                {
                    
                }
            }

            if (control.FormDefinition.PageAfterPost > 0)
            {
                var pageData = EPiServer.DataFactory.Instance.GetPage(new PageReference(control.FormDefinition.PageAfterPost));
                if (pageData != null)
                {
                    HttpContext.Current.Response.Redirect(pageData.LinkURL);
                }

            }





        }

        private void SendFormattedEmail(XFormControl formControl, SaveFormDataEventArgs e)
        {
            string to = null;
            to = e.FormData.MailTo;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><title>Form post</title></head><body><table>");
            StringWriter sw = new StringWriter(sb);
            List<Attachment> attachments = new List<Attachment>();
            if (formControl.Page.Request.Files != null)
            {
                //foreach (String request in formControl.Page.Request.Files)
                //{
                //    Attachment newAttachment = new Attachment(request);
                //    attachments.Add(newAttachment);
                //}
            }
            bool cellGuard = false;

            foreach (System.Xml.XmlNode node in formControl.Data.Data.FirstChild.ChildNodes)
            {
                sb.AppendFormat("<tr><td><b>{0}</b></td><td>{1}</td></tr>", node.Name, node.InnerText);
            }

            if (cellGuard)
            {
                var key = Request.Form.AllKeys.FirstOrDefault(k => k.EndsWith("ChannelEmail"));
                if (key != null && !string.IsNullOrWhiteSpace(Request.Form[key]))
                    to = string.Concat(to, ", ", Request.Form[key]);
            }

            sb.Append("</table></form></body></html>");
            string html = sb.ToString();
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.Body = html;
            foreach (Attachment attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }
            message.From = new MailAddress(formControl.Data.MailFrom);
            message.Subject = formControl.Data.MailSubject;
            message.To.Add(to);
            SmtpClient client = new SmtpClient();
            client.Send(message);
        }

        private void CleanupXFormHtmlMarkup(XFormControl formControl)
        {
            if (formControl.EditMode)
            {
                return;
            }

            bool firstLiteralControl = false;
            LiteralControl literalControl = null;

            ControlCollection controls = formControl.Controls;

            foreach (object control in controls)
            {
                if (control is LiteralControl)
                {
                    literalControl = control as LiteralControl;

                    if (!literalControl.Text.Contains("FileUpload"))
                    {
                        if (!firstLiteralControl)
                        {
                            literalControl.Text = "<div class='xform row-fluid'><div>";
                            firstLiteralControl = true;
                        }
                        else
                        {
                            literalControl.Text = "</div><div>";
                        }
                    }
                }
            }

            if (literalControl != null)
            {
                literalControl.Text = "</div></div>";
            }
        }
    }
}