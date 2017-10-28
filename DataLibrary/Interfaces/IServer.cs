namespace DataLibrary.Interfaces
{
    public interface IServer
    {
        string Name { get; set; }
        int Votes { get; }
        string JoinMethod { get; set; }
        string Description { get; set; }
        IUser Owner { get; set; }
        void Vote(IUser user);
        void RemoveVote(IUser user);
    }
}