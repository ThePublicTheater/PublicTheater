using System.Collections;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Adage.EpiServer.Library.PageTypes.Sources;
using Adage.Tessitura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura.CartObjects;
using Adage.Tessitura.Common;
using Adage.Tessitura.PerformanceObjects;
using Adage.Tessitura.UserObjects;
using com.tessiturasoftware.tessitura;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using Microsoft.Ajax.Utilities;
using PassKitAPIWrapper;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using PublicTheater.Custom.Episerver;
using PublicTheater.Custom.CoreFeatureSet.Helper;
using PublicTheater.Web.Services;
using TheaterTemplate.Shared.CartObjects;
using TheaterTemplate.Shared.Common;
using TheaterTemplate.Shared.EpiServerConfig;
using TheaterTemplate.Shared.EpiServerProperties;
using TheaterTemplate.Shared.GiftCertificateObjects;
using System.Text.RegularExpressions;

namespace PublicTheater.Web.Controls.Checkout
{
    public class PaymentControl : TheaterTemplate.Web.Controls.CheckoutControls.PaymentControl
    {
        protected global::System.Web.UI.WebControls.Literal litDonationDescription;
        protected global::System.Web.UI.WebControls.CheckBox cbNYMagazine;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2PublicTheater;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2JoesPub;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2Shakespeare;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2Utr;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2Forum;
        protected global::System.Web.UI.WebControls.CheckBox cbMail2Weekly;
        protected global::System.Web.UI.WebControls.Literal litEmailSignUpDescription;
        protected global::System.Web.UI.WebControls.DropDownList SourceDD;
        protected global::System.Web.UI.WebControls.RequiredFieldValidator ReqSource;
        protected HtmlGenericControl SourceSelectionDiv;
        protected CheckBox cbDigi;
        protected Panel digitalDelivery;
        protected HiddenField hfDigitalDelivery;
        private List<int> _currentMail2Subscriptions;
        /// <summary>
        /// mail2 subcription list current user already opted-in
        /// </summary>
        public virtual List<int> CurrentMail2Subscriptions
        {
            get
            {
                if (_currentMail2Subscriptions == null)
                {
                    CurrentUser.GetAccountInfo();
                    _currentMail2Subscriptions = Mail2Helper.GetSubscriptionsByEmail(CurrentUser.Email);
                }
                return _currentMail2Subscriptions;

            }
        }

        protected virtual PublicPackageListConfig CurrentPackageListConfig
        {
            get
            {
                return PublicPackageListConfig.GetPublicFlexPackageListConfig();
            }
        }

        /// <summary>
        /// check is current user subscribed the list
        /// </summary>
        /// <param name="siteTheme"></param>
        /// <returns></returns>
        public virtual bool AleadySubscribed(Enums.SiteTheme siteTheme)
        {
            return CurrentMail2Subscriptions.Contains(Mail2Helper.GetMail2ListIdByTheme(siteTheme));
        }

        /// <summary>
        /// mirror base check out logic and apply the partner tickets payment method here
        /// </summary>
        /// <param name="currentPayment"></param>
        /// <param name="paymentPlan"></param>
        /// <returns></returns>
        protected virtual bool BaseCheckoutCurrentCart(Adage.Tessitura.Payment currentPayment, Adage.Tessitura.Constants.PaymentSchedule paymentPlan)
        {
            try
            {


                //pay off the partner tickets use specicial payment method first

                if (paymentPlan == null)
                {
                    Adage.Tessitura.CartObjects.PaymentPlan.RemoveCurrentPlan();
                    CurrentCart.Checkout(currentPayment);
                }
                else
                {
                    CurrentCart.Checkout(currentPayment, paymentPlan);
                }


                SendConfirmationEmail();
                if (CurrentCart.CartLineItems.Any(item => item is GiftCertificateLineItem))
                {
                    SendGiftCertificateEmails();
                }
            }
            catch (TessituraException ex)
            {
                HandleTessituraCheckoutException(ex);
                return false;
            }
            catch (Exception ex)
            {
                HandleCheckoutGenericException(ex);
                return false;
            }

            return true;
        }

