using DataLibrary.Useraccounts.Interfaces;

namespace DataLibrary.Useraccounts.Implementation
{
    public class APISummoner : ISummoner
    {
        public int Id { get; set; }
        public long SummonerId { get; set; }
        public string Region { get; set; }

        public APISummoner(long summonerId, string region)
        {
            this.SummonerId = summonerId;
            this.Region = region;
        }

        public APISummoner(int id, long summonerId, string region)
        {
            this.SummonerId = summonerId;
            this.Region = region;
            this.Id = id;
        }
    }
}
