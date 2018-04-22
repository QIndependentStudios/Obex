using System.Xml.Serialization;

namespace QIndependentStudios.Obex.Serialization
{
    public class ObexFolderListingItemXmlData
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
