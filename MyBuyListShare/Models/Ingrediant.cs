using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyBuyListShare.Models
{
    public class IngrediantModel
    {
        private int _quantity0;
        private decimal _quantity1;

        public int? id { get; set; }
        public int foodId { get; set; }
        public string name { get; set; }
        public decimal quantity { get; set; }
        public string unit { get; set; }
        public int unitId { get; set; }
        public string howTo { get; set; }

        public int quantity0
        {
            get
            {
                return (int)quantity;
            }
            set
            {
                _quantity0 = value;
            }
        }

        public decimal quantity1
        {
            get
            {
                return quantity < 1 ? quantity : quantity - (int)quantity;
            }
            set
            {
                _quantity1 = value;
            }
        }

        public string displayName
        {
            get
            {
                string displayQuantity = "";

                if(quantity0 != 0)
                {
                    displayQuantity = quantity0.ToString();
                }

                string x = quantity1.ToString("F", CultureInfo.CreateSpecificCulture("he-IL"));
                switch (x)
                {
                    case "0.25":
                        displayQuantity = "¼" + displayQuantity;
                        break;
                    case "0.3":
                    case "0.33":
                    case "0.34":
                        displayQuantity = "⅓" + displayQuantity;
                        break;
                    case "0.5":
                    case "0.50":
                        displayQuantity = "½" + displayQuantity;
                        break;
                    case "0.6":
                    case "0.66":
                    case "0.67":
                        displayQuantity = "⅔" + displayQuantity;
                        break;
                    case "0.75":
                        displayQuantity = "¾" + displayQuantity;
                        break;
                }
                return string.Format("{0} {1} {2} {3}", displayQuantity, unit, name, howTo);
            }
        }
    }

    public class IngrediantModelContainer
    {
        public List<IngrediantModel> ingrediants { get; set; }
    }
}