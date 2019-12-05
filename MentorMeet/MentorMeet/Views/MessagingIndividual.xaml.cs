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
            new Message("William Duncan", new DateTime(2019, 12, 1, 16, 12, 15), "", false),
            new Message(CurrentUser.First + " " + CurrentUser.Last, new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum ", true),
            new Message(CurrentUser.First + " " + CurrentUser.Last, new DateTime(2019, 12, 1, 16, 12, 15), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", true),
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


            // Get database entries where 
            // Search (fromUser == currentUser AND toUser == currentlySelectedUser) OR (fromUser == currentlySeleectedUser AND toUser == currentUser)  SORT BY time DESC

            // for each entry
            // Append to messages


            messages = new ObservableCollection<Message>()
            {
            new Message(CurrentUser.First + " " + CurrentUser.Last, new DateTime(2019, 12, 1, 16, 12, 15), "Hello, I hope you're enjoying your day. I wanted to ask when would you be free to meet?", true),
            new Message(individual.name, new DateTime(2019, 12, 1, 16, 12, 15), "My office hours today are 1-3 P.M.", false),
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

        void Send_Tapped(object sender, System.EventArgs args)
        {
            messages.Add(new Message(CurrentUser.First + " " + CurrentUser.Last, new DateTime(2019, 12, 1, 16, 12, 15), chatTextInput.Text, true));

            chatTextInput.Text = "";
        }
    }
}