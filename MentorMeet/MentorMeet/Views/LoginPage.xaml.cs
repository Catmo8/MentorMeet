using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorMeet.Users;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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