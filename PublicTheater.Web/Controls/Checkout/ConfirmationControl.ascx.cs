using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Adage.Tessitura;
using Adage.Tessitura.CartObjects;

namespace PublicTheater.Web.Controls.Checkout
{
    public class ConfirmationControl : TheaterTemplate.Web.Controls.CheckoutControls.ConfirmationControl
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (Session["BestAvailableLiSeqNo"] != null)
            {

                foreach (var li in CurrentCart.CartLineItems.OfType<PerformanceLineItem>())
                {
                    if (!((List<int>)Session["BestAvailableLiSeqNo"]).Contains(li.ItemId))
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("@li_seq_no",li.ItemId);
                        dic.Add("@notes","SYOS");
                        dic.Add("@category", 14);
                        TessSession.GetSession().ExecuteLocalProcedure("Update_Special_Req_Notes", dic);
                        break;
                    }
                }

            }
            else
            {
                foreach (var li in CurrentCart.CartLineItems.OfType<PerformanceLineItem>())
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("@li_seq_no", li.ItemId);
                    dic.Add("@notes", "SYOS");
                    dic.Add("@category", 14);
                    TessSession.GetSession().ExecuteLocalProcedure("Update_Special_Req_Notes", dic);
                }
            }
        }

        protected override void TransferSession()
        {
            base.TransferSession();

            try
            {
                //reset mos after purchase
                CurrentUser.Logout();
                CurrentUser.Login();
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("reset mos after purchase - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }

        protected override void SendOrderCsiData()
        {
            base.SendOrderCsiData();
            try
            {
                //also send out order note which saved as csi
                var csi = CustomerServiceIssue.GetFor(Config.GetValue(Custom.CoreFeatureSet.Common.PublicAppSettings.OrderNotesCSISessionKey, "OrderNotesCSISessionKey"));
                if (csi != null && !string.IsNullOrEmpty(csi.Notes))
                {
                    csi.SendNow();
                }
            }
            catch (Exception ex)
            {
                Adage.Common.ElmahCustomError.CustomError.LogError(ex, string.Format("Send CSI - {0} - {1}", ex.Message, ex.StackTrace), null);
            }
        }
    }
}