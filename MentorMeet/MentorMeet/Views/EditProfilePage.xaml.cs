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

        public Entry getContact()
        {
            return email;
        }

        public Entry getName()
        {
            return displayName;
        }

        public Editor getDetails()
        {
            return detailsEditor;
        }
        public Editor getInterests()
        {
            return interestEditor;
        }

    }
}