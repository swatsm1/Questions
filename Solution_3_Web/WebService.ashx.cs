using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Solution_3_Web
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    public class WebService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string strXml = new StreamReader(context.Request.InputStream).ReadToEnd();

            var xml = processFile(strXml);

            var obj = new JavaScriptSerializer().Serialize(xml);

            context.Response.ContentType = "application/json; charset=utf-8";

            context.Response.Write(obj);
        }

        private WebServiceReponse processFile(string strXml)
        {
            //string stringfile = File.ReadAllText(strXml);

            XmlSerializer InDoc = new XmlSerializer(typeof(InputDocument));

            TextReader myreader = new StringReader(strXml);
            string myxml = myreader.ReadToEnd();

            InputDocument inputDoc = new InputDocument();
            TextReader myReader = new StringReader(myxml);

            inputDoc = (InputDocument)InDoc.Deserialize(myReader);

            InputDocumentDeclarationListDeclaration references = inputDoc.DeclarationList.Declaration;
            InputDocumentDeclarationListDeclarationDeclarationHeader DeclarationHeader = references.DeclarationHeader;

            WebServiceReponse R = new WebServiceReponse();
            if (DeclarationHeader.SiteID != "DUB")
            {
                R.Status = -2;
                R.Reason = "– invalid Site specified";
            }
            
            else if (references.Command != "DEFAULT")
            {
                R.Status = -1;
                R.Reason = "1’ – which means invalid command specified.";
            }   

            else
            {
                R.Status = 0;
                R.Reason = "SUCCESS";

            }

            return R;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}