using System.Xml.Serialization;

namespace QIndependentStudios.Obex.Serialization
{
    /// <summary>
    /// Object representing the XML Obex folder listing response.
    /// </summary>
    [XmlRoot("folder-listing")]
    public class ObexFolderListingXmlData
    {
        /// <summary>
        /// The collection of Obex folder listing item XML elements.
        /// </summary>
        [XmlElement("folder")]
        public ObexFolderListingItemXmlData[] Items { get; set; }
    }
}