        protected override bool CheckoutCurrentCart(Adage.Tessitura.Payment currentPayment, Adage.Tessitura.Constants.PaymentSchedule paymentPlan)
        {
            if (OverLimit())
            {
                pnlCheckoutError.Visible = true;
                ltrError.Text =
                    "You are over the ticket limit for a production in your cart. Please edit your cart to be within the limit or contact the Taub Box Office at 212.967.7555 for assistance (10am-7pm daily).";
                return false;
            }
            if (IsMobile())
                UpdateChannel(Config.GetValue("MobileSalesChannel", 20));

            var checkoutSucceed = base.CheckoutCurrentCart(currentPayment, paymentPlan);

            if (checkoutSucceed)
            {
                CheckEmailPreferences();
                //send e-ticketEmail

                //if (cbDigi.Checked)
                //{
                //    PrintTicketsCriteria c = new PrintTicketsCriteria();
                //    int orderNumber = Adage.Tessitura.Cart.GetCart().OrderNumber;
                //    c.OrderId = orderNumber;
                //    Adage.Tessitura.UserObjects.PrintTicketsInfo.PrintTickets(c);
                //    TicketHistorySubLineItems ticketHistoryItems =
                //        TicketHistorySubLineItems.Get(new TicketHistorySubLineItemCriteria());
                //    var currentOrderItems =
                //        ticketHistoryItems.Where(p => p.OrderNumber == orderNumber).ToList();
                //    var pass = new PassKit("1jvsuro4rTRkw5739T3Pz",
                //        "qgfnedpf1VCh7xtEz8Jmku9QGOqgI1NwUSDNzb5tZ/PMjz9fNzL1y");
                //    var allFields = new List<Dictionary<string, string>>();
                //    var urls = new List<string>();
                //    foreach (var item in currentOrderItems)
                //    {
                //        var fields = new Dictionary<string, string>();
                //        fields["barcodeContent"] = Convert.ToString(item.TicketId);
                //        fields["theater"] = item.Venue;
                //        fields["title"] = item.PerformanceTitle;
                //        fields["showtime"] = item.PerformanceDate.ToLongDateString();
                //        fields["row"] = item.SeatRow;
                //        fields["seat"] = item.SeatNumber;
                //        fields["customername"] = Adage.Tessitura.User.GetUser().FullName;
                //        fields["price"] = item.AmountDue.ToString("C");
                //        fields["showname_label"] = item.PerformanceTitle;
                //        string transactionDetails = "";
                //        transactionDetails += "Customer Number: " + Convert.ToString(Adage.Tessitura.User.GetUser().CustomerNumber)+" \r\n";
                //        transactionDetails += "Order Number: " + Convert.ToString(item.OrderNumber) + " \r\n";
                //        transactionDetails += "Ticket Number: " + Convert.ToString(item.TicketId) + " \r\n";
                //        transactionDetails += "Customer Name: " + User.GetUser().FullName + " \r\n"; 
                //        transactionDetails += "Total Ticket Price: " + item.AmountDue.ToString("C");
                //        fields["transdetails"] = transactionDetails;
                //        var pdp = getPDP(item.ProductionSeasonId);
                //        fields["showname"] = pdp.Property["MainBody"].ToWebString();
                //        PassKitResponse response = pass.IssuePass("Test Template", fields);
                //        allFields.Add(fields);
                //        urls.Add(response.response["url"]);


                //    }

                //    string to = Adage.Tessitura.User.GetUser().Email;
                //    string from = "bbenns@publictheater.org";
                //    string subject = "your e-tickets";
                //    string body = "";
                //    int i = 1;
                //    foreach (var url in urls)
                //    {
                //        body += "Ticket " + i.ToString() + ": ";
                //        body += "<a href='" + url + "'>Click here to add to passbook</a>";
                //        body += "<br>";
                //        i++;

                //    }
                //    SendEmail(to, from, subject, body);

                //}

                //call SP for contribution transactions
                var publicCart = CurrentCart as Custom.CoreFeatureSet.CartObjects.PublicCart;
                if (publicCart != null)
                {
                    publicCart.UpdateMembershipContribution();
                }


            }

            return checkoutSucceed;
        }

