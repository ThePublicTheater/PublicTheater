using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PublicTheater.Web.Controls.Calendar
{
    public class CalendarHeader : TheaterTemplate.Web.Controls.CalendarControls.CalendarHeader
    {
        protected override void PopulateDropDownMonths()
        {
            DateTime builder = StartDateToUse;

            while (builder <= EndDateToUse)
            {
                ddlMonths.Items.Add(new ListItem(string.Format("{0} {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(builder.Month), builder.Year),
                                        builder.ToString(DATE_FORMAT)));
                builder = builder.AddMonths(1);
            }

        }
    }
}