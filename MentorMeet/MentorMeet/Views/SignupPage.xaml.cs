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
    public partial class SignUpPage : ContentPage
    {
        public bool isMentor;

        public SignUpPage()
        {
            InitializeComponent();
            isMentor = false;
        }

        async private void Register_Clicked(object sender, EventArgs e)
        {
            //string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
            //var conn = new SQLiteConnection(dbPath);
            //var data = conn.Table<User>();
            //var data1 = data.Where(x => x.Email == Email.Text).FirstOrDefault();
            String eduSubstring = "lsu.edu";
            if ((string.IsNullOrWhiteSpace(First.Text)) || (string.IsNullOrWhiteSpace(Last.Text)) ||
                (string.IsNullOrWhiteSpace(Email.Text)) || (string.IsNullOrWhiteSpace(Password.Text)) ||
                (string.IsNullOrWhiteSpace(ConfirmPass.Text)) ||
                (string.IsNullOrEmpty(First.Text)) || (string.IsNullOrEmpty(Last.Text)) ||
                (string.IsNullOrEmpty(Email.Text)) || (string.IsNullOrEmpty(Password.Text)) ||
                (string.IsNullOrEmpty(ConfirmPass.Text)))
            {
                await DisplayAlert("Error", "Complete all fields", "OK");
            }
            else if (!Email.Text.Contains(eduSubstring))
            {
                await DisplayAlert("Error", "Invalid lsu.edu email", "OK");
            }
            else if (!string.Equals(Password.Text, ConfirmPass.Text))
            {
                warningLabel.Text = "Enter Same Password";
                Password.Text = string.Empty;
                ConfirmPass.Text = string.Empty;
                warningLabel.TextColor = Color.IndianRed;
                warningLabel.IsVisible = true;
            }
            else
            {
                User account = new User()
                {
                    First = First.Text,
                    Last = Last.Text,
                    Email = Email.Text,
                    Major = Major.SelectedItem.ToString(),
                    Password = Password.Text,
                    IsMentor = isMentor,
                };
                try
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                    var conn = new SQLiteConnection(dbPath);
                    conn.CreateTable<User>();
                    int rowsAdded = conn.Insert(account);
                    conn.Close();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }

                Navigation.PopModalAsync();
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (sender == StudentBox && isMentor)
            {
                Box.TranslateTo(Box.TranslationX - 100, Box.TranslationY);
                isMentor = false;
            }
            else if (sender == MentorBox && !isMentor)
            {
                Box.TranslateTo(Box.TranslationX + 100, Box.TranslationY);
                isMentor = true;
            }
        }
    }
}