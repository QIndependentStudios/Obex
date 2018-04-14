namespace QIndependentStudios.Obex.Path
{
    /// <summary>
    /// Specifies the direction when through Obex directories.
    /// </summary>
    public enum ObexSetPathFlag
    {
        /// <summary>
        /// When a Name header is used in the request, this causes directory navigation to go to the child folder with that name.
        /// If a Name header is not used or its value is blank, this causes navigation to go to the root directory.
        /// </summary>
        DownToNameOrRoot = 0x0002,
        /// <summary>
        /// Causes directory navigation to go up one level.
        /// </summary>
        Up = 0x0003
    }
}
