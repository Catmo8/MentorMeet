using System;
using SQLite;

namespace MentorMeet.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
        public string Major { get; set; }
        public string Password { get; set; }
        public bool IsMentor { get; set; }
        public string Details { get; set; }
    }
}  
