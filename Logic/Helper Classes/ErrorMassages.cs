using System;
using System.Linq;
using System.Text;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Popups;
using Windows.UI.Xaml.Data;
using Newtonsoft.Json.Schema;
using JsonSubTypes;

namespace Logic
{
    public static class ErrorMassages
    {
        /* Dictionary Try
        static Dictionary<string, string> eErrorMassages = new Dictionary<string, string>
        {
            { "Name", $"Invalid: Name" },
            {"Publisher", $"Invalid: Publish" },
             {"Genre", $"Invalid: %word%" },
            {"TryAsync","ASYNCCCCc" }
        };
        public static async void GetAsyncError(string errorName, string data = null)
        {
            if (data != null)
            {
                string result = eErrorMassages[errorName];
                result.Replace("%word%", data);
                MessageDialog errorDataAsync = new MessageDialog(result);
                await errorDataAsync.ShowAsync();
            }
            else
            {
                MessageDialog errorAsync = new MessageDialog(eErrorMassages[errorName]);
                await errorAsync.ShowAsync();
            }
        }
        public static string GetError(string errorName, string data = null)
        {
            if (data != null)
            {
                string result = eErrorMassages[errorName];
                result.Replace("%word%", data);
                return result;
            }
            return eErrorMassages[errorName];
        } */

        //String Send Errors
        public static string InvalidError(string word) => $"Invalid: {word}";
        public static string NullOrEmptyError(string word) => $"{word} Cannot be Null or Empty";
        public static string NegetiveNumError(string word) => $"{word} Cannot Be Negetive";
        public static string FutureDateError() => "The Date Cannot be in the Future";
        public static string UnknownIDError() => "This Item ID dosent Exist";
        public static string MagazineAuthorChangeError() => "you cant change author because Magazine Dont have Author";
        public static string ItemExistsError(string name) => $"The item {name} is already inside the Library";
        public static string PasswordsNotTheSameError() => $"Passwords are not the same\n Try Again Please";
        public static string NameExistsError(string name) => $"The name: {name} is already exists\n Try Again Please";
        public static string WrongUserError() => "Name or password is wrong";


        //MessageDialog Errors
        public static async void ItemNotSelectedError(string rent = null)
        {
            MessageDialog error = new MessageDialog($"Didnt choose any book from {rent} Library");
            await error.ShowAsync();
        }
        public static async void CantReturnError()
        {
            MessageDialog error = new MessageDialog("You can't return items that not belongs to you");
            await error.ShowAsync();
        }
        public static async void NoCashInLibraryError()
        {
            MessageDialog error = new MessageDialog($"there isnt Enoght cash in the library Cash Box");
            await error.ShowAsync();
        }
    }
}



