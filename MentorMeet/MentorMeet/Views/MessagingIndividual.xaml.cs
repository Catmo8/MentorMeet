using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MentorMeet.Users;
using SQLite;
using System.IO;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagingIndividual : ContentPage
    {
        public ObservableCollection<Message> messages = new ObservableCollection<Message>()
        {
            new Message("William Duncan", new DateTime(2019, 12, 1, 16, 12, 15), "Hello, World!", false),
            new Message("Seth Williamson", new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum ", true),
            new Message("Seth Williamson", new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", true),
            new Message("William Duncan", new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", false)
        };
        
        public MessagingIndividual()
        {
            InitializeComponent();
            MessageList.ItemsSource = messages;
        }

        public MessagingIndividual(Professor individual)
        {
            InitializeComponent();
            IndividualTitleImage.Source = individual.picture;
            IndividualTitleName.Text = individual.name;
            IndividualTitleDetails.Text = individual.details;

            messages = new ObservableCollection<Message>()
            {
            new Message(individual.name, new DateTime(2019, 12, 1, 16, 12, 15), "Hello, World!", false),
            new Message("Seth Williamson", new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum ", true),
            new Message("Seth Williamson", new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", true),
            new Message(individual.name, new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", false)
            };

            MessageList.ItemsSource = messages;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            // Close the page
            Navigation.PopModalAsync();
        }

        //public ICommand ItemClickCommand
        //{
        //    get
        //    {
              // Append Date / Time to the stack layout
              // Or remove if it is already there
        //   }
        //}

        async void Send_Tapped(object sender, System.EventArgs args)
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                var conn = new SQLiteConnection(dbPath);
                var UserData = conn.Table<User>();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }

            messages.Add(new Message("Seth Williamson", new DateTime(2019, 12, 1, 16, 12, 15), chatTextInput.Text, true));

            chatTextInput.Text = "";
        }
    }
}