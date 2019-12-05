using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorMeet.Models;
using MentorMeet.Users;
using Plugin.Media;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        bool editProfileToggle;

        public ProfilePage()
        {
            UIHelper helper = new UIHelper();

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            editProfileToggle = false;

            //Binding the fields in the profile to their respective fields in profile editor
            profileData.Name.SetBinding(Entry.TextProperty, new Binding("Text", source: nameLabel));
            profileData.Interests.SetBinding(Editor.TextProperty, new Binding("Text", source: areasOfInterest));
            profileData.Details.SetBinding(Editor.TextProperty, new Binding("Text", source: profileDetails));

            nameLabel.Text = CurrentUser.First + ' ' + CurrentUser.Last;
            contactInfo.Text = CurrentUser.Email;
            areasOfInterest.Text = CurrentUser.Interests;
            profileDetails.Text = CurrentUser.Details;
        }

        //Creates an autosized gap in the yellow line by the name
        async protected override void OnAppearing()
        {
            nameLabel.WidthRequest = -1;
            await Task.Delay(500);
            nameLabel.WidthRequest = nameLabel.Width + 40;
        }

    
    //Changes view from profile to edit profile
    async void EditProfileClicked(object sender, EventArgs e)
        {
            int moveAmount = 30;
            if (!editProfileToggle)
                GoToEdit();
            else
            {
                //Save profile confirmation
                string[] buttons = new string[3] { "Yes", "No", "Discard Changes" };
                string decision = await DisplayActionSheet("Are you sure that you want to save?", null, null, buttons);

                if (decision == "Yes")
                {
                    CurrentUser.First = nameLabel.Text.Split(' ')[0];
                    CurrentUser.Last = nameLabel.Text.Split(' ')[1];
                    CurrentUser.Details = profileDetails.Text;
                    CurrentUser.Interests = areasOfInterest.Text;
                    EditUserDetails();
                    ReturnToProfile();
                }

                else if (decision == "Discard Changes")
                    ReturnToProfile();
            }
        }

        async void ReturnToProfile()
        {
            detailsGrid.IsEnabled = true;
            detailsGrid.IsVisible = true;

            pictureFrame.ScaleTo(1, 500);
            pictureFrame.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY + 40, 500);
            profilePictureOverlay.ScaleTo(1, 500);
            profilePictureOverlay.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY + 40, 500);
            profilePictureOverlay.FadeTo(0);
            editProfile.Source = "ic_pencil_plus";
            editProfilePage.TranslateTo(editProfilePage.TranslationX, editProfilePage.TranslationY + 80, 500);
            editProfilePage.FadeTo(0, 500);
            await backgroundCardOverlay.FadeTo(0, 500);

            editProfilePage.IsEnabled = false;
            editProfilePage.IsVisible = false;

            editProfileToggle = false;

            nameLabel.WidthRequest = -1;
            await Task.Delay(250);
            nameLabel.WidthRequest = nameLabel.Width + 40;
        }

        async void GoToEdit()
        {
            editProfilePage.IsEnabled = true;
            editProfilePage.IsVisible = true;
            detailsGrid.IsEnabled = false;
            detailsGrid.IsVisible = false;
            editProfileToggle = true;
            editProfile.Source = "save";
            profilePictureOverlay.IsEnabled = true;

            pictureFrame.ScaleTo(.6, 350);
            pictureFrame.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY - 40, 350);
            profilePictureOverlay.ScaleTo(.6, 350);
            profilePictureOverlay.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY - 40, 350);
            profilePictureOverlay.FadeTo(.5);
            backgroundCardOverlay.FadeTo(.5, 350);
            editProfilePage.FadeTo(1, 350);
            editProfilePage.TranslateTo(editProfilePage.TranslationX, editProfilePage.TranslationY - 80, 350);
        }

        async void ChangePhotoClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {

            });

            if (file == null)
                return;

            profilePicture.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        #region Accessing User Database for Profile Page
        async void EditUserDetails()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                var conn = new SQLiteConnection(dbPath);
                var usersData = conn.Table<User>();
                var currentUserData = usersData.Where(x => x.Email == CurrentUser.Email).FirstOrDefault();

                currentUserData.First = CurrentUser.First; 
                currentUserData.Last = CurrentUser.Last; 
                currentUserData.Interests = CurrentUser.Interests; 
                currentUserData.Details = CurrentUser.Details;

                conn.InsertOrReplace(currentUserData);

                //await App.Database.UserSaveItemAsync(currentUserData);

                conn.Close();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
        #endregion
    }

}