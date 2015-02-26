using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    /// <summary>
    /// Public API Monitor - Allows monitoring of calls going through the Tessitura API
    /// </summary>
    public class PublicApiMonitor : TheaterTemplate.Shared.Common.TheaterSharedApiMonitor
    {

        protected override void PostApiCall(string methodName, object[] parameters, object[] results)
        {
            base.PostApiCall(methodName, parameters, results);
        }

    }
}
