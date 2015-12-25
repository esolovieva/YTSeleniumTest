using System;
using System.Xml.Linq;
using System.Xml.XPath;

namespace YouTubeTests
{
    public class TestConfig
    {
        private static XDocument testConfigDocument = LoadXml(".\\test_config.xml");


        private static XDocument LoadXml(string xmlDocumentPath)
        {
            try
            {
                
                XDocument document = XDocument.Load(xmlDocumentPath);
                return document;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }


        public static string GetTestVariableValue(string nodeName)
        {
            try
            {
                
                //return testEnvDocument.XPathSelectElement("//*[@name='" + nodeName + "']").Value;
                string s = testConfigDocument.XPathSelectElement("//" + nodeName).Value;
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "";
        }
    }
}
