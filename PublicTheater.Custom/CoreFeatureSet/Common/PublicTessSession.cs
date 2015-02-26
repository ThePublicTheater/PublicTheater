using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Adage.Tessitura;

namespace PublicTheater.Custom.CoreFeatureSet.Common
{
    public class PublicTessSession : Adage.Tessitura.TessSession
    {
        /// <summary>
        /// Session is in the flex package path, if the current session source code is a flex package source code
        /// </summary>
        /// <returns></returns>
        public static bool IsInFlexPackagePath()
        {
            var currentSession = GetSession();
            var promoCodeDetails = PromoCodeDetails.Get(currentSession.PromotionCode);
            return IsFlexPackageSourceCode(promoCodeDetails.SourceName);
        }

        /// <summary>
        /// determine if a source code name is a flex package source code
        /// </summary>
        /// <param name="sourceCodeName"></param>
        /// <returns></returns>
        public static bool IsFlexPackageSourceCode(string sourceCodeName)
        {
            var configSourceCodeName = Config.GetValue("FlexPackageSourceCodeName", "Package");

            return sourceCodeName.ToLower().Contains(configSourceCodeName.ToLower());
        }
        /// <summary>
        /// Serialize object and save into session variable
        /// </summary>
        /// <param name="objectToSerilalize"></param>
        /// <param name="sVariableName"></param>
        internal static void SerializeSessionVar(object objectToSerilalize, string sVariableName)
        {
            GetSession().Variables.SetVariable(sVariableName, XmlSerialize(objectToSerilalize));
        }

        /// <summary>
        /// Deserializes the object from the session variables
        /// </summary>
        /// <returns>Deserialized object</returns>
        internal static T DeserializeSessionVar<T>(string sVariableName)
        {
            var variableValue = GetSession().Variables.GetVariable(sVariableName);
            return XmlDeserialize<T>(variableValue);
        }


        /// <summary>
        /// xml serialize
        /// </summary>
        /// <param name="objectToSerilalize"></param>
        /// <returns></returns>
        internal static string XmlSerialize(object objectToSerilalize)
        {
            var serializer = new XmlSerializer(objectToSerilalize.GetType());

            var writeCsiInfo = new StringWriter();
            var xWriter = new XmlTextWriter(writeCsiInfo);
            serializer.Serialize(xWriter, objectToSerilalize);

            return writeCsiInfo.GetStringBuilder().ToString();
        }

        /// <summary>
        /// xml deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        internal static T XmlDeserialize<T>(string xmlString)
        {
            var deserializer = new XmlSerializer(typeof(T));
            var readCsiInfo = new StringReader(xmlString);
            var xRdr = new XmlTextReader(readCsiInfo);
            return (T)deserializer.Deserialize(xRdr);
        }
    }
}
