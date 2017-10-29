namespace DataLibrary.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Id is used so multiple users can have multiple names.
        /// An Id should be a random (hex) unique number so people can not look at other users if they know their id.
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// Name will not have to be unique and can be anything.
        /// </summary>
        string Name { get; set; }
    }
}