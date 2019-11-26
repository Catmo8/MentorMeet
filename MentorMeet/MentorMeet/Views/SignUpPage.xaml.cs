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
        public SignUpPage()
        {
            InitializeComponent();
        }

        async private void Register_Clicked(object sender, EventArgs e)
        {
            User account = new User()
            {
                email = Email.Text,
                password = Password.Text,
                confirmPass = ConfirmPass.Text,
                first = First.Text,
                last = Last.Text,
                
             };

            using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3")))
            {
                conn.CreateTable<User>();
                int rowsAdded = conn.Insert(account);
            }

            Navigation.PopModalAsync();
        }
    }
}