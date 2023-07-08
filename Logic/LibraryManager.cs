using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Logic
{
    public class LibraryManager
    {

        public static int Count { get; set; } = 0;
        public MyCashBox LibraryCashBox { get; set; }
        List<Item> _items;
        List<Users> _users;

        // ||Login Page||
        public bool AddNewUser(string name, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                throw new NullReferenceException();
            }
            else if (password != confirmPassword || password.Length < 4)
            {
                throw new ArgumentException();
            }
            else if (_users.FirstOrDefault(item => item.Name == name) != null)
            {
                throw new Exception();
            }
            else
            {
                Users newUser = new Users(name, password);
                _users.Add(newUser);
                return true;
            }

        }
        public Users UserCheck(string name, string password)
        {
            Users currectUser = _users.FirstOrDefault(item => item.Name == name && item.Password == password);

            if (currectUser != null)
            {
                return currectUser;
            }
            throw new ArgumentException();
        }

        // ||Add Edit Page||
        public void AddItem(string name, string publisher, string genre, string stringPrice, string stringDiscountPrice, string newYear, string newMonth, string newDay, string author = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(ErrorMassages.NullOrEmptyError("Name"));

            if (string.IsNullOrEmpty(publisher))
                throw new Exception(ErrorMassages.NullOrEmptyError("Company"));

            if (string.IsNullOrEmpty(genre))
                throw new Exception(ErrorMassages.NullOrEmptyError("Genre"));

            double Price = NumbersCheck.PriceCheck(stringPrice);
            double DiscountPrice = NumbersCheck.DiscountPriceCheck(stringDiscountPrice);
            int PublishDay = NumbersCheck.DayCheck(newDay);
            int PublishMonth = NumbersCheck.MonthCheck(newMonth);
            int PublishYear = NumbersCheck.YearCheck(newYear);

            DateTime newDate = new DateTime(PublishYear, PublishMonth, PublishDay);
            if (newDate > DateTime.Now)
                throw new Exception(ErrorMassages.FutureDateError());

            if (_items.FirstOrDefault(MyItems => MyItems.Name == name && MyItems.Publisher == publisher && MyItems.PublishDate == newDate) == null)
            {
                Item newItem;

                if (string.IsNullOrEmpty(author))
                    newItem = new Magazine(name, publisher, genre, newDate, Price, DiscountPrice);
                else
                    newItem = new Book(name, publisher, genre, newDate, Price, DiscountPrice, author);

                _items.Add(newItem);
                LibraryCashBox.AddCost();
                Count++;
            }
            else
                throw new Exception(ErrorMassages.ItemExistsError(name));
        }
        public void EditItem(Item OldItem, string name = null, string publisher = null, string genre = null, string stringPrice = null, string stringDiscountPrice = null, string author = null, string newYear = null, string newMonth = null, string newDay = null) // i think its more right like this than check all in the UI?? ask some people.
        {
            int PublishYear = OldItem.PublishDate.Year;
            if (!string.IsNullOrEmpty(newYear))
                PublishYear = NumbersCheck.YearCheck(newYear);

            int PublishMonth = OldItem.PublishDate.Month;
            if (!string.IsNullOrEmpty(newMonth))
                PublishMonth = NumbersCheck.MonthCheck(newMonth);

            int PublishDay = OldItem.PublishDate.Day;
            if (!string.IsNullOrEmpty(newDay))
                PublishDay = NumbersCheck.DayCheck(newDay);

            double Price = OldItem.Price;
            if (!string.IsNullOrEmpty(stringPrice))// first check if its null, if yes so keep it at is
            {
                Price = NumbersCheck.PriceCheck(stringPrice);
                OldItem.Price = Price;
            }

            double Discount = OldItem.DiscountPrice;
            if (!string.IsNullOrEmpty(stringDiscountPrice))
            {
                Discount = NumbersCheck.DiscountPriceCheck(stringDiscountPrice);
                OldItem.DiscountPrice = Discount;
            }

            DateTime newDate = new DateTime(PublishYear, PublishMonth, PublishDay);

            if (newDate > DateTime.Now)
                throw new Exception(ErrorMassages.FutureDateError());

            if (string.IsNullOrEmpty(author) == false)
            {
                if (OldItem is Book)
                {
                    Book book = (Book)OldItem;
                    book.Author = author;
                    OldItem = book;
                }
                else
                    throw new ArgumentException(ErrorMassages.MagazineAuthorChangeError());

            }

            OldItem.PublishDate = newDate;
            OldItem.Name = string.IsNullOrEmpty(name) ? OldItem.Name : name;
            OldItem.Publisher = string.IsNullOrEmpty(publisher) ? OldItem.Publisher : publisher;
            OldItem.Genre = string.IsNullOrEmpty(genre) ? OldItem.Genre : genre;

            LibraryCashBox.EditCost();
        }

        //||Main Page||
        public List<Item> GetAllItems() => _items;
        public List<Item> Serch(string name, string genre, string author, string publisher, string type)
        {
            IEnumerable<Item> temp = _items.Where(item => item.RentDate == null).ToList();


            if (!string.IsNullOrEmpty(type))
                temp = temp.Where(item => item.ItemType.ToLower().Contains(type));

            if (!string.IsNullOrEmpty(name))
                temp = temp.Where(item => item.Name.ToLower().Contains(name)); // i already Tolower the sending stings in the ui

            if (!string.IsNullOrEmpty(genre))
                temp = temp.Where(item => item.Genre.ToLower().Contains(genre));

            if (!string.IsNullOrEmpty(publisher))
                temp = temp.Where(item => item.Publisher.ToLower().Contains(publisher));

            if (!string.IsNullOrEmpty(author))
            {
                temp = temp.Where(item => item is Book);
                temp = temp.Where(item => ((Book)item).Author.ToLower().Contains(author));
            }

            return temp.ToList();

        }

        //Renting
        public void RentItem(Item item, Users renter)
        {
            item.RentDate = DateTime.Now;
            item.ReturnDate = DateTime.Now.AddDays(14);
            item.Renter = renter;

            if (!librarianCheck(renter))
            {
                LibraryCashBox.RentItem(item.CurrectPrice);
            }
        }
        public void ReturnItem(Item item, Users currectUser)
        {
            if (currectUser is Librarian || item.Renter.Name == currectUser.Name && item.Renter.Password == currectUser.Password)
            {
                item.RentDate = null;
                item.ReturnDate = null;
                item.Renter = null;
            }
            else
                throw new ArgumentException();
        }
        public void LateOnReturnCheck(Item item)
        {
            if (item.ReturnDate <= DateTime.Now)
            {
                item.LateOnReturn = true;
            }
        }


        //Librarian Main
        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            Count--;
        }

        //Discounts
        public void AddDiscount(Item item, string discount)
        {
            item.DiscountPrice = NumbersCheck.DiscountPriceCheck(discount);
            item.GetCurrectPrice();
        }
        public void RemoveDiscount(Item item)
        {
            item.DiscountPrice = 0;
            item.GetCurrectPrice();
        }

        //Serilaiztion
        public void CreateDefaultData()
        {
            LibraryCashBox = new MyCashBox(500);

            _users = new List<Users>()
        {
            new Librarian("ido","1234"),
            new Librarian("dolev","2468"),
            new Users("simpleUser1","1111"),
            new Users("simpleUser2","2222"),
            new Users("simpleUsee3","3333")
        };

            _items = new List<Item>()
            {
        new Book("Atomic Habits","idoProductions","Economy",new DateTime(2020,4,16),100,0,"James"),
        new Magazine("The Comedy Show","idoProductions","Comedy",new DateTime(2001,1,22),80,0),
        new Book( "To Kill a Mockingbird",  "HarperCollins Publishers", "Classic", new DateTime(1960, 7, 11), 20, 0,"Harper Lee"),
        new Book( "1984", "Penguin Books Ltd", "Dystopian", new DateTime(1949, 6, 8), 15, 10,"George Orwell"),
        new Book( "The Hitchhiker's Guide to the Galaxy", "Pan Books", "Science Fiction", new DateTime(1979, 10, 12), 25, 0, "Douglas Adams"),
        new Magazine( "National Geographic", "National Geographic Society", "Science", new DateTime(1888, 9, 22), 5, 0),
        new Book("The Great Gatsby", "Scribner", "Classic", new DateTime(1925, 4, 10), 18, 0, "F. Scott Fitzgerald"),
        new Book("The Catcher in the Rye", "Little, Brown and Company", "Coming-of-Age Fiction", new DateTime(1951, 7, 16), 20, 0, "J.D. Salinger"),
        new Book("One Hundred Years of Solitude", "Harper & Row", "Magical Realism", new DateTime(1967, 5, 30), 22, 0, "Gabriel García Márquez"),
        new Book("The Alchemist", "HarperCollins", "Fiction", new DateTime(1988, 4, 25), 12, 0, "Paulo Coelho"),
        new Magazine("Time", "Time Inc.", "News", new DateTime(1923, 3, 3), 4, 0),
        new Magazine("Vogue", "Condé Nast", "Fashion", new DateTime(1892, 12, 17), 6, 0),
        new Book("Pride and Prejudice", "T. Egerton, Whitehall", "Classic", new DateTime(1813, 1, 28), 16, 0, "Jane Austen"),
        new Book("The Lord of the Rings", "Allen & Unwin", "Fantasy", new DateTime(1954, 7, 29), 30, 0, "J.R.R. Tolkien"),
        new Book("The Picture of Dorian Gray", "Ward, Lock and Company", "Gothic Fiction", new DateTime(1890, 6, 20), 14, 0, "Oscar Wilde"),
        new Magazine("Forbes", "Integrated Whale Media", "Business", new DateTime(1917, 9, 15), 9, 0),
        new Magazine( "Wired", "Condé Nast", "Technology", new DateTime(1993, 1, 25), 8, 0,new DateTime(2020,8,20),new DateTime(2021,8,19),_users[0]),
        new Book("The Adventures of Sherlock Holmes", "George Newnes Ltd", "Mystery", new DateTime(1892, 10, 14), 25, 20,"ido", new DateTime(2022, 2, 1), new DateTime(2022, 2, 15), _users[2])
            };

        }
        public async void GetJsonFile()
        {
            try
            {
                var JsonTask = JsonFileSerialize.SimpleRead(); // Deserialization
                var listFromJson = await JsonTask;
                _items = listFromJson.items;
                _users = listFromJson.users;
                LibraryCashBox = new MyCashBox();
                LibraryCashBox.TotalCash = listFromJson.Cash;

            }
            catch (FileNotFoundException)
            {
                CreateDefaultData();
            }
        }
        public void AddToJsonFile() => JsonFileSerialize.SimpleWrite(_items, _users, LibraryCashBox.TotalCash); // Serialization


        //Private
        bool librarianCheck(Users renter)
        {
            if (renter is Librarian)
            {
                return true;
            }
            return false;
        }
    }
}

