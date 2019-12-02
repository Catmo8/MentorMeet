using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMeet.Users
{
    class User
    {
        public string username { get; set; }
        public string password { get; set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public bool checkIfEmpty()
        {
            if (string.IsNullOrEmpty(username) == true || string.IsNullOrEmpty(password) == true)
            {
                return true;
            }
            return false;
        }


        public bool checkIfLSUid()
        {
            int l = username.Length;

            if (l < 7)
            {
                return false;
            }

            string lastadd = username.Substring(l - 7);

            if (lastadd.Equals("lsu.edu"))
            {
                return true;
            }

            return false;
        }

        public bool containsAstreisks()
        {

            if (password.Contains("*"))
            {
                return true;
            }
            return false;
        }
    }
}
