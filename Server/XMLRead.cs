using System.Xml;

namespace WDeff.Server
{
    public class XMLRead
    {
        public void Read(string filepath)
        {
            // odczyt zawartości dokumentu
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            string langFrom ;
            string langTo;
            string text;
            
            var translate = doc.GetElementsByTagName("Translate");

            foreach (XmlNode w in translate)
            {
                langFrom = w.Attributes.GetNamedItem("fromLanguage").Value;
                langTo = w.Attributes.GetNamedItem("toLanguage").Value;
                text = w.FirstChild.Attributes.GetNamedItem("text").Value;
                
            }
        }
    }
}