        protected virtual void CheckEmailPreferences()
        {
            try
            {
                if (cbNYMagazine.Visible)
                {
                    var publicUser = CurrentUser as Custom.CoreFeatureSet.UserObjects.PublicUser;
                    if (publicUser != null)
                    {
                        publicUser.SubscribeNYMagazineOffer(cbNYMagazine.Checked);
                    }
                }


                ////if any mail2 subscription changed
                //if (cbMail2PublicTheater.Checked != AleadySubscribed(Enums.SiteTheme.Default))
                //{
                //    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Default, CurrentUser.Email, cbMail2PublicTheater.Checked);
                //}
                //if (cbMail2JoesPub.Checked != AleadySubscribed(Enums.SiteTheme.JoesPub))
                //{
                //    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.JoesPub, CurrentUser.Email, cbMail2JoesPub.Checked);
                //}
                //if (cbMail2Shakespeare.Checked != AleadySubscribed(Enums.SiteTheme.Shakespeare))
                //{
                //    Mail2Helper.SubscribeEmailList(Enums.SiteTheme.Shakespeare, CurrentUser.Email, cbMail2Shakespeare.Checked);
                //}
                EmailSubscribeService.Subscribe(cbMail2PublicTheater.Checked, cbMail2JoesPub.Checked,
                    cbMail2Shakespeare.Checked, cbMail2Forum.Checked, cbMail2Weekly.Checked, cbMail2Utr.Checked);
            }
            catch (Exception ex)
            {

                Adage.Common.ElmahCustomError.CustomError.LogError(ex, "CheckEmailPreferences on payment page failed", null);
            }

        }

        protected override void BindPage()
        {
            base.BindPage();



            if (string.IsNullOrWhiteSpace(PaymentPageContent.DonationAskDescriptionText) == false && litDonationDescription != null)
            {
                litDonationDescription.Text = PaymentPageContent.DonationAskDescriptionText;
            }

            var publicUser = CurrentUser as Custom.CoreFeatureSet.UserObjects.PublicUser;
            if (publicUser != null)
            {
                //display the opt-in checkbox if user is not a subscriber already
                cbNYMagazine.Checked = publicUser.NYMagazineOffer;
                cbNYMagazine.Visible = !publicUser.NYMagazineOffer;
            }

            //cbMail2PublicTheater.Checked = AleadySubscribed(Enums.SiteTheme.Default);
            //cbMail2JoesPub.Checked = AleadySubscribed(Enums.SiteTheme.JoesPub);
            //cbMail2Shakespeare.Checked = AleadySubscribed(Enums.SiteTheme.Shakespeare);

            var tessSession = Adage.Tessitura.TessSession.GetSession();
            var defaultPromo = Adage.Tessitura.Config.GetValue("DefaultPromoCode");
            if (tessSession.PromotionCode.ToString() == defaultPromo && (Session["source"] == null || Session["source"].ToString() == defaultPromo))
            {
                var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
                var contentReference = new ContentReference(PaymentPageContent.PageId);
                var page = repository.Get<PageData>(contentReference);
                var sourcesProperty = page["SourceItemsList"];


                if (sourcesProperty != null)
                {
                    var sourcesList = new KeyValueList();
                    sourcesList.LoadData(sourcesProperty);
                    List<KeyValuePair> sources =
                        sourcesList.GetTypedCollectionAll<KeyValuePair>();
                    if (sources.Count > 0)
                    {

                        sources.ForEach(pair => SourceDD.Items.Add(new ListItem((pair.Key), pair.ValueOfKey)));
                        SourceSelectionDiv.Visible = true;
                        SourceDD.Visible = true;
                        ReqSource.Enabled = true;
                    }
                }
            }

            if (!IsPostBack)
            {
                ApplyPackageVoucherInSession();
            }

        }

