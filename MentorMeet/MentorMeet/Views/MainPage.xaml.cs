using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using SQLite;
using System.IO;
using Xamarin.Forms.Xaml;

namespace MentorMeet.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            // This sets the tabbed page so the matching is page is on startup
            var pages = Children.GetEnumerator();
            pages.MoveNext(); // First page
            pages.MoveNext(); // Second page
            CurrentPage = pages.Current;
        }
    }

}