using System;
using System.Collections.Generic;
using System.Text;

namespace MentorMeet.Users
{
    public class Professor
    {
        public string name;
        public string university;
        public string details;

        public Professor()
        {
            name = "";
            university = "";
            details = "";
        }

        public Professor(string u, string n, string d)
        {
            name = n;
            university = u;
            details = d;
        }
    }
}
