using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorMeet.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                    nameLabel.Text = profileData.getName().Text;
                    contactInfo.Text = profileData.getContact().Text;
                    areasOfInterest.Text = profileData.getInterests().Text;
                    profileDetails.Text = profileData.getDetails().Text;

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
            pictureFrame.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY + 50, 500);
            editProfile.Source = "ic_pencil_plus";
            editProfilePage.TranslateTo(editProfilePage.TranslationX, editProfilePage.TranslationY + 100, 500);
            editProfilePage.FadeTo(0, 500);
            await backgroundCardOverlay.FadeTo(0, 500);

            editProfilePage.IsEnabled = false;
            editProfilePage.IsVisible = false;

            editProfileToggle = false;
        }

        async void GoToEdit()
        {
            editProfilePage.IsEnabled = true;
            editProfilePage.IsVisible = true;
            detailsGrid.IsEnabled = false;
            detailsGrid.IsVisible = false;
            editProfileToggle = true;
            editProfile.Source = "save";

            pictureFrame.ScaleTo(.5, 350);
            pictureFrame.TranslateTo(pictureFrame.TranslationX, pictureFrame.TranslationY - 50, 350);
            backgroundCardOverlay.FadeTo(.5, 350);
            editProfilePage.FadeTo(1, 350);
            editProfilePage.TranslateTo(editProfilePage.TranslationX, editProfilePage.TranslationY - 100, 350);
        }
    }

}