using System;
using SQLite;

namespace MentorMeet.Models
{
    public class MessagesTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
