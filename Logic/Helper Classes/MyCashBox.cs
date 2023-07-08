using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    

    public class MyCashBox
    {
        public double TotalCash { get; set; }
        double addItemCost = 100;
        double editItemCost = 25;
        //can add alot more like trafic and stuff

        public MyCashBox(double startMoney)
        {
            TotalCash = startMoney;
        }
        public MyCashBox() { }

        public void AddCost() => TotalCash -= addItemCost;
        public void EditCost() => TotalCash -= editItemCost;
        public void RentItem(double itemCost) => TotalCash += itemCost;

        public bool EnugthCashCheck(Costs neededCash)
        {
            if (neededCash == Costs.editItem)
            {
                if (TotalCash >= editItemCost)
                    return true;
            }
            else if (neededCash == Costs.addItem)
            {
                if (TotalCash >= addItemCost)
                    return true;
            }

            throw new ArgumentException();
        }
        public override string ToString()
        {
            return $"{TotalCash:C}";
        }

        //for later using?
        public void AddCash(double cashToAdd) => TotalCash += cashToAdd;
        public void SubstractCash(double cashToSubstract)
        {
            TotalCash -= cashToSubstract;
        }
    }
}

