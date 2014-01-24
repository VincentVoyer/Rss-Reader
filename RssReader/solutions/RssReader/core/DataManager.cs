using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Core
{
    public class DataManager
    {
        private List<Flow> result = new List<Flow>();
        private XNamespace mUri = XMLTags.URIDATA;
        private String xmlF = "../data/XML/data.xml";
        private String xsdF = "../data/XML/data.xsd";

        /// <summary>
        /// the root Document Node
        /// </summary>
        protected XDocument mXDoc = null;

        public static DataManager Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new DataManager();
                return mInstance;
            }
        }
        private static DataManager mInstance = null;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="xmlFile">xml file to parse</param>
        private DataManager()
        {
            try
            {
                LoadXMLFile(xmlF, xsdF, XMLTags.URIDATA);
            }
            catch (SystemException e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// loads an xml file
        /// </summary>
        /// <param name="xml_file">xml file to load</param>
        void LoadXMLFile(string xml_file, string xsdFile, string namespaceURI)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);

                System.Xml.Schema.XmlSchemaSet schemaSet = new System.Xml.Schema.XmlSchemaSet();
                schemaSet.Add(namespaceURI, xsdFile);
                mXDoc.Validate(schemaSet, ValidatingProblemHandler);
            }
            catch { }
        }

        /// <summary>
        /// Handler for schema validation of the score xml file
        /// </summary>
        /// <param name="sender">sender of the event</param>
        /// <param name="e">arguments of the event</param>
        void ValidatingProblemHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == System.Xml.Schema.XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
        private void Save()
        {
            mXDoc.Save(xmlF);
        }

        public void Write(Flow rss)
        {
            foreach (Flow f in result)
            {
                if (f.Name.Equals(rss.Name))
                    return;
            }
            XElement element = new XElement(mUri + XMLTags.FLUX,
                    new XElement(mUri + XMLTags.NOM, rss.Name),
                    new XElement(mUri + XMLTags.DESC, rss.Description),
                    new XElement(mUri + XMLTags.LIEN, rss.Link));

            mXDoc.Element(mUri + XMLTags.DATARSS).Add(element);
            Save();
        }

        public List<Flow> Read()
        {
            
            result = new List<Flow>();
            XElement root = mXDoc.Root;
            List<XElement> items = root.Elements(mUri + XMLTags.FLUX).ToList();
            if (items != null)
            {
                foreach (XElement element in items)
                {
                    try
                    {
                        string nom = element.Element(mUri + XMLTags.NOM).Value;
                        string description = element.Element(mUri + XMLTags.DESC).Value;
                        string lien = element.Element(mUri + XMLTags.LIEN).Value;

                        result.Add(new Flow(nom, description, lien));
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }
                }
            }
            return result;
        }

        public void Remove(String flowName)
        {
            Flow f = result.Where(rss => rss.Name == flowName).ToList().ElementAt(0);
            result.Remove(f);

            XElement removeElement = null;
            XElement root = mXDoc.Root;
            List<XElement> items = root.Elements(mUri + XMLTags.FLUX).ToList();
            if (items != null)
            {
                foreach (XElement element in items)
                {
                    try
                    {
                        string nom = element.Element(mUri + XMLTags.NOM).Value;

                        if (nom.Equals(flowName))
                            removeElement = element;
                        

                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }
                }

                removeElement.Remove();
                Save();
            }
        }
    }
}

