using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Users;
using MentorMeet.Models;
using SQLite;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchingMenteePage : ContentPage
    {
        private bool tapped; //Keeps track of the tapped state to control which animations (expand/shrink) get executed on the profile card
        private BoxView profileDetailsBox;
        private BoxView[] detailCardShadow;
        private BoxView[] profilePictureShadow;
        private BoxView profileCircle;
        private ScrollView scrollView;
        private Label name;
        private Label details;
        private List<Professor> professors = new List<Professor>();
        private Image profilePic, checkMark, declineX;
        private int currentProfessor;

        public MatchingMenteePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            UIHelper helper = new UIHelper();
            currentProfessor = 0;
            professors.Add(new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"));
            professors.Add(new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"));
            professors.Add(new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains"));

            // For adding professors:

            // bool searchingMentors = true

            // if currentUser.isMentor:
            // searchingMentors = false

            // Query where (of all usernames) is not in swipedOnUser in incompletedMatches table

            // Add each to professors
            AddProfessor();

            int backgroundCardHeight = 500;
            int profileDetailsHeight = backgroundCardHeight - 100;
            int cardWidth = 360;
            int profileDetailsYStart = -50;
            int profileCircleSize = 100;
            int profileCircleYStart = profileDetailsYStart - 200;

            //for keeping track of the profileDetailsCard state
            tapped = false;
            double opacity = 0.05;
            
            /*GenerateShadowBoxes(numOfBoxes, cornerRadius, width, height, yStart, moveX)
             * 
             *GenerateShadowBoxes function takes amount of boxviews(smoothness of the gradient), corner radius, width, height,
             *starting position on the y-axis(function autocenters each box so this the starting position with respect to the center.
             *If 0, the box will be in the center of the UI) and whether or not the x axis needs to be adjusted per box like the y axis(for a perspective effect).
             *Both Y(int) and X(bool) parameters are optional and will default to 0 (the center in this case) and false respectively;
             */
            //cardShadow = GenerateShadowBoxes(10, 26, cardWidth+2, backgroundCardHeight+2, 0, true); //Generates shadow for entire profile card
            detailCardShadow = helper.GenerateShadowBoxes(20, 26, cardWidth+2, profileDetailsHeight+2, profileDetailsYStart, true); //Generates shadow for the profile details
            profilePictureShadow = helper.GenerateShadowBoxes(10, profileCircleSize / 2, profileCircleSize, profileCircleSize, profileCircleYStart, true);

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
            scrollView.WidthRequest = cardWidth - 75;
            scrollView.HeightRequest = profileDetailsHeight - 100;
            scrollView.Content = details;

            profilePic = new Image();
            nextProfessor();

            
            profilePic.HorizontalOptions = LayoutOptions.Center;
            profilePic.VerticalOptions = LayoutOptions.Center;

            Frame frame = new Frame();
            frame.HorizontalOptions = LayoutOptions.Center;
            frame.VerticalOptions = LayoutOptions.Center;
            frame.WidthRequest = profileCircleSize - 6;
            frame.HeightRequest = frame.WidthRequest;
            frame.CornerRadius = (float)frame.WidthRequest / 2;
            frame.Padding = 0;
            frame.IsClippedToBounds = true;
            frame.Content = profilePic;
            frame.TranslationY = profileCircle.TranslationY;
            frame.BackgroundColor = Color.Transparent;

            uniLogo.BackgroundColor = Color.FromHex("#FF64289A");
            //Adds all created BoxViews to the UI
            /*foreach (BoxView b in cardShadow)
                matchScreen.Children.Add(b);*/

            //matchScreen.Children.Add(backgroundCard);

            foreach (BoxView b in detailCardShadow)
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

            checkMark = new Image();
            checkMark.Source = "check.png";
            checkMark.HeightRequest = 100;
            checkMark.WidthRequest = 100;
            checkMark.HorizontalOptions = LayoutOptions.Center;
            checkMark.VerticalOptions = LayoutOptions.Center;
            checkMark.Scale = .1;
            checkMark.Opacity = 0;

            declineX = new Image();
            declineX.Source = "x.png";
            declineX.HorizontalOptions = LayoutOptions.Center;
            declineX.VerticalOptions = LayoutOptions.Center;
            declineX.WidthRequest = 100;
            declineX.HeightRequest = 100;
            declineX.Scale = checkMark.Scale;
            declineX.Opacity = checkMark.Opacity;

            uniLogo.Children.Add(checkMark);
            uniLogo.Children.Add(declineX);

            AnimateSwipeArrows();

        }

        //Handles the swipe gestures and their respective animations.
        //Resets the UI objects to their original positions and then recycles the card.
        async void OnSwiped(object sender, SwipedEventArgs e)
        {
            // if swiped left:
                // add entry [swipingUser, swipedOnUser, swipedRight = false]

            // if swiped right:
                // add entry [swipingUser, swipedUser, swipedRight = true]
                // if [swipingUser = swipedUser, swipedUser = currentUser, swipedRight = true] is in table:
                    // New match
                    // add entry(ies) into matches
                    // add entry [id?, swipedUser, swipingUser]
                    // add entry [id?, swipingUser, swipedUser]

                    // Notify other user ?


            if (tapped)
                resetProfileCard();

            if (e.Direction == SwipeDirection.Left)
            {
                matchScreen.RotateTo(-25);
                await matchScreen.TranslateTo(matchScreen.TranslationX - 500, matchScreen.TranslationY + 100, 125);
                await matchScreen.RotateTo(25);
                nextProfessor();

                declineX.FadeTo(1,100);
                declineX.ScaleTo(1,100);
                await declineX.RotateTo(360,100);

                await Task.Delay(500);

                declineX.FadeTo(0);
                await declineX.ScaleTo(.1);
                declineX.RotateTo(0);

                matchScreen.RotateTo(0);
                matchScreen.TranslationX = 500;

                await matchScreen.TranslateTo(matchScreen.TranslationX - 500, matchScreen.TranslationY - 100);
                
            }
            else if (e.Direction == SwipeDirection.Right)
            {
                matchScreen.RotateTo(25);
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY + 100, 125);
                await matchScreen.RotateTo(-25);
                nextProfessor();

                checkMark.FadeTo(1);
                checkMark.ScaleTo(1);
                await checkMark.RotateTo(360);

                await Task.Delay(750);

                checkMark.ScaleTo(.1);
                checkMark.RotateTo(0);
                await checkMark.FadeTo(0);
                
                matchScreen.RotateTo(0);
                matchScreen.TranslationX = -500;
                await matchScreen.TranslateTo(matchScreen.TranslationX + 500, matchScreen.TranslationY - 100);
                
                
            }
            

        }

        async void AnimateSwipeArrows()
        {
            while (true)
            {
                leftArrow.TranslateTo(leftArrow.TranslationX - 10, leftArrow.TranslationY, 500);
                await rightArrow.TranslateTo(rightArrow.TranslationX + 10, rightArrow.TranslationY, 500);

                leftArrow.TranslateTo(leftArrow.TranslationX + 10, leftArrow.TranslationY, 500);
                await rightArrow.TranslateTo(rightArrow.TranslationX - 10, rightArrow.TranslationY, 500);
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
                if (currentProfessor < professors.Count)
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

        #region Accessing Matching Database
        async void AddProfessor()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MentorMeetSQLite.db3");
                var conn = new SQLiteConnection(dbPath);
                var data1 = conn.Table<User>();
                var data2 = conn.CreateTable<Matches>();
                var data3 = conn.CreateTable<Matching>();

                var possibleMentorsList = conn.Query<User>("SELECT * FROM User WHERE IsMentor = 1");

                foreach (var mentor in possibleMentorsList)
                {
                    professors.Add(new Professor("LSU", mentor.First + " " + mentor.Last, mentor.Details));
                }

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