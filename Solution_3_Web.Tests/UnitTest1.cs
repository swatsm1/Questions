using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Solution_3_Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //README 

            //TO TEST START THE SOLUTION_3_WEB FIRST - IT WILL POINT TO A LOCALHOST (WILL OPEN BROWSER)
            //THEN DEBUG THE TESTMETHOD Solution_3_Web.Tests.
            //CHANGE THE VALUE OF THE XML FILE IN THE UNITTEST 

            string stringfile = File.ReadAllText("XMLFile.xml");

            string strNVPSandboxServer = string.Empty;
                       
            strNVPSandboxServer = "https://localhost:44374/WebService.ashx";
            
            string strNVP = stringfile;

            //Create web request and web response objects, make sure you using the correct server (sandbox/live)
             HttpWebRequest wrWebRequest = (HttpWebRequest)WebRequest.Create(strNVPSandboxServer);

            wrWebRequest.Method = "POST";
            wrWebRequest.ContentType = "application/text";

            var requestWriter = new StreamWriter(wrWebRequest.GetRequestStream());

            requestWriter.Write(strNVP);
            requestWriter.Close();

            // Get the response.
            HttpWebResponse hwrWebResponse = (HttpWebResponse)(wrWebRequest.GetResponse());

            var responseReader = new StreamReader(wrWebRequest.GetResponse().GetResponseStream());
            
            var responseData = responseReader.ReadToEnd();

            WebServiceReponse objUsr = Deserialize<WebServiceReponse>(responseData);


            Console.WriteLine("Status= " + objUsr.Status + " Reason= " + objUsr.Reason);

        }

        public T Deserialize<T>(string context)
        {
            string jsonData = context;

            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
            return obj;
        }
    }
}
