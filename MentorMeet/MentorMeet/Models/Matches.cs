using SQLite;

namespace MentorMeet.Models
{
    // Matches made are stored here
    public class Matches
    {
        [PrimaryKey, AutoIncrement]
        public int MatchId { get; set; }
        public string MatchingUserEmail { get; set; }
        public string MatchedUserEmail { get; set; }
    }
}
