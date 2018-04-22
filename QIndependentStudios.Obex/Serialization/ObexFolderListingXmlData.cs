using System.Xml.Serialization;

namespace QIndependentStudios.Obex.Serialization
{
    [XmlRoot("folder-listing")]
    public class ObexFolderListingXmlData
    {
        [XmlElement("folder")]
        public ObexFolderListingItemXmlData[] Items { get; set; }
    }
}
