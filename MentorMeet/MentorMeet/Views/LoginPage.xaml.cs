using System;
using MentorMeet.Models;
using MentorMeet.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void LoginClicked(object sender, System.EventArgs e)
        {
            try
            {
                //connect to database and check for existing user 
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                var conn = new SQLiteConnection(dbPath);
                var data = conn.Table<User>();
                var user = data.Where(x => x.Email == UsernameEntry.Text && x.Password == PasswordEntry.Text).FirstOrDefault();

                if ((string.IsNullOrWhiteSpace(UsernameEntry.Text)) || (string.IsNullOrWhiteSpace(PasswordEntry.Text)) ||
                    (string.IsNullOrEmpty(UsernameEntry.Text)) || (string.IsNullOrEmpty(PasswordEntry.Text)))
                {
                    await DisplayAlert("Error", "Username or Password is empty", "OK");
                }
                else if (user == null)
                {
                    await DisplayAlert("Error", "Username or Password invalid", "OK");
                }
                else
                {
                    //retrieve profile fields to currently logged in user
                    CurrentUser.First = user.First;
                    CurrentUser.Last = user.Last;
                    CurrentUser.Email = user.Email;
                    CurrentUser.Major = user.Major;
                    CurrentUser.IsMentor = user.IsMentor;
                    CurrentUser.Interests = user.Interests;
                    CurrentUser.Details = user.Details;

                    conn.Close();

                    await Navigation.PushModalAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        //navigate to Sign-Up Page
        async void SignUpClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpPage());
        }

        private void UsernameEntry_Completed(object sender, EventArgs e)
        {
            PasswordEntry.Focus();
        }
    }
}