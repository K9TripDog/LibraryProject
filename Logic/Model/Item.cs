using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace Logic
{
    public abstract class Item
    {
        public static int id;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? RentDate { get; set; } // only if made part 2
        public DateTime? ReturnDate { get; set; } // only if made part 2?
        public double Price { get; set; }
        public double DiscountPrice { get; set; } // only if made part 3?
        public double CurrectPrice { get; set; } // only if made part 3?
        public Users Renter { get; set; }
        public bool LateOnReturn { get; set; }
        public string ItemType => this.GetType().Name;

        public Item(string name, string publisher, string genre, DateTime publishDate, double price, double discountPrice = 0, DateTime? rentDate = null, DateTime? returnDate = null, Users renter = null)
        {
            //  Guid g = Guid.NewGuid();
            id++;
            ID = id;
            Name = name;
            Publisher = publisher;
            Genre = genre;
            Price = price;
            DiscountPrice = discountPrice;
            PublishDate = publishDate;
            RentDate = rentDate;
            ReturnDate = returnDate;
            Renter = renter;
            GetCurrectPrice();

        }
        public void GetCurrectPrice()
        {
            CurrectPrice = Price - Price * (DiscountPrice/100);
        }

        public override string ToString()
        {
            return $"{Name}.\n at Price of:{CurrectPrice:C} \n\n More information about the book:\nGenre:{Genre} \nPublisher:{Publisher}\npublish:{PublishDate:d}\nRent Date:{RentDate:d}\n Return Date{ReturnDate:d}";
        }

    }
}
