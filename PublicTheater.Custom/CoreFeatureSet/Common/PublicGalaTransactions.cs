using Adage.Tessitura;
using System.Data;
using System.Collections.Generic;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicGalaTransactions
    {
        public static List<KeyValuePair<string, string>> GetGalaDetails()
        {
            // provide a garabage login name so it doesn't skew the results for now
            string loginName = "!@#$^%^&*(_)!@#$^%^&*(_)";

            string sqlParameters = string.Format("@emailAddress=a@b.com&@custLogin={0}", loginName);
            int storedProcId = Config.GetValue("LPW_GET_GALA_DETAILS", 8048);
            DataSet currentResults = TessituraAPI.Get().ExecuteLocalProcedure(TessSession.GetSessionKey(), storedProcId, sqlParameters);

            var finalResults = new List<KeyValuePair<string, string>>();
            if (currentResults != null && currentResults.Tables.Count > 0)
            {
                foreach (DataRow eachRow in currentResults.Tables[0].Rows)
                {
                    finalResults.Add(new KeyValuePair<string, string>((string)eachRow[1], (string)eachRow[0]));
                }
            }

            return finalResults;
        }
    }
}
