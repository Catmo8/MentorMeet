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
        BoxView[] shadow, pictureShadow;
        BoxView detailsCard, detailsCardOverlay;
        ScrollView scrollView;
        StackLayout stackLayout;
        Image profileImage;
        Frame profilePictureFrame, detailsEditorFrame, profilePictureOverlay;
        Label detailsLabel, editPictureLabel, emailLabel;
        Editor detailsEditor;
        bool editProfileToggle;

        public ProfilePage()
        {
            UIHelper helper = new UIHelper();

            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            editProfileToggle = false;

        //Creating UI elements
            detailsCard = new BoxView
            {
                BackgroundColor = Color.White,
                CornerRadius = 25,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 500,
                HeightRequest = 750,
                TranslationY = 100
            };

            profileImage = new Image{ Source = "Anas_Mahmoud" };

            profilePictureFrame = new Frame
            {
                CornerRadius = 20,
                Padding = 0,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TranslationY = -250,
                WidthRequest = 150,
                Content = profileImage
            };

            editPictureLabel = new Label 
            { 
                Text = "Change photo",
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.Black
            };

            profilePictureOverlay = new Frame
            {
                CornerRadius = profilePictureFrame.CornerRadius,
                Padding = 0,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TranslationY = profilePictureFrame.TranslationY,
                WidthRequest = profilePictureFrame.WidthRequest,
                HeightRequest = profilePictureFrame.Height,
                Content = editPictureLabel,
                BackgroundColor = Color.DarkGray,
                Opacity = 0
            };

            detailsLabel = new Label { Text = "Testing this piece"};
            emailLabel = new Label { Text = "E-mail: illendyou@lsu.edu" };
            stackLayout = new StackLayout
            {
                Children = { detailsLabel }
            };
            scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = detailsCard.WidthRequest - 110,
                HeightRequest = detailsCard.HeightRequest - 300,
                Content = stackLayout,
                TranslationY = detailsCard.TranslationY + 75
            };

            detailsEditor = new Editor
            {
                BackgroundColor = Color.White,
                Text = detailsLabel.Text,
            };

            detailsEditorFrame = new Frame
            {
                Padding = 0,
                CornerRadius = 20,
                Content = detailsEditor,
                IsClippedToBounds = true,
            };

            shadow = helper.GenerateShadowBoxes(10, 20, detailsCard.WidthRequest, detailsCard.HeightRequest, detailsCard.TranslationY - 7);
            pictureShadow = helper.GenerateShadowBoxes(true, 10, profilePictureFrame.CornerRadius, profilePictureFrame.WidthRequest - 1, profilePictureFrame.WidthRequest, profilePictureFrame.TranslationY - 1);
            detailsCardOverlay = helper.GenerateShadowBoxes(1, 20, detailsCard.WidthRequest, detailsCard.HeightRequest, detailsCard.TranslationY)[0];
            detailsCardOverlay.Opacity = 0;

            //Adding created elements to layout
            foreach (BoxView b in shadow)
                profilePageGrid.Children.Add(b);

            profilePageGrid.Children.Add(detailsCard);
            profilePageGrid.Children.Add(detailsCardOverlay);

            foreach (BoxView b in pictureShadow)
                profilePageGrid.Children.Add(b);

            profilePageGrid.Children.Add(scrollView);
            profilePageGrid.Children.Add(profilePictureFrame);
            profilePageGrid.Children.Add(profilePictureOverlay);

            
        }

        //Changes view from profile to edit profile
        async void EditProfileClicked(object sender, EventArgs e)
        {
            int moveAmount = 30;
            if (editProfileToggle)
            {
                detailsLabel.FormattedText = detailsEditor.Text;
                scrollView.Content = stackLayout;
                profilePictureFrame.TranslateTo(profilePictureFrame.TranslationX, profilePictureFrame.TranslationY + moveAmount);
                profilePictureFrame.ScaleTo(1);

                profilePictureOverlay.TranslateTo(profilePictureFrame.TranslationX, profilePictureFrame.TranslationY + moveAmount);
                profilePictureOverlay.ScaleTo(1);
                profilePictureOverlay.FadeTo(0);

                scrollView.TranslateTo(scrollView.TranslationX, scrollView.TranslationY + moveAmount + 20);

                detailsCardOverlay.TranslateTo(detailsCardOverlay.TranslationX, detailsCardOverlay.TranslationY + moveAmount);
                detailsCardOverlay.FadeTo(0);

                detailsLabel.FadeTo(1);

                foreach (BoxView b in pictureShadow)
                {
                    b.TranslateTo(b.TranslationX, b.TranslationY + moveAmount);
                    b.ScaleTo(1);
                    b.FadeTo(0.1);
                }
                await detailsCard.TranslateTo(detailsCard.TranslationX, detailsCard.TranslationY + moveAmount);
                editProfileToggle = false;
                editProfile.Source = "ic_pencil_plus";
            }

            else
            {
                scrollView.Content = detailsEditorFrame;
                profilePictureFrame.TranslateTo(profilePictureFrame.TranslationX, profilePictureFrame.TranslationY - moveAmount);
                //profilePictureFrame.FadeTo(0);
                profilePictureFrame.ScaleTo(.75);

                profilePictureOverlay.TranslateTo(profilePictureFrame.TranslationX, profilePictureFrame.TranslationY - moveAmount);
                profilePictureOverlay.ScaleTo(.75);
                profilePictureOverlay.FadeTo(.7);

                scrollView.TranslateTo(scrollView.TranslationX, scrollView.TranslationY - moveAmount - 20);
                detailsCardOverlay.TranslateTo(detailsCardOverlay.TranslationX, detailsCardOverlay.TranslationY - moveAmount);
                
                detailsLabel.FadeTo(0);
                detailsCardOverlay.FadeTo(0.3);
                foreach (BoxView b in pictureShadow)
                {
                    b.TranslateTo(b.TranslationX, b.TranslationY - moveAmount);
                    b.ScaleTo(.75);
                    b.FadeTo(.01);
                }
                detailsLabel.FadeTo(0);
                //scrollView.TranslateTo(scrollView.TranslationX, scrollView.TranslationY - moveAmount - 50);
                await detailsCard.TranslateTo(detailsCard.TranslationX, detailsCard.TranslationY - moveAmount);
                editProfileToggle = true;
                editProfile.Source = "Profile_Check";
            }
            
        }

        /*async void profileDetailsFade(string inOut)
        {
            double opacity, shadowOpacity;
            int moveAmount = 30, scrollMoveAmount = 50;
            if (inOut == "in")
            {
                opacity = 1;
                shadowOpacity = 0.1;
                moveAmount = -moveAmount;
            }

            profilePictureFrame.TranslateTo(profilePictureFrame.TranslationX, profilePictureFrame.TranslationY + moveAmount);
            profilePictureFrame.FadeTo(1);
            foreach (BoxView b in pictureShadow)
            {
                b.TranslateTo(b.TranslationX, b.TranslationY + moveAmount);
                b.FadeTo(0.1);
            }
            detailsLabel.FadeTo(1);
            scrollView.TranslateTo(scrollView.TranslationX, scrollView.TranslationY + moveAmount + 50);
            await detailsCard.TranslateTo(detailsCard.TranslationX, detailsCard.TranslationY + moveAmount);
            editProfileToggle = false;
            editProfile.Source = "ic_pencil_plus";
        }*/
    }
}