using System;

namespace Logic
{
    public class NumbersCheck
    {
        public static double PriceCheck(string textbox)
        {
            bool priceCheck = double.TryParse((textbox), out double Price);
            if (priceCheck == false || Price <= 0)
                throw new ArgumentException(ErrorMassages.InvalidError("Price"));

            return Price;
        }
        public static double DiscountPriceCheck(string textbox)
        {
            bool DiscountCheck = double.TryParse((textbox), out double DiscountPrice);
            if (!DiscountCheck || DiscountPrice < 0 || DiscountPrice > 100)
                throw new ArgumentException(ErrorMassages.InvalidError("Discount Price"));

            return DiscountPrice;

        }

        public static int YearCheck(string textbox)
        {
            bool publishYear = int.TryParse((textbox), out int PublishYear);
            if (publishYear == false || PublishYear < 1000 || PublishYear > DateTime.Now.Year)
                throw new ArgumentException(ErrorMassages.InvalidError("Year"));

            return PublishYear;
        }
        public static int MonthCheck(string textbox)
        {
            bool publishMonth = int.TryParse((textbox), out int PublishMonth);
            if (publishMonth == false || PublishMonth < 1 || PublishMonth > 12)
                throw new ArgumentException(ErrorMassages.InvalidError("Month"));

            return PublishMonth;
        }
        public static int DayCheck(string textbox)
        { //add specific months?
            bool publishDay = int.TryParse((textbox), out int PublishDay);

            if (publishDay == false || PublishDay < 1 || PublishDay > 31)
                throw new ArgumentException(ErrorMassages.InvalidError("Day"));

            return PublishDay;
        }
    }
}
