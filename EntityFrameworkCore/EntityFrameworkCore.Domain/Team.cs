namespace EntityFrameworkCore.Domain
{
    public class Team : BaseDomainModel
    {
        public string? Name { get; set; }
        //public int? TeamId {  get; set; }
        public int LeagueId {  get; set; }
        public int CoachId {  get; set; }
    }
}
