using Logic;
using System;
using Windows.UI.Popups;

namespace Logic
{
    public class UIMassages
    {
        //buttons Change
        public static string LibrarianRentButtonText() => "Move item to Rent List";
        public static string LibrarianReturnButtonText() => "Move item to Library List";


        //async
        public static async void GetReceipMsg(Item item, Users user)
        {
            MessageDialog error = new MessageDialog($"Name:{user}      Date:{DateTime.Now}\n\n {user} Thanks for renting!\n {item}");
            await error.ShowAsync();
        }
        public static async void EditedItemMsg()
        {
            MessageDialog error = new MessageDialog("Edit Complete");
            await error.ShowAsync();
        }
        public static async void AddItemMsg(string item)
        {
            MessageDialog error = new MessageDialog($"{item} Added Secsusfully");
            await error.ShowAsync();
        }

     
    }
}