        /// <summary>
        /// override send gift certificate email to use different template for Gift Certificate versus Package Voucher
        /// </summary>
        protected override void SendGiftCertificateEmails()
        {
            try
            {
                if (Config.GetValue(TheaterSharedAppSettings.SEND_GIFT_CERT_EMAIL, true) == false)
                    return;

                var email = Repository.GetNew<PublicGiftCertificateEmailObject>();
                var epiEmailData = GetGiftCertificateEmailPage();
                var epiPackageVoucherEmailData = GetPackageVoucherEmailPage();
                foreach (var giftCertificate in CurrentCart.CartLineItems
                                               .Where(item => item is GiftCertificateLineItem)
                                               .Cast<GiftCertificateLineItem>())
                {
                    var giftCertificateData = GiftCertificateEmail.GetGiftCertificateEmail(giftCertificate.GiftCertificateNumber);

                    if (PublicPackageVoucher.IsPackageVoucher(giftCertificate))
                    {
                        //use package voucher email template if it is a voucher
                        email.SetMessageRecipient(CurrentUser.Email);
                        email.GiftCertificateRedemptionCode = giftCertificateData.RedemptionCode;
                        email.RedemptionUrl = PublicPackageVoucher.GetPackageVoucherRedemptionUrl(giftCertificateData.RedemptionCode);
                        email.Render(epiPackageVoucherEmailData, CurrentCart, CurrentUser);
                    }
                    else
                    {
                        //user regular gift certificate email template
                        var graphicGenerator = new GiftCertificateGraphicGenerator();
                        var certificateImage = graphicGenerator.CreateGiftCertificateGraphic(giftCertificateData);
                        email.GiftCertificateImage = certificateImage;
                        email.SetMessageRecipient(giftCertificateData.Email);
                        email.Render(epiEmailData, CurrentCart, CurrentUser);
                    }

                    email.SendMessage();
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Checkout - Send Gift Certificate Email - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        /// <summary>
        /// get package voucher email template page
        /// </summary>
        /// <returns></returns>
        protected virtual PageData GetPackageVoucherEmailPage()
        {
            var pageId = Config.GetValue<int>("PackageVoucherEmailPageID", 0);
            return DataFactory.Instance.GetPage(new PageReference(pageId));
        }

        /// <summary>
        /// Auto apply package voucher if this is a voucher redemption transaction for flex package
        /// </summary>
        protected virtual void ApplyPackageVoucherInSession()
        {
            if (PublicTessSession.IsInFlexPackagePath() == false)
            {
                //not in flex package path
                return;
            }

            //is in flex package path
            var packageVoucher = PublicPackageVoucher.LoadFromSession();
            if (packageVoucher == null)
            {
                //no voucher found in session
                return;
            }

            var packageConfig = CurrentPackageListConfig.GetPackageDetailConfigByPaymentMethodId(packageVoucher.PaymentMethodId);
            if (packageConfig == null)
            {
                //voucher in session not assigned to any package
                return;
            }

            var applicableItem = CurrentCart.CartLineItems.OfType<FlexPackageLineItem>()
                    .FirstOrDefault(item => item.PackageId == packageConfig.FlexPackageId);
            if (applicableItem == null)
            {
                //voucher applicable pacakge is not in cart
                return;
            }

            try
            {
                //apply package voucher if this is a package redemption transaction
                packageVoucher.ApplyGiftCertificate(100000);
                //disable other payment methods, not allow to use credit card or gift cert
                checkoutPaymentOptions_GiftCertificateApplied(null, null);
            }
            catch (Exception ex)
            {
                //package voucher is in use to cart already paid in full?
                Adage.Common.ElmahCustomError.CustomError.LogError(ex,
                    "ApplyPackageVoucherInSession on payment options failed", null);
            }
        }

        protected override void AddOrderNotes()
        {
            // only append the order notes if they don't already exist
            // public asked to use CSI instead of order note, as no one in BOX OFFICE look at order notes
            if (CurrentCart.OrderNotes.Contains(checkoutPaymentOptions.OrderNotes) == false)
            {
                var notes = string.IsNullOrEmpty(CurrentCart.OrderNotes)
                                ? string.Concat(CurrentCart.OrderNotes, " ", checkoutPaymentOptions.OrderNotes)
                                : checkoutPaymentOptions.OrderNotes;

                var csi = CSIConfig.GetCSIFromEpiServer(Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.OrderNotesCSIPageID, 0));
                if (csi != null)
                {
                    csi.Notes = string.Format("OrderNumber:{0}; OrderNotes:{1}", CurrentCart.OrderNumber, notes);
                    csi.AttachTo(Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.OrderNotesCSISessionKey, "OrderNotesCSISessionKey"));
                }
            }

            //Attached Partner Tickets CSIs
            if (MembershipHelper.IsMemberPartnerModeOfSale())
            {
                try
                {
                    foreach (var performanceLineItem in CurrentCart.CartLineItems.OfType<PerformanceLineItem>())
                    {
                        var hasParnterTicketDiscount = performanceLineItem.TicketBlocks.SelectMany(t => t).OfType<PublicTicket>().Any(t => t.HasPartnerDiscount);

                        if (hasParnterTicketDiscount)
                        {
                            //attach CSI if any partner tickets purchased
                            var csi = CSIConfig.GetCSIFromEpiServer(MembershipHelper.PartnerTicketCSI);
                            if (csi != null)
                            {
                                csi.Notes += Environment.NewLine + "Partner Tickets Purchased";
                                csi.AttachTo(performanceLineItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Add CSI for partner tickets perf line item - {0} - {1}", ex.Message, ex.StackTrace), null);
                }
            }
        }

        protected override void lbCheckout_Click(object sender, EventArgs e)
        {
            var tessSession = Adage.Tessitura.TessSession.GetSession();
            var defaultPromo = Adage.Tessitura.Config.GetValue("DefaultPromoCode");

            if (SourceDD.Visible)
            {
                string source = SourceDD.SelectedValue;
                if (source != "-1" && !source.IsNullOrWhiteSpace())
                {
                    Session["source"] = source;
                    try
                    {
                        TessSession.GetSession().UpdateSourceCode(source);
                    }
                    catch (Exception ex)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, "Invalid Source Code Selected, Change in cms: CFS->custom template pages->Cart->Payment->Sources", null);
                    }

                }
            }
            else if (tessSession.PromotionCode.ToString() == defaultPromo && (Session["source"] != null))
            {

                string source = (string)Session["source"];
                if (source != string.Empty)
                {
                    try
                    {
                        TessSession.GetSession().UpdateSourceCode(source);
                    }
                    catch (Exception ex)
                    {
                        Adage.Common.ElmahCustomError.CustomError.LogError(ex, "Invalid Source Code from query string.", null);
                    }
                }
            }

            base.lbCheckout_Click(sender, e);
        }

        protected override bool SetCartAndShippingInformation()
        {
            int shippingId = 0;
            bool primaryAddress = false;
            try
            {
                CurrentUser.GetAccountInfo();
                CurrentUser.GetAddresses();
                if (CurrentCart.ShippingAddressID > 0)
                {
                    shippingId = CurrentCart.ShippingAddressID;
                    primaryAddress = false;
                }
                else if (CurrentUser.PrimaryAddress.AddressId > 0)
                {
                    shippingId = CurrentUser.PrimaryAddress.AddressId;
                    primaryAddress = true;
                }

                CurrentCart.SetShippingMethod(checkoutPaymentOptions.ShippingMethod, shippingId);

                if (checkoutPaymentOptions.ShippingMethod == TheaterSharedCart.ShippingMethodPostal())
                {
                    var address = primaryAddress
                        ? CurrentUser.PrimaryAddress
                        : CurrentUser.UserAddresses.FirstOrDefault(ua => ua.AddressId == shippingId);
                    if (address != null && PublicUser.IsPlaceHolderAddress(address))
                    {
                        pnlCheckoutError.Visible = true;
                        ltrError.Text = Config.GetValue("DefaultAddressMessage", DefaultAddressMsgError);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                HandleCartShippingError(ex, checkoutPaymentOptions.ShippingMethod, shippingId, primaryAddress);
                return false;
            }
        }

        private const string DefaultAddressMsgError =
            "<p>You have indicated that you would like us to mail your tickets, but your account still contains a placeholder delivery address. Please use the Edit button to update your address before confirming your transaction.</p><p></p>";

        private bool SendEmail(string to, string from, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to);
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "pt-mail2.publictheater.local";
                mail.Subject = subject;
                mail.Body = body;
                client.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private PageData getPDP(int prodSeasonNumber)
        {

            var pdpPageCriteria = new PropertyCriteria()
            {
                Name = "PageTypeID",
                Condition = CompareCondition.Equal,
                Required = true,
                Type = PropertyDataType.PageType,
                Value = Config.GetValue<string>("PDP_PAGE_TYPE", string.Empty)
            };

            var cc = new PropertyCriteriaCollection();
            cc.Add(pdpPageCriteria);
            cc.Add(new PropertyCriteria()
            {
                Name = "TessituraId",
                Condition = CompareCondition.Equal,
                Required = true,
                Type = PropertyDataType.Number,
                Value = Convert.ToString(prodSeasonNumber)
            });
            var pdpPage = DataFactory.Instance.FindAllPagesWithCriteria(ContentReference.StartPage, cc, "en", new LanguageSelector("en")).FirstOrDefault();

            return pdpPage;

        }

        protected bool IsMobile()
        {
            string u = Request.ServerVariables["HTTP_USER_AGENT"];
            Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))));

        }

        protected bool UpdateChannel(int channel)
        {

            var cart = Adage.Tessitura.Cart.GetCart();

            try
            {
                TessituraAPI.Get().UpdateOrderDetails(
                       TessSession.GetSessionKey(),
                       cart.Solicitor,
                       0,
                       channel,
                       string.Empty
                       );
                return true;
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Update Channel - {0} - {1}",
                                                                                           ex.Message, ex.StackTrace), null);
                return false;
            }


        }

