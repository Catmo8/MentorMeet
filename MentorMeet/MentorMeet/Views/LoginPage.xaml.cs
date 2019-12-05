using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //response after login button clicked
        async void LoginClicked(object sender, System.EventArgs e)
        {
            /*
            //check for valid LSU id
            if (!item.checkIfLSUid())
            {
                textToDisplay = "Enter LSUID (with @lsu.edu) to login";
                
            }
            */
            /*
             using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3")))
            {
                var account = conn.Table<User>().ToList();
            }
            if (!account.Contains(UsernameEntry.Text))
            {
                DisplayAlert("Message", "Username o)
            }
            */
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                var conn = new SQLiteConnection(dbPath);
                var data = conn.Table<User>();
                var data1 = data.Where(x => x.Email == UsernameEntry.Text && x.Password == PasswordEntry.Text).FirstOrDefault();
                if((string.IsNullOrWhiteSpace(UsernameEntry.Text)) || (string.IsNullOrWhiteSpace(PasswordEntry.Text)) ||
                    (string.IsNullOrEmpty(UsernameEntry.Text)) || (string.IsNullOrEmpty(PasswordEntry.Text)))
                {
                    await DisplayAlert("Error", "Username or Password is empty", "OK");
                }
                else if (data1 == null)
                {
                    await DisplayAlert("Error", "Username or Password invalid", "OK");
                }
                else
                {
                    CurrentUser.First = data1.First;
                    CurrentUser.Last = data1.Last;
                    CurrentUser.Email = data1.Email;
                    CurrentUser.Major = data1.Major;
                    CurrentUser.IsMentor = data1.IsMentor;
                    CurrentUser.Interests = data1.Interests;
                    CurrentUser.Details = data1.Details;

                    conn.Close();
                    
                    await Navigation.PushModalAsync(new MainPage());
                    
                }
            }
            catch (Exception ex)
            {
                //await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        //response after sign up button clicked
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