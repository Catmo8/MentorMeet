using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Views;

namespace MentorMeet
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
