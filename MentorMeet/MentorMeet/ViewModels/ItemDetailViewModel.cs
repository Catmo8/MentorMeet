using System;

using MentorMeet.Models;

namespace MentorMeet.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public User Item { get; set; }
        public ItemDetailViewModel(User item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
