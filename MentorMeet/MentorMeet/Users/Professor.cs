using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MentorMeet.Users
{
    public class Professor
    {
        public string name;
        public string university;
        public string details;
        public string picture;
        public Professor()
        {
            picture = "";
            name = "";
            university = "";
            details = "";
        }

        public Professor(string u, string n, string d)
        {
            picture = n.Replace(' ','_') + ".png";
            name = n;
            university = u;
            details = d;
        }
    }
}