        protected bool OverLimit()
        {
            Dictionary<int,int> sumDictionary = null;
            foreach (var cli in CurrentCart.CartLineItems)
            {

                if (cli is PerformanceLineItem)
                {
                    var pli = (PerformanceLineItem)cli;
                    var production=Adage.Tessitura.Production.GetProduction(pli.Performance.ProductionSeasonId);
                    if ( !String.IsNullOrEmpty(production.WebContent["Production Season Ticket Limit"]))
                    {
                        if(sumDictionary==null)
                           sumDictionary= SumUpOverProdSeasDictionary(CurrentCart);
                        if (sumDictionary[pli.Performance.ProductionSeasonId]>Convert.ToInt32(production.WebContent["Production Season Ticket Limit"]))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected Dictionary<int, int> SumUpOverProdSeasDictionary(Adage.Tessitura.Cart cart)
        {
            Dictionary<int, int> returnValue = new Dictionary<int, int>();
            foreach (var cli in CurrentCart.CartLineItems)
            {
                if (cli is PerformanceLineItem)
                {
                    var pli = (PerformanceLineItem)cli;
                    if (returnValue.ContainsKey(pli.Performance.ProductionSeasonId))
                        returnValue[pli.Performance.ProductionSeasonId] += pli.Quantity;
                    else
                    {
                        returnValue.Add(pli.Performance.ProductionSeasonId,pli.Quantity);
                    }
                }
            }
            return returnValue;
        }
    }
}