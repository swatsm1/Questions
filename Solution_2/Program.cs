using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Solution_2
{
    class Program
    {
        static void Main(string[] args)
        {

        var xml = processFile("XMLFile.xml");

            foreach (Refcode r in xml)
            {
                Console.WriteLine(" The Refcodes are " + r.refcode );
            }

            Console.WriteLine($"Press any key to continue");
            Console.ReadLine();

        }



        private static List<Refcode> processFile(string v)
        {
            string stringfile = File.ReadAllText(v);

            XmlSerializer InDoc = new XmlSerializer(typeof(InputDocument));

            TextReader myreader = new StringReader(stringfile);
            string myxml = myreader.ReadToEnd();
            
            InputDocument inputDoc = new InputDocument();
            TextReader myReader = new StringReader(myxml);

            inputDoc = (InputDocument)InDoc.Deserialize (myReader);

            InputDocumentDeclarationListDeclaration references = inputDoc.DeclarationList.Declaration;
            InputDocumentDeclarationListDeclarationDeclarationHeader DeclarationHeader = references.DeclarationHeader;
           
            var listofRefs = new List<Refcode>(); 
            foreach (InputDocumentDeclarationListDeclarationDeclarationHeaderReference reference in DeclarationHeader.Reference)
            {
                if (reference.RefCode == "MWB" || reference.RefCode == "TRV" || reference.RefCode == "CAR")
                {
                    var refs = new Refcode();

                    refs.refcode = reference.RefCode;

                    listofRefs.Add(refs);
                }
                
            }

            return listofRefs; 


        }
    }
}
