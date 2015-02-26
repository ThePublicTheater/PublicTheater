using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using TheaterTemplate.Shared.Common;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicGiftCertificateEmailObject : TheaterSharedGiftCertificateEmailObject
    {
        /// <summary>
        /// Used to set the image that will be placed in the email template
        /// </summary>
        public virtual string GiftCertificateRedemptionCode { get; set; }

        /// <summary>
        /// Used to set the image that will be placed in the email template
        /// </summary>
        public virtual string RedemptionUrl { get; set; }

        protected override string GetCodeProperty(string name)
        {
            var lookupName = name.ToUpper();
            if (lookupName.Equals("REDEMPTIONCODE"))
            {
                return this.GiftCertificateRedemptionCode;
            }
            if (lookupName.Equals("REDEMPTIONURL"))
            {
                return this.RedemptionUrl;
            }
            return base.GetCodeProperty(name);

        }

        protected override void FillEmailBody()
        {
            base.FillEmailBody();

            var regexSpan = new Regex("<span(.*?)</span>");
            Body = HandlePlaceHolderTags(Body, regexSpan).Trim();

            regexSpan = new Regex("<a(.+?)href=[\\\"']#REDEMPTIONURL[\\\"'](.+?)>");
            var replacement = "<a $1 href=\"" + this.RedemptionUrl + "\" $2>";
            Body = regexSpan.Replace(Body, replacement);
        }

        protected override void AttachImage(MailMessage message)
        {
            //if gift certificate image not generated, don't attached it to email
            if (GiftCertificateImage != null)
            {
                base.AttachImage(message);    
            }
            
        }
    }
}
