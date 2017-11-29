using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SmashGgHandler
{
    public partial class ResultRoot
    {
        [JsonProperty("actionRecords")]
        public List<object> ActionRecords { get; set; }

        [JsonProperty("entities")]
        public ResultEntities Entities { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("resultEntity")]
        public string ResultEntity { get; set; }
    }

    public partial class ResultEntities
    {
        //[JsonProperty("entrants")]
        //public List<Entrant> Entrants { get; set; }

        //[JsonProperty("groups")]
        //public Groups Groups { get; set; }

        [JsonProperty("player")]
        public List<Player> Player { get; set; }

        //[JsonProperty("rankingIteration")]
        //public List<RankingIteration> RankingIteration { get; set; }

        //[JsonProperty("rankingSeries")]
        //public List<RankingSery> RankingSeries { get; set; }
        [JsonProperty("seeds")]
        public List<Seed> Seeds { get; set; }
    }

    public partial class RankingSery
    {
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("locationId")]
        public long? LocationId { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("locationRadius")]
        public long? LocationRadius { get; set; }

        [JsonProperty("locationType")]
        public long LocationType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permissionType")]
        public string PermissionType { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("radiusUnit")]
        public string RadiusUnit { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("sourceId")]
        public long? SourceId { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("videogameId")]
        public long VideogameId { get; set; }
    }

    public partial class RankingIteration
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("endAt")]
        public long? EndAt { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("permissionType")]
        public string PermissionType { get; set; }

        [JsonProperty("published")]
        public bool Published { get; set; }

        [JsonProperty("publishedAt")]
        public long PublishedAt { get; set; }

        [JsonProperty("seriesId")]
        public long SeriesId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("startAt")]
        public long? StartAt { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("submittedAt")]
        public long? SubmittedAt { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }
    }

    public partial class Player
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("entrantId")]
        public string EntrantId { get; set; }

        [JsonProperty("gamerTag")]
        public string GamerTag { get; set; }

        [JsonProperty("gamerTagChangedAt")]
        public long? GamerTagChangedAt { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("nameDisplay")]
        public string NameDisplay { get; set; }

        [JsonProperty("permissionType")]
        public string PermissionType { get; set; }

        [JsonProperty("playerType")]
        public string PlayerType { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("rank")]
        public long? Rank { get; set; }

        [JsonProperty("rankings")]
        public List<Ranking> Rankings { get; set; }

        [JsonProperty("redditUsername")]
        public object RedditUsername { get; set; }

        [JsonProperty("region")]
        public object Region { get; set; }

        [JsonProperty("slug")]
        public object Slug { get; set; }

        [JsonProperty("smashboardsLink")]
        public string SmashboardsLink { get; set; }

        [JsonProperty("smashboardsUserId")]
        public long? SmashboardsUserId { get; set; }

        [JsonProperty("ssbwikiLink")]
        public object SsbwikiLink { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("streamMeStream")]
        public object StreamMeStream { get; set; }

        [JsonProperty("twitchStream")]
        public string TwitchStream { get; set; }

        [JsonProperty("twitterHandle")]
        public string TwitterHandle { get; set; }

        [JsonProperty("youtube")]
        public object Youtube { get; set; }
    }

    public partial class Ranking
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("delta")]
        public double? Delta { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("iterationId")]
        public long IterationId { get; set; }

        [JsonProperty("publishedAt")]
        public long PublishedAt { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("seriesId")]
        public long SeriesId { get; set; }

        [JsonProperty("sourceId")]
        public long SourceId { get; set; }

        [JsonProperty("videogameId")]
        public long VideogameId { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("createdAt")]
        public object CreatedAt { get; set; }

        [JsonProperty("entity")]
        public object Entity { get; set; }

        [JsonProperty("entityId")]
        public object EntityId { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("isOriginal")]
        public bool IsOriginal { get; set; }

        [JsonProperty("ratio")]
        public double Ratio { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("updatedAt")]
        public object UpdatedAt { get; set; }

        [JsonProperty("uploadedBy")]
        public object UploadedBy { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Groups
    {
        [JsonProperty("applyAll")]
        public object ApplyAll { get; set; }

        [JsonProperty("autoAssigning")]
        public object AutoAssigning { get; set; }

        [JsonProperty("bestOf")]
        public object BestOf { get; set; }

        [JsonProperty("canAutoAssign")]
        public bool CanAutoAssign { get; set; }

        [JsonProperty("displayIdentifier")]
        public string DisplayIdentifier { get; set; }

        //[JsonProperty("expand")]
        //public List<string> Expand { get; set; }

        [JsonProperty("finalized")]
        public bool Finalized { get; set; }

        [JsonProperty("firstRoundTime")]
        public object FirstRoundTime { get; set; }

        [JsonProperty("groupTypeId")]
        public long GroupTypeId { get; set; }

        [JsonProperty("hasCustomWinnerByes")]
        public bool HasCustomWinnerByes { get; set; }

        [JsonProperty("hasSets")]
        public bool HasSets { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("losersTargetPhaseId")]
        public object LosersTargetPhaseId { get; set; }

        [JsonProperty("matchmakingEnabled")]
        public bool MatchmakingEnabled { get; set; }

        [JsonProperty("numProgressing")]
        public long NumProgressing { get; set; }

        [JsonProperty("numRounds")]
        public object NumRounds { get; set; }

        [JsonProperty("percentageComplete")]
        public string PercentageComplete { get; set; }

        [JsonProperty("phaseId")]
        public long PhaseId { get; set; }

        [JsonProperty("pointsPerBye")]
        public object PointsPerBye { get; set; }

        [JsonProperty("pointsPerGameWin")]
        public object PointsPerGameWin { get; set; }

        [JsonProperty("pointsPerMatchWin")]
        public object PointsPerMatchWin { get; set; }

        [JsonProperty("poolRefId")]
        public object PoolRefId { get; set; }

        [JsonProperty("rematchSeconds")]
        public long RematchSeconds { get; set; }

        [JsonProperty("rounds")]
        public List<object> Rounds { get; set; }

        [JsonProperty("runOnce")]
        public object RunOnce { get; set; }

        [JsonProperty("scheduleId")]
        public long ScheduleId { get; set; }

        [JsonProperty("seeds")]
        public List<object> Seeds { get; set; }

        [JsonProperty("sets")]
        public List<object> Sets { get; set; }

        [JsonProperty("setsOnDeck")]
        public long SetsOnDeck { get; set; }

        [JsonProperty("startAt")]
        public object StartAt { get; set; }

        [JsonProperty("startedAt")]
        public object StartedAt { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("tiebreakOrder")]
        public List<TiebreakOrder> TiebreakOrder { get; set; }

        [JsonProperty("tiebreaks")]
        public object Tiebreaks { get; set; }

        [JsonProperty("title")]
        public object Title { get; set; }

        [JsonProperty("waveId")]
        public object WaveId { get; set; }

        [JsonProperty("winnersTargetPhaseId")]
        public object WinnersTargetPhaseId { get; set; }
    }
    public partial class Entrant
    {
        [JsonProperty("defaultSkill")]
        public long DefaultSkill { get; set; }

        [JsonProperty("eventId")]
        public long EventId { get; set; }

        [JsonProperty("expand")]
        public List<object> Expand { get; set; }

        [JsonProperty("finalPlacement")]
        public long FinalPlacement { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("initialSeedNum")]
        public long InitialSeedNum { get; set; }

        //[JsonProperty("isPlaceholder")]
        //public object IsPlaceholder { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("participant1Id")]
        //public object Participant1Id { get; set; }

        //[JsonProperty("participant2Id")]
        //public object Participant2Id { get; set; }

        [JsonProperty("participantIds")]
        public List<long> ParticipantIds { get; set; }

        [JsonProperty("playerIds")]
        public Dictionary<string, long?> PlayerIds { get; set; }

        [JsonProperty("prefixes")]
        public Dictionary<int, string> Prefixes { get; set; }

        //[JsonProperty("skill")]
        //public long Skill { get; set; }

        //[JsonProperty("skillOrder")]
        //public long SkillOrder { get; set; }

        [JsonProperty("unverified")]
        public bool Unverified { get; set; }
    }

    public class Seed
    {
        [JsonProperty("checkInState")]
        public long CheckInState { get; set; }

        [JsonProperty("entrantId")]
        public long EntrantId { get; set; }

        [JsonProperty("expand")]
        public List<object> Expand { get; set; }

        [JsonProperty("groupSeedNum")]
        public long GroupSeedNum { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("isBye")]
        public bool IsBye { get; set; }

        [JsonProperty("isDisqualified")]
        public bool? IsDisqualified { get; set; }

        [JsonProperty("isEligible")]
        public bool? IsEligible { get; set; }

        [JsonProperty("isFinal")]
        public bool? IsFinal { get; set; }

        [JsonProperty("isSeeded")]
        public bool? IsSeeded { get; set; }

        [JsonProperty("losses")]
        public long Losses { get; set; }

        [JsonProperty("phaseGroupId")]
        public long PhaseGroupId { get; set; }

        [JsonProperty("phaseId")]
        public long PhaseId { get; set; }

        [JsonProperty("phaseLinkId")]
        public long? PhaseLinkId { get; set; }

        [JsonProperty("placement")]
        public long? Placement { get; set; }

        [JsonProperty("prereqProgressionId")]
        public object PrereqProgressionId { get; set; }

        [JsonProperty("progressionSeedId")]
        public object ProgressionSeedId { get; set; }

        [JsonProperty("projectedEntrantId")]
        public object ProjectedEntrantId { get; set; }

        [JsonProperty("seedNum")]
        public long SeedNum { get; set; }

        [JsonProperty("unverified")]
        public bool Unverified { get; set; }
    }
}
