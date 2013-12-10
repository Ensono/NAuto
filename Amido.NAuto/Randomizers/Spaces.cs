namespace Amido.NAuto.Randomizers
{
    /// <summary>
    /// Enumeration for settings spaces within random strings.
    /// </summary>
    public enum Spaces
    {
        /// <summary>
        /// No spaces.
        /// </summary>
        None,

        /// <summary>
        /// Add spaces anywhere within the string.
        /// </summary>
        Any,

        /// <summary>
        /// Add space at the start of the string.
        /// </summary>
        Start,

        /// <summary>
        /// Add space in the middle of the string.
        /// </summary>
        Middle,

        /// <summary>
        /// Add space at the end of the string.
        /// </summary>
        End,

        /// <summary>
        /// Add space at the start and end of the string.
        /// </summary>
        StartAndEnd
    }
}