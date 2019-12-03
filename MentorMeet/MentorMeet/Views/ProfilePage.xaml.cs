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
        BoxView detailsCard, detailsCardOverlay, horizontalLine, verticalLine, horizontalLine2;
        ScrollView scrollView;
        StackLayout profileStack, editStack;
        Image profileImage, lsu;
        Frame profilePictureFrame, detailsEditorFrame, profilePictureOverlay, circle;
        Label detailsLabel, editPictureLabel, emailLabel, email, name;
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
            lsu = new Image {
                Source = "eye",
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.End,
                TranslationX = -30,
                TranslationY = -110

            };

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

            name = new Label
            {
                Text = "Anas Mahmoud",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 26,
                TranslationY = 10,
            };

            detailsLabel = new Label
            {
                Text = "Testing this piece",
                TranslationY = 100,
            };
            emailLabel = new Label
            {
                Text = "E-mail",
                TranslationY = 25,
                TranslationX = 60,
                FontSize = 16,
                TextDecorations = TextDecorations.Underline,
                FontAttributes = FontAttributes.Bold
            };

            email = new Label
            {
                Text = "somthing@lsu.edu",
                TranslationY = emailLabel.TranslationY,
                TranslationX = emailLabel.TranslationX -30
            };


            profileStack = new StackLayout();
            editStack = new StackLayout();

            scrollView = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = detailsCard.WidthRequest - 110,
                HeightRequest = detailsCard.HeightRequest - 500,
                Content = profileStack,
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

            horizontalLine = new BoxView
            {
                HeightRequest = 1,
                CornerRadius = 1,
                WidthRequest = scrollView.WidthRequest,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Color = Color.Gold,
                TranslationY = -110
            };

            horizontalLine2 = new BoxView
            {
                HeightRequest = 1,
                CornerRadius = 1,
                WidthRequest = scrollView.WidthRequest,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                TranslationY = -300,
                Color = Color.Gold,
            };

            verticalLine = new BoxView
            {
                HeightRequest = 70,
                CornerRadius = 1,
                WidthRequest = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                Color = Color.Gold,
                TranslationY = -35
            };

            circle = new Frame
            {
                HeightRequest = 20,
                CornerRadius = 10,
                WidthRequest = 20,
                BackgroundColor = Color.White,
                BorderColor = Color.Gold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,
                TranslationY = horizontalLine.TranslationY - 15,
                Padding = 0,
                HasShadow = false
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

            profileStack.Children.Add(name);
            profileStack.Children.Add(emailLabel);
            profileStack.Children.Add(email);
            profileStack.Children.Add(verticalLine);
            profileStack.Children.Add(lsu);
            profileStack.Children.Add(horizontalLine);
            profileStack.Children.Add(circle);
            profileStack.Children.Add(detailsLabel);
            profileStack.Children.Add(horizontalLine2);

        }

        //Changes view from profile to edit profile
        async void EditProfileClicked(object sender, EventArgs e)
        {
            int moveAmount = 30;
            if (editProfileToggle)
            {
                detailsLabel.FormattedText = detailsEditor.Text;
                scrollView.Content = profileStack;
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