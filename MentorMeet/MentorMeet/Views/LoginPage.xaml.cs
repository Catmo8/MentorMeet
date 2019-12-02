using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorMeet.Models;
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
            var item = new User(UsernameEntry.Text, PasswordEntry.Text);

            //check for empty imputs
            if (item.checkIfEmpty())
            {
                DisplayAlert("Message", "Username or password is empty", "OK");
                return;
            }

            string textToDisplay = "";

            //check for valid LSU id
            if (!item.checkIfLSUid())
            {
                textToDisplay = "Enter LSUID (with @lsu.edu) to login";
                
            }
            */

            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3")))
            {
                var account = conn.Table<User>().ToList();
            }
            if (!account.Contains(UsernameEntry.Text))
            {
                DisplayAlert("Message", "Username o)
            }

            await Navigation.PushModalAsync(new MainPage());
            
        }

        //response after sign up button clicked
        async void SignUpClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpPage());
        }

        async void databaseClicked(object sender, System.EventArgs e)
        {

        }
    }
}