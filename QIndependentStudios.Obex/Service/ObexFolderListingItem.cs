namespace QIndependentStudios.Obex.Service
{
    /// <summary>
    /// An Obex folder.
    /// </summary>
    public class ObexFolderListingItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObexFolderListingItem"/> class with a folder name.
        /// </summary>
        /// <param name="name">THe folder's name.</param>
        public ObexFolderListingItem(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the folder.
        /// </summary>
        public string Name { get; }
    }
}
