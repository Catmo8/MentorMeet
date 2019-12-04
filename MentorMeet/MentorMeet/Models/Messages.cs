using System;
using SQLite;

namespace MentorMeet.Models
{
    // Keeps track of all the messages sent to and from users
    public class Messages
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
}
