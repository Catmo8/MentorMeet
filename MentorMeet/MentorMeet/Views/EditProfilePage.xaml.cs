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
    public partial class EditProfilePage : ContentView
    {
        public EditProfilePage()
        {
            InitializeComponent();
        }

        public Entry Email
        {
            get { return email; }
            set { email.Text = value.Text; }
        }

        public Entry Name
        {
            get { return displayName; }
            set { displayName.Text = value.Text; }
        }

        public Editor Details
        {
            get { return detailsEditor; }
            set { detailsEditor.Text = value.Text; }
        }
        public Editor Interests
        {
            get { return interestEditor; }
            set { interestEditor.Text = value.Text; }
        }

    }
}