using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Users;
using System.Windows.Input;

namespace MentorMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagingPage : ContentPage
    {
        public List<Professor> people = new List<Professor>()
        {
            new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"),
            new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"),
            new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains"),
            new Professor("LSU", "Bijaya Karki", "Scientific Visualization and Applications, Computational Materials, Large-Scale Simulations"),
            new Professor("LSU", "Feng Chen", "Operating Systems, Storage Systems (Flash SSDs, Persistent Memory, Cloud Storage), Data Management in Cloud and Large-Scale Distributed Storage Systems"),
            new Professor("LSU", "Golden Richard", "Digital forensics, memory forensics, reverse engineering, malware analysis, operating systems"),
            new Professor("LSU", "Jinwei Ye", "Computer Vision, Computational Photography, and Computer Graphics"),
            new Professor("LSU", "Doris Carver", "Conformance Testing Distributed Systems, Requirement Traceability, Model-Driven Software Development, Reverse Engineering"),
            new Professor("LSU", "Gerald Baumgartner", "Design and Implementation of Domain-Specific Languages, Compiler Optimization, Desktop Grids, Object-Oriented Languages, Software Engineering Tools, and Embedded Systems Programming Tools"),
            };

        public MessagingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            MessagerList.ItemsSource = people;

        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                MessagerList.ItemsSource = people;
            }

            else
            {
                MessagerList.ItemsSource = people.Where(x => x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        //public ICommand ItemClickCommand
        //{
        //    get
        //    {

         //   }
        //}
    }
}