using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchingMenteePage : ContentPage
    {
        private bool tapped; //Keeps track of the tapped state to control which animations (expand/shrink) get executed on the profile card
        private BoxView profileDetailsBox;
        private BoxView[] cardShadow;
        private BoxView[] detailCardShadow;
        private BoxView[] profilePictureShadow;
        private BoxView profileCircle;
        private BoxView blankBox;
        public MatchingMenteePage()
        { 
            InitializeComponent();

            tapped = false;
            var opacity = 0.05;
            var profileCircleSize = 100;

            /*Function takes amount of boxviews(smoothness of the gradient), corner radius, width, height,
             *starting position on the y-axis(function autocenters each box so this the starting position with respect to the center.
             *If 0, the box will be in the center of the UI) and whether or not the x axis needs to be adjusted per box like the y axis(for a perspective effect).
             *Both Y(int) and X(bool) parameters are optional and will default to 0 (the center in this case) and false respectively;
             */
            cardShadow = generateShadowBoxes(10, 26, 312, 402, 0, true); //Generates shadow for entire profile card
            detailCardShadow = generateShadowBoxes(10, 26, 310, 295, 40); //Generates shadow for the profile details

            //Generates shadow for the profile picture circle
            profilePictureShadow = new BoxView[10];
            var profilePictureShadowSize = profileCircleSize + 4;
            for (int i = 0; i < profilePictureShadow.Length; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;
                tempBox.WidthRequest = profilePictureShadowSize + i;
                tempBox.HeightRequest = tempBox.WidthRequest;
                tempBox.CornerRadius = tempBox.WidthRequest / 2;
                tempBox.Color = Color.FromHex("#000000");
                tempBox.Opacity = opacity;
                tempBox.TranslationY = -100;
                profilePictureShadow[i] = tempBox;
            }

            //Creates pic place holder...peach colored thing
            BoxView picPlaceHolder = new BoxView();
            picPlaceHolder.Color = Color.Bisque;
            picPlaceHolder.CornerRadius = 26;
            picPlaceHolder.WidthRequest = 310;
            picPlaceHolder.HeightRequest = 400;
            picPlaceHolder.HorizontalOptions = LayoutOptions.Center;
            picPlaceHolder.VerticalOptions = LayoutOptions.Center;

            Image organizationLogo = new Image();
            organizationLogo.Source = "LSU.png";
            organizationLogo.WidthRequest = 290;
            organizationLogo.HeightRequest = 100;
            organizationLogo.HorizontalOptions = LayoutOptions.Center;
            organizationLogo.VerticalOptions = LayoutOptions.Center;
            organizationLogo.TranslationY = organizationLogo.TranslationY - 140;
            
            

            //Creates the white boxview to the UI that will hold the profile details.
            profileDetailsBox = new BoxView();
            profileDetailsBox.Color = Color.White;
            profileDetailsBox.CornerRadius = 23;
            profileDetailsBox.HorizontalOptions = LayoutOptions.Center;
            profileDetailsBox.VerticalOptions = LayoutOptions.Center;
            profileDetailsBox.TranslationY = 50;
            profileDetailsBox.HeightRequest = 300;
            profileDetailsBox.WidthRequest = 310;
              
            profileCircle = new BoxView();
            
            profileCircle.HorizontalOptions = LayoutOptions.Center;
            profileCircle.VerticalOptions = LayoutOptions.Center;
            profileCircle.WidthRequest = profileCircleSize;
            profileCircle.HeightRequest = profileCircle.WidthRequest;
            profileCircle.CornerRadius = profileCircle.WidthRequest/2;
            profileCircle.Color = Color.LightGray;
            profileCircle.TranslationY = -100;

            //Adds all created BoxViews to the UI
            foreach (BoxView b in cardShadow)
                matchScreen.Children.Add(b);

            matchScreen.Children.Add(picPlaceHolder);
            matchScreen.Children.Add(organizationLogo);

            foreach(BoxView b in detailCardShadow)
                matchScreen.Children.Add(b);

            foreach (BoxView b in profilePictureShadow)
            {
                matchScreen.Children.Add(b);
            }

            matchScreen.Children.Add(profileDetailsBox);
            matchScreen.Children.Add(profileCircle);
            
            
        }

        public BoxView[] generateShadowBoxes(int numOfBoxes, int cornerRadius, int width, int height, int startY = 0, bool moveX = false)
        {
            
            if(numOfBoxes > 15)
            {
                numOfBoxes = 15;
            }

            BoxView[] boxViews = new BoxView[numOfBoxes];

            double opacity = 1/(double)numOfBoxes; //to gradually and equally increase the opacity as the cardShadow overlap.
            //Generates the cardShadow for the shadow's gradient effect

            for (int i = 0; i < numOfBoxes; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.CornerRadius = cornerRadius;
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;
                tempBox.WidthRequest = width;
                tempBox.HeightRequest = height;
                tempBox.TranslationY = i + startY;
                if(moveX)
                    tempBox.TranslationX = (i) / 2;

                tempBox.Color = Color.FromHex("#000000");

                tempBox.Opacity = opacity;
                boxViews[i] = tempBox;
            }
            
            return boxViews;
        }

        //Handles the swipe gestures and their respective animations.
        //Resets the UI objects to their original positions and then recycles the card.
        async void OnSwiped(object sender, SwipedEventArgs e)
        {
            if (tapped)
                resetProfileCard();

            if (e.Direction == SwipeDirection.Left)
            {
                matchScreen.RotateTo(-25);
                await matchScreen.TranslateTo(matchScreen.TranslationX - 500, matchScreen.TranslationY + 100, 125);
                await matchScreen.RotateTo(25);
                matchScreen.RotateTo(0);
                matchScreen.TranslationX = 500;
                await matchScreen.TranslateTo(matchScreen.TranslationX - 500, matchScreen.TranslationY - 100);
            }
            else if (e.Direction == SwipeDirection.Right)
            {
                matchScreen.RotateTo(25);
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY + 100, 125);
                await matchScreen.RotateTo(-25);
                matchScreen.RotateTo(0);
                matchScreen.TranslationX = -500;
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY - 100);
            }
        }

        async void OnTapped(object sender, EventArgs args)
        {
            //If the card has been tapped already, reset all views to their original states
            if (tapped)
                resetProfileCard();
            else 
            {
                profileDetailsBox.ScaleTo(1.2);
                profileCircle.TranslateTo(0, profileCircle.TranslationY - 50);
                foreach (BoxView b in detailCardShadow)
                {
                    b.ScaleTo(1.25);
                    b.TranslateTo(0, b.TranslationY + 5);
                }
                foreach (BoxView b in profilePictureShadow)
                {
                    b.TranslateTo(0, b.TranslationY - 50);
                    b.ScaleTo(1.05);
                }
                tapped = true;
            }
        }

        void resetProfileCard()
        {
            foreach (object o in matchScreen.Children)
            {
                BoxView b = new BoxView();
                if (o.GetType() == b.GetType())
                {
                    b = o as BoxView;
                    resetScaling(b);
                }
            }

            foreach (BoxView b in profilePictureShadow)
                b.TranslateTo(0, b.TranslationY + 50);

            profileCircle.TranslateTo(0, profileCircle.TranslationY + 50);

            foreach (BoxView b in detailCardShadow)
                b.TranslateTo(0, b.TranslationY - 5);


            tapped = false;
        }

        void resetScaling(BoxView box)
        {
            box.ScaleTo(1);
        }

        //to handle the shadows which are arrays
        void resetScaling(BoxView[] box)
        {
            foreach (BoxView b in box)
                b.ScaleTo(1);
        }
    }
}