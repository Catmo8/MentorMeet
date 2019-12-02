using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MentorMeet.Models
{
    public class Message
    {
        public string name { get; set; }
        public DateTime time { get; set; }
        public string text { get; set; }
        public LayoutOptions nameSide { get; set; }
        public TextAlignment textSide { get; set; }

        public Message(string name, DateTime time, string text, bool isFromUser)
        {
            this.name = name;
            this.time = time;
            this.text = text;

            if (isFromUser)
            {
                nameSide = LayoutOptions.EndAndExpand;
                textSide = TextAlignment.End;
            }
            else
            {
                nameSide = LayoutOptions.StartAndExpand;
                textSide = TextAlignment.Start;
            }
        }
    }
}
