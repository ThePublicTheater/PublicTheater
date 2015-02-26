using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;
using Adage.Theater.RelationshipManager.Helpers;
using PublicTheater.Custom.CoreFeatureSet.Common;
using PublicTheater.Custom.CoreFeatureSet.EpiServerConfig;
using PublicTheater.Custom.CoreFeatureSet.Memberships;
using PublicTheater.Custom.CoreFeatureSet.UserObjects;
using PublicTheater.Custom.CoreFeatureSet.CartObjects;
using TheaterTemplate.Shared.CartObjects;

namespace PublicTheater.Web.Controls.Checkout
{
    public class PaymentOptions : TheaterTemplate.Web.Controls.CheckoutControls.PaymentOptions
    {
        protected virtual PublicCart CurrentPublicCart { get { return CurrentCart as PublicCart; } }
        protected virtual PublicUser CurrentPublicUser { get { return CurrentUser as PublicUser; } }
        protected bool IsTicketInCart = false;
        protected bool AllowPah = true;



        protected override void BindPage()
        {
            base.BindPage();
            string amexString = Adage.Tessitura.Config.GetValue("AmexOnly:ProdSeas-PriceTypes");
            if (!String.IsNullOrEmpty(amexString))
            {
                try
                {
                    int prodSeas = Int32.Parse(amexString.Split('-')[0]);

                    string priceTypes = amexString.Split('-')[1];

                    List<string> priceTypeList = priceTypes.Split(';').ToList();
                    LimitPaymentMethod("43", prodSeas, priceTypeList);

                }
                catch (Exception ex)
                {

                    Adage.Common.ElmahCustomError.CustomError.LogError(ex, "Error Parsing Amex Only Config", null);
                }
              
            }
            if (!IsTicketInCart)
                phDeliveryMethod.Visible = false;
            var currentPublicUser = CurrentPublicUser;
            BillingAddressDisplay.Fill(CurrentUser, currentPublicUser.BillingAddress);
            ShippingAddressDisplay.Fill(CurrentUser, currentPublicUser.ShippingAddress);

        }

        /// <summary>
        /// Override shipping methods available to current user
        /// </summary>
        /// <returns></returns>
        protected override Adage.Tessitura.ShippingMethods GetShippingMethods()
        {
            var userShippingMethods = base.GetShippingMethods();

            if (userShippingMethods.Count > 1)
            {
                //only remove mail shipping if there are more than one options available
                var mailOption =
                    userShippingMethods.FirstOrDefault(s => s.ID == TheaterSharedCart.ShippingMethodPostal());
                if (mailOption != null)
                {
                    if (hideMailDeliveryOption())
                    {
                        userShippingMethods.Remove(mailOption);
                    }
                }

                //remove print at home if it doesnt apply 
                var pahOption =
                    userShippingMethods.FirstOrDefault(s => s.ID == TheaterSharedCart.ShippingMethodPrintAtHome());
                if(CurrentPublicCart.HasOnlyContributions())
                    userShippingMethods.Remove(pahOption);
                else
                {
                    if (pahOption != null)
                    {
                        foreach (var cli in CurrentCart.CartLineItems)
                        {
                            if (cli is PackageLineItem || cli is PublicFlexPackageLineItem || cli is FlexPackageLineItem)
                            {
                                //TEMPORARY FOR UTR. Looks at first perf to see if pah should be surpressed.
                                IsTicketInCart = true;
                                if (
                                   !String.IsNullOrEmpty(
                                       ((PublicFlexPackageLineItem)cli).PerformanceLineItems[0].Performance.WebContent["No PAH Ticketing"]))
                                {
                                    if (((PublicFlexPackageLineItem)cli).PerformanceLineItems[0].Performance.WebContent["No PAH Ticketing"] == "1201")
                                        AllowPah = false;
                                }
                            }
                            else if (cli is PerformanceLineItem)
                            {
                                IsTicketInCart = true;
                                //there is a ticket in the cart. if this isnt set, delivery panel with be hidden in this.BindPage()
                                if (
                                    !String.IsNullOrEmpty(
                                        ((PerformanceLineItem)cli).Performance.WebContent["No PAH Ticketing"]))
                                {
                                    if (((PerformanceLineItem)cli).Performance.WebContent["No PAH Ticketing"] == "1201")
                                        AllowPah = false;
                                }
                            }
                        }
                        if (pahOption != null && AllowPah == false)
                            userShippingMethods.Remove(pahOption);
                    }
                }
              
            }
            return userShippingMethods;
        }

        protected bool hideMailDeliveryOption()
        {
            //hide mail option if cart only has GC or shipping address is international or purchase is within set time of performance date
            if (CurrentPublicCart.HasOnlyGiftCards() || !PublicUser.IsUSAAddress(CurrentPublicUser.ShippingAddress) ||
                CurrentPublicCart.HasGalaTickets() || CurrentPublicCart.HasOnlyContributions() )
                return true;
            // For UTR ONLY - Hide symposium packages based on source codes
            if (CurrentCart.SourceNumber == 19117 || CurrentCart.SourceNumber == 18873)
            {
                return true;
            }
          
            //hide mail by mos and prod seas
            string noMailMosProd =
                        (Adage.Tessitura.Config.GetValue("NO_MAIL_MOS_" +
                                                         Adage.Tessitura.TessSession.GetSession().ModeOfSale.ToString() +
                                                         "_PRODSEAS"));
            List<string> noMailMosProdList = null;
            try
            {
                noMailMosProdList = noMailMosProd.Split(';').ToList();
            }
            catch (Exception ex)
            {

                Adage.Common.ElmahCustomError.CustomError.LogError(ex, "Error Parsing 'NO_MAIL_MOS_' config value", null);
            }

            int leadUpTime = Config.GetValue<int>("DAYS_BEFORE_PERF_HIDE_MAIL", 14);
            foreach (var cli in CurrentCart.CartLineItems)
            {
              
                if (cli is PerformanceLineItem)
                {
                    var pli = ((PerformanceLineItem) cli);
                    //if there is web content to remove mail option
                    if (!String.IsNullOrEmpty(pli.Performance.WebContent["No Mail Delivery"]))
                        if (pli.Performance.WebContent["No Mail Delivery"] == "1201")
                            return true;
                    //if there is a performance too close to purchase date to mail
                    if ((pli.Performance.PerformanceDate - DateTime.Now).TotalDays < leadUpTime)
                        return true;
                    // For UTR ONLY - Hide symposium packages based on source codes
                    if (CurrentCart.SourceNumber == 19117 || CurrentCart.SourceNumber == 18873)
                    {
                        return true;
                    }

                    if (noMailMosProdList!=null)
                    {
                        if (noMailMosProdList.Contains(pli.Performance.ProductionSeasonId.ToString()))
                            return true;
                    }
                }
            }

            return false;
        }

        protected override void PopulateShippingMethods()
        {
            base.PopulateShippingMethods();
            //rblShippingMethod.Items.Add(new ListItem("Digital Delivery", "-1"));
            phDeliveryMethod.Visible = PaymentPageContent.ShowDeliveryMethod && !CurrentPublicCart.HasOnlyGiftCards();
        }

        protected void LimitPaymentMethod(string paymentMethod, int prodSeas, List<string> priceTypesList)
        {
            foreach (var item in CurrentPublicCart.CartLineItems)
            {
                if (item is PerformanceLineItem)
                {
                    PerformanceLineItem pli = (PerformanceLineItem) item;
                    if (pli.Performance.ProductionSeasonId == prodSeas)
                    {
                        foreach (string priceTypeString in priceTypesList)
                        {
                            int priceType;
                            if (Int32.TryParse(priceTypeString, out priceType))
                            {
                                if (CurrentPublicCart.PriceTypes.Exists(pt => pt.PriceTypeId == priceType))
                                {
                                    var paymentMethodOption = ddlCreditCardType.Items.FindByValue(paymentMethod);
                                    ddlCreditCardType.Items.Clear();
                                    ddlCreditCardType.Items.Add(paymentMethodOption);
                                    return;

                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                       
                    }
                }

               
            }
        }
    }
}