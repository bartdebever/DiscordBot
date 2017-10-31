namespace DataLibrary.Useraccounts.Interfaces
{
    /// <summary>
    /// A summoner is an account on League of Legends.
    /// A user can own multiple summoners.
    /// </summary>
    public interface ISummoner
    {
        /// <summary>
        /// An Id that Riot Games uses to identify users.
        /// </summary>
        long SummonerId { get; set; }
        /// <summary>
        /// A region is a server that users play on. This needs to be tracked to get their information back
        /// 
        /// TODO Convert to proper region once that is commited
        /// </summary>
        string Region { get; set; }
    }
}
