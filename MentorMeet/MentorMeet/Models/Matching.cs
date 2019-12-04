using SQLite;

namespace MentorMeet.Models
{
    // Keeps track of all the matching connections accepted or denied
    public class Matching
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public User SwipingUser { get; set; }
        public User SwipedOnUser { get; set; }
        public bool SwipedRight { get; set; }
    }
}
