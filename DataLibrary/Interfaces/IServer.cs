namespace DataLibrary.Interfaces
{
    /// <summary>
    /// A server that a user owns and can be listed
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// The name of the server
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// A short summary on how someone can join the server.
        /// </summary>
        string JoinMethod { get; set; }
        /// <summary>
        /// A summary on what the server is about.
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// The user who created the server.
        /// </summary>
        IUser Owner { get; set; }
    }
}