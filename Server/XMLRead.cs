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

            string langFrom = string.Empty;
            string langTo = string.Empty;
            string inputText = string.Empty;
            string outputText = string.Empty;
            
            var translate = doc.GetElementsByTagName("Translation");

            foreach (XmlNode w in translate)
            {
                langFrom = w.Attributes.GetNamedItem("fromLanguage").Value;
                langTo = w.Attributes.GetNamedItem("toLanguage").Value;
                inputText = w.FirstChild.Attributes.GetNamedItem("InputText").Value;
                outputText = w.FirstChild.Attributes.GetNamedItem("OutputText").Value;
                
            }
        }
    }
}
