using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Recaptcha;

namespace PublicTheater.Web.Services
{
    /// <summary>
    /// Summary description for RecaptchaClient
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class RecaptchaClient : System.Web.Services.WebService
    {

        [WebMethod(EnableSession=true)]
        public bool ValidateCaptcha(string challengeValue, string responseValue)
        {
            
            Recaptcha.RecaptchaValidator captchaValidator = new Recaptcha.RecaptchaValidator
            {
                PrivateKey = "6Lff_fASAAAAANrgjcUPZoafxHwI-P0B2QDkSDOK",
                RemoteIP = HttpContext.Current.Request.UserHostAddress,
                Challenge = challengeValue,
                Response = responseValue
            };

            Recaptcha.RecaptchaResponse recaptchaResponse = captchaValidator.Validate(); // Send data about captcha validation to reCAPTCHA site.
            bool valid = recaptchaResponse.IsValid;
         
            /* This is what the server will actually check on the registration submission 
             * Possible bug if Session dies? 
             */   
            Session.Add("reCaptchaResponse", challengeValue);
            Session.Add("reCaptchaValid", valid);
            return valid;
        }
    }
}
