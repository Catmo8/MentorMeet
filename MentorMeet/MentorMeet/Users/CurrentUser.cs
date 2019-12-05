﻿namespace MentorMeet.Users
{
    // Saves user that is currently logged in
    public static class CurrentUser
    {
        public static string First { get; set; }
        public static string Last { get; set; }
        public static string Email { get; set; }
        public static string Major { get; set; }
        public static bool IsMentor { get; set; }
        public static string Interests { get; set; }
        public static string Details { get; set; }
    }
}
