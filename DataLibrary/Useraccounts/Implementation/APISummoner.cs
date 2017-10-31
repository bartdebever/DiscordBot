using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Useraccounts.Implementation
{
    public class APISummoner : ISummoner
    {
        public long SummonerId { get; set; }
        public string Region { get; set; }

        public APISummoner(long summonerId, string region)
        {
            this.SummonerId = summonerId;
            this.Region = region;
        }
    }
}
