using System;

namespace Logic
{
    public class Magazine : Item
    {
        public Magazine(string name, string publisher, string genre, DateTime publishDate, double price, double discountPrice, DateTime? rentDate = null, DateTime? returnDate = null, Users renter = null) : base(name, publisher, genre, publishDate, price, discountPrice, returnDate, returnDate,renter)
        {
        }
        public override string ToString()
        {
            return $"Magazine: {base.ToString()}";
        }
    }
}
