using SQLite;

namespace MentorMeet.Models
{
    // Keeps track of all the matching connections accepted or denied
    public class Matching
    {
        [PrimaryKey, AutoIncrement]
        public int MatchingId { get; set; }
        public string SwipingUserEmail { get; set; }
        public string SwipedOnUserEmail { get; set; }
        public bool SwipedRight { get; set; }
    }
}
