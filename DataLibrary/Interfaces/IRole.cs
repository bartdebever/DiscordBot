namespace DataLibrary.Interfaces
{
    /// <summary>
    /// A class used to keep track of roles given to the user.
    /// This is is more seen as a Discord role rather than a League of Legends Role.
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// The internal Id the role has.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// The name the role has.
        /// </summary>
        string Name { get; set; }
    }
}