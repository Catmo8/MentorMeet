﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Users;

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
        private ScrollView scrollView;
        private Label name;
        private Label details;
        private Professor[] professors = new Professor[3];
        private Image profilePic;
        private int currentProfessor;

        public MatchingMenteePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            currentProfessor = 0;
            professors[0] = new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory");
            professors[1] = new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis");
            professors[2] = new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains");

            int backgroundCardHeight = 500;
            int profileDetailsHeight = backgroundCardHeight - 100;
            int cardWidth = 360;
            int profileDetailsYStart = 50;
            int profileCircleSize = 100;
            int profileCircleYStart = profileDetailsYStart - 200;

            //for keeping track of the profileDetailsCard state
            tapped = false;
            double opacity = 0.05;
            
            /*generateShadowBoxes(numOfBoxes, cornerRadius, width, height, yStart, moveX)
             * 
             *generateShadowBoxes function takes amount of boxviews(smoothness of the gradient), corner radius, width, height,
             *starting position on the y-axis(function autocenters each box so this the starting position with respect to the center.
             *If 0, the box will be in the center of the UI) and whether or not the x axis needs to be adjusted per box like the y axis(for a perspective effect).
             *Both Y(int) and X(bool) parameters are optional and will default to 0 (the center in this case) and false respectively;
             */
            //cardShadow = generateShadowBoxes(10, 26, cardWidth+2, backgroundCardHeight+2, 0, true); //Generates shadow for entire profile card
            detailCardShadow = generateShadowBoxes(10, 26, cardWidth+2, profileDetailsHeight+2, profileDetailsYStart, true); //Generates shadow for the profile details
            profilePictureShadow = generateShadowBoxes(10, profileCircleSize / 2, profileCircleSize, profileCircleSize, profileCircleYStart, true);
            

            //Creates the card behind the profileDetails card
            BoxView backgroundCard = new BoxView();
            backgroundCard.Color = Color.Gray;
            backgroundCard.CornerRadius = 26;
            backgroundCard.WidthRequest = cardWidth;
            backgroundCard.HeightRequest = backgroundCardHeight;
            backgroundCard.HorizontalOptions = LayoutOptions.Center;
            backgroundCard.VerticalOptions = LayoutOptions.Center;

            /*organizationLogo = new Image();
            organizationLogo.Source = "LSU.png";
            organizationLogo.WidthRequest = 290;
            organizationLogo.HeightRequest = 100;
            organizationLogo.HorizontalOptions = LayoutOptions.Center;
            organizationLogo.VerticalOptions = LayoutOptions.Center;*/
            organizationLogo.TranslationY = profileCircleYStart - 100 ;
            
            

            //Creates the white boxview that will hold the profile details.
            profileDetailsBox = new BoxView();
            profileDetailsBox.Color = Color.White;
            profileDetailsBox.CornerRadius = 23;
            profileDetailsBox.HorizontalOptions = LayoutOptions.Center;
            profileDetailsBox.VerticalOptions = LayoutOptions.Center;
            profileDetailsBox.TranslationY = profileDetailsYStart;
            profileDetailsBox.HeightRequest = profileDetailsHeight;
            profileDetailsBox.WidthRequest = cardWidth;
            


            profileCircle = new BoxView();
            profileCircle.HorizontalOptions = LayoutOptions.Center;
            profileCircle.VerticalOptions = LayoutOptions.Center;
            profileCircle.WidthRequest = profileCircleSize;
            profileCircle.HeightRequest = profileCircle.WidthRequest;
            profileCircle.CornerRadius = profileCircle.WidthRequest/2;
            profileCircle.Color = Color.Gold;
            profileCircle.TranslationY = profileCircleYStart;


            name = new Label();
            details = new Label();
            name.Text = professors[0].name;
            name.HorizontalOptions = LayoutOptions.Center;
            name.TranslationY = profileDetailsYStart + 200;
            name.FontSize = 30;

            details.Text = professors[0].details;
            details.HorizontalTextAlignment = TextAlignment.Center;
            details.FontSize = 16;

            scrollView = new ScrollView();
            scrollView.HorizontalOptions = LayoutOptions.Center;
            scrollView.VerticalOptions = LayoutOptions.Center;
            scrollView.TranslationY = name.TranslationY - 150;
            scrollView.WidthRequest = cardWidth - 5;
            scrollView.HeightRequest = profileDetailsHeight - 100;
            scrollView.Content = details;

            profilePic = new Image();

            nextProfessor();

            profilePic.HorizontalOptions = LayoutOptions.Center;
            profilePic.VerticalOptions = LayoutOptions.Center;

            Frame frame = new Frame();
            frame.HorizontalOptions = LayoutOptions.Center;
            frame.VerticalOptions = LayoutOptions.Center;
            frame.WidthRequest = profileCircleSize - 5;
            frame.HeightRequest = frame.WidthRequest;
            frame.CornerRadius = (float)frame.WidthRequest / 2;
            frame.Padding = 0;
            frame.IsClippedToBounds = true;
            frame.Content = profilePic;
            frame.TranslationY = profileCircle.TranslationY;
            frame.BackgroundColor = Color.Transparent;

            
            uniLogo.BackgroundColor = Color.Purple;
            //Adds all created BoxViews to the UI
            /*foreach (BoxView b in cardShadow)
                matchScreen.Children.Add(b);*/
            
            //matchScreen.Children.Add(backgroundCard);

            foreach(BoxView b in detailCardShadow)
                matchScreen.Children.Add(b);

            

            matchScreen.Children.Add(profileDetailsBox);

            foreach (BoxView b in profilePictureShadow)
            {
                matchScreen.Children.Add(b);
            }
            matchScreen.Children.Add(profileCircle);

            matchScreen.Children.Add(name);
            matchScreen.Children.Add(scrollView);
            //matchScreen.Children.Add(profilePic);
            matchScreen.Children.Add(frame);


        }

        public BoxView[] generateShadowBoxes(int numOfBoxes, int cornerRadius, int width, int height, int yStart = 0, bool moveX = false)
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
                tempBox.TranslationY = i + yStart;
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
                nextProfessor();
                await matchScreen.TranslateTo(matchScreen.TranslationX - 500, matchScreen.TranslationY - 100);
                
            }
            else if (e.Direction == SwipeDirection.Right)
            {
                matchScreen.RotateTo(25);
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY + 100, 125);
                await matchScreen.RotateTo(-25);
                matchScreen.RotateTo(0);
                matchScreen.TranslationX = -500;
                nextProfessor();
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY - 100);
                
            }
            

        }

        async void OnTapped(object sender, EventArgs args)
        {
            return;
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

        void nextProfessor()
        {
            while (true)
            {
                if (currentProfessor < professors.Length)
                {
                    name.Text = professors[currentProfessor].name;
                    details.Text = professors[currentProfessor].details;
                    profilePic.Source = professors[currentProfessor].picture;
                    var prof = profilePic;

                    string caseSwitch = professors[currentProfessor].university;
                    switch (caseSwitch)
                    {
                        case "LSU":
                            organizationLogo.Source = "LSU.png";
                            break;

                        case "SELU":
                            organizationLogo.Source = "SELU.jpg";
                            break;

                        default:
                            organizationLogo.IsVisible = false;
                            break;
                    }
                    currentProfessor++;
                    break;
                }
                else
                {
                    currentProfessor = 0;
                }
            }
        }
    }
}