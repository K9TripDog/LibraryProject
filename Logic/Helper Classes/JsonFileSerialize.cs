using Newtonsoft.Json;
using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using Windows.Data.Json;
using Windows.UI.Xaml.Controls;

namespace Logic
{
    public class LibraryData
    {
        public List<Item> Items { get; set; }
        public List<Users> Users { get; set; }
        public double CashBox { get; set; }

    }

    public static class JsonFileSerialize
    {
        static StorageFile Path;
        static string fileNameLibraryJson = "Library1.1.1.json";

        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public async static void SimpleWrite(List<Item> items, List<Users> users, double cash)
        {
            var libraryData = new LibraryData { Items = items, Users = users, CashBox = cash };

            // serialize JSON to a string
            string jsonLibraryData = JsonConvert.SerializeObject(libraryData);

            // write string to a file
            Path = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileNameLibraryJson, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(Path, jsonLibraryData);
        }

        public static async Task<(List<Item> items, List<Users> users, double Cash)> SimpleRead()
        {
            Path = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileNameLibraryJson, CreationCollisionOption.OpenIfExists);

            string jsonItemList = await FileIO.ReadTextAsync(Path);

            if (string.IsNullOrEmpty(jsonItemList))
            {
                throw new FileNotFoundException();
            }

            List<Item> itemList = new List<Item>();
            List<Users> userList = new List<Users>();
            double cashBox;

            var jsonData = JObject.Parse(jsonItemList);

            //Cash
            cashBox = jsonData["CashBox"].ToObject<double>();

            //items
            JArray jsonItems = jsonData["Items"].ToObject<JArray>();
            foreach (JObject itemInJson in jsonItems)
            {
                string itemType = (string)itemInJson["ItemType"];

                if (itemType == "Book")
                {
                    Book book = itemInJson.ToObject<Book>();
                    itemList.Add(book);
                }
                else if (itemType == "Magazine")
                {
                    Magazine magazine = itemInJson.ToObject<Magazine>();
                    itemList.Add(magazine);
                }
            }

            //users
            JArray jsonUsers = jsonData["Users"].ToObject<JArray>();
            foreach (JObject itemInJson in jsonUsers)
            {
                string userType = (string)itemInJson["UserType"];

                if (userType == "Users")
                {
                    Users user = itemInJson.ToObject<Users>();
                    userList.Add(user);
                }
                else if (userType == "Librarian")
                {
                    Librarian librarian = itemInJson.ToObject<Librarian>();
                    userList.Add(librarian);
                }
            }

            return (itemList, userList, cashBox);
        }
    }
}


