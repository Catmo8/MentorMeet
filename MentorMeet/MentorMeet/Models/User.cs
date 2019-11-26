using System;
using SQLite;

namespace MentorMeet.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string first { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPass { get; set; }
    }
}  
