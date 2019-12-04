using SQLite;

namespace MentorMeet.Models
{
    // Matches made are stored here
    public class Matches
    {
        [PrimaryKey, AutoIncrement]
        public int MatchId { get; set; }
        public User MatchingUser { get; set; }
        public User MatchedUser { get; set; }
    }
}
