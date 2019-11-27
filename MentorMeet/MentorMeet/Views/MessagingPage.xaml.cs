using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MentorMeet.Users;

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
            new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"),
            new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"),
            new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains"),
            new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"),
            new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"),
            new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains"),
            new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"),
            new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"),
            new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains"),
            new Professor("LSU", "Konstantin Busch", "Distributed Algorithms and Data Structures, Communication Algorithms, and Algorithmic Game Theory"),
            new Professor("LSU", "Anas Mahmoud", "Software Engineering, Requirements Engineering, Program Comprehension, and Code Analysis"),
            new Professor("LSU", "William Duncan", "Knowledge Discovery and Data Mining, Bioinformatics, Stochastic Process and Markov Chains")
        };

        public MessagingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            MessageList.ItemsSource = people;

        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                MessageList.ItemsSource = people;
            }

            else
            {
                MessageList.ItemsSource = people.Where(x => x.name.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    }
}