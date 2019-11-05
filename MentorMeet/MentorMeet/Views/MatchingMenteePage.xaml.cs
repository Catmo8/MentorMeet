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
        public MatchingMenteePage()
        { 
            InitializeComponent();
            var translateY = 40;
            var opacity = 0.05;
            BoxView[] boxViews = generateShadowBoxes(10, 26, 40);
            //Generates shadow for entire profile card
            for (int i = 0; i < 10; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.CornerRadius = 26;
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;
                tempBox.WidthRequest = 312;
                tempBox.HeightRequest = 402;
                tempBox.TranslationY = i;
                tempBox.TranslationX = (i) / 2;

                tempBox.Color = Color.FromHex("#000000");

                tempBox.Opacity = opacity;
                boxViews[i] = tempBox;
            }

            foreach (BoxView b in boxViews)
                matchScreen.Children.Add(b);

            //adds pic place holder...peach colored thing
            BoxView picPlaceHolder = new BoxView();
            picPlaceHolder.Color = Color.Bisque;
            picPlaceHolder.CornerRadius = 26;
            picPlaceHolder.WidthRequest = 310;
            picPlaceHolder.HeightRequest = 400;
            picPlaceHolder.HorizontalOptions = LayoutOptions.Center;
            picPlaceHolder.VerticalOptions = LayoutOptions.Center;
            matchScreen.Children.Add(picPlaceHolder);
            /*Function takes amount of boxes(smoothness of the gradient), corner radius, 
             *starting position on the y-axis(function autocenters each box so this the starting position with respect to the center. If 0, the box will be in the center of the UI)
             *and starting position of x-axis(same explaination as the Y-axis).
             *Both axis parameters are optional and will default to 0 (the center);
             */
           
            
            

            //Generates the boxviews for the shadow's gradient effect
            //This overwrites the returned array from "generateShadowBoxes" until I figure out why it's not working
            for (int i = 0; i < boxViews.Length; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.CornerRadius = 26;
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;
                tempBox.WidthRequest = 310;
                tempBox.HeightRequest = 295;
                tempBox.TranslationY = translateY;
                tempBox.Color = Color.FromHex("#000000");
                //Moves the next box down one position
                translateY++;
                tempBox.Opacity = opacity;
                boxViews[i] = tempBox;
            }

            //Adds each boxview shadow gradient to the UI
            foreach (BoxView b in boxViews)
                matchScreen.Children.Add(b);

            

            //Adds the white boxview to the UI that will hold the user info.
            BoxView backBox = new BoxView();
            backBox.Color = Color.White;
            backBox.CornerRadius = 23;
            backBox.HorizontalOptions = LayoutOptions.Center;
            backBox.VerticalOptions = LayoutOptions.Center;
            backBox.TranslationY = 50;
            backBox.HeightRequest = 300;
            backBox.WidthRequest = 310;
            matchScreen.Children.Add(backBox);
            
        }

        public BoxView[] generateShadowBoxes(int numOfBoxes, int cornerRadius, int startY = 0, int startX = 0)
        {
            
            if(numOfBoxes > 15)
            {
                numOfBoxes = 15;
            }

            BoxView[] boxViews = new BoxView[numOfBoxes];

            int opacity = 1/numOfBoxes; //to gradually and equally increase the opacity as the boxviews overlap.
            //Generates the boxviews for the shadow's gradient effect
            for (int i = 0; i < numOfBoxes; i++)
            {
                boxViews[i] = new BoxView();
                boxViews[i].CornerRadius = cornerRadius;
                boxViews[i].HorizontalOptions = LayoutOptions.Center;
                boxViews[i].VerticalOptions = LayoutOptions.Center;
                boxViews[i].WidthRequest = 310;
                boxViews[i].HeightRequest = 295;
                boxViews[i].TranslationY = startY;
                boxViews[i].Color = Color.FromHex("#000000");
                //Moves the next box down one position
                startY++;
                boxViews[i].Opacity = opacity;
            }
            
            return boxViews;
        }

        //Handles the swipe gestures and their respective animations
        async void OnSwiped(object sender, SwipedEventArgs e)
        {
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

        async void OnTapped(object sender)
        {
            await (sender as Grid).TranslateTo(0, 0);
        }
    }
}