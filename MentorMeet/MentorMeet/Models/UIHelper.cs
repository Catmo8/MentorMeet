using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MentorMeet.Models
{
    class UIHelper
    {
        public BoxView[] GenerateShadowBoxes(int numOfBoxes, double cornerRadius, double width, double height, double yStart = 0, bool moveX = false)
        {

            if (numOfBoxes > 15)
            {
                numOfBoxes = 15;
            }

            BoxView[] boxViews = new BoxView[numOfBoxes];

            double opacity = 1 / (double)numOfBoxes; //to gradually and equally increase the opacity as the cardShadow overlap.
            //Generates the cardShadow for the shadow's gradient effect

            for (int i = 0; i < numOfBoxes; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.CornerRadius = cornerRadius;
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;

                if(width!=0)
                    tempBox.WidthRequest = width;
                if(height!=0)
                    tempBox.HeightRequest = height;

                tempBox.TranslationY = i + yStart;
                if (moveX)
                    tempBox.TranslationX = (i) / 2;

                tempBox.Color = Color.FromHex("#000000");

                tempBox.Opacity = opacity;
                boxViews[i] = tempBox;
            }

            return boxViews;
        }

        //Same as above except modified for only a lifting effect and not a perspective effect
        public BoxView[] GenerateShadowBoxes(bool expanding, int numOfBoxes, double cornerRadius, double width, double height, double yStart = 0)
        {

            if (numOfBoxes > 15)
            {
                numOfBoxes = 15;
            }

            BoxView[] boxViews = new BoxView[numOfBoxes];

            double opacity = 1 / (double)numOfBoxes; //to gradually and equally increase the opacity as the cardShadow overlap.
            //Generates the cardShadow for the shadow's gradient effect

            for (int i = 0; i < numOfBoxes; i++)
            {
                BoxView tempBox = new BoxView();
                tempBox.CornerRadius = cornerRadius;
                tempBox.HorizontalOptions = LayoutOptions.Center;
                tempBox.VerticalOptions = LayoutOptions.Center;

                if (width != 0)
                    tempBox.WidthRequest = width + i;
                if (height != 0)
                    tempBox.HeightRequest = height + i;
                 
                tempBox.TranslationY = yStart;

                tempBox.Color = Color.FromHex("#000000");

                tempBox.Opacity = opacity;
                boxViews[i] = tempBox;
            }

            return boxViews;
        }
    }
}
