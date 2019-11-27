using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MentorMeet.Users
{
    public class Professor
    {
        public string name { get; set; }
        public string university { get; set; }
        public string details { get; set; }
        public string picture { get; set; }

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
