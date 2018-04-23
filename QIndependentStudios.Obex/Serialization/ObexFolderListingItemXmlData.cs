using System.Xml.Serialization;

namespace QIndependentStudios.Obex.Serialization
{
    /// <summary>
    /// Object representing an Obex folder listing item XML element.
    /// </summary>
    public class ObexFolderListingItemXmlData
    {
        /// <summary>
        /// The name of the folder.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
