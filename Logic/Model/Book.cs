using System;

namespace Logic
{
    public class Book : Item
    {
        public string Author { get; set; }

        public Book(string name, string publisher, string genre, DateTime publishDate, double price, double discountPrice, string author, DateTime? rentDate = null, DateTime? returnDate = null, Users renter = null) : base(name, publisher, genre, publishDate, price, discountPrice, rentDate, returnDate,renter)
        {
            Author = author;
        }

        public override string ToString()
        {
            return $"Book: {base.ToString()} ,Author: {Author}";
        }
    }
}
