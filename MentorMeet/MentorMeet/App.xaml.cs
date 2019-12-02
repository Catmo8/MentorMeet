using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Views;
using MentorMeet.Data;

namespace MentorMeet
{
    public partial class App : Application
    {
        static MentorMeetDatabase database;

        public static MentorMeetDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new  MentorMeetDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#FF461D7C"));
            MainPage.SetValue(NavigationPage.BackgroundColorProperty, Color.FromHex("#FF461D7C"));
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.Gold);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
