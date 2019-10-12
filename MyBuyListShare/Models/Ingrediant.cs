using System;
using System.Collections.Generic;

namespace MyBuyListShare.Models
{
    public class IngrediantModel
    {
        private string _quantity0;
        private string _quantity1;

        public int? id { get; set; }
        public string name { get; set; }
        public decimal quantity { get; set; }
        public string unit { get; set; }
        public int unitId { get; set; }
        public string howTo { get; set; }

        public string quantity0
        {
            get
            {
                return ((int)quantity).ToString();
            }
            set
            {
                _quantity0 = value;
                UpdateQuantity();
            }
        }

        public string quantity1
        {
            get
            {
                return (quantity - (int)quantity).ToString("0.##");
            }
            set
            {
                _quantity1 = value;
                UpdateQuantity();
            }
        }

        public string displayName
        {
            get
            {
                string displayQuantity = "";
                string[] arr = this.quantity.ToString().Split(",".ToCharArray());
                if (arr.Length > 0)
                {
                    if (arr[0] != "0")
                        displayQuantity = arr[0];
                }
                if (arr.Length > 1 && arr[1] != "")
                {
                    while (arr[1].EndsWith("0"))
                    {
                        arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                    }

                    if (arr[1] == "25")
                    {
                        displayQuantity = "¼" + displayQuantity;
                    }
                    else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                    {
                        displayQuantity = "⅓" + displayQuantity;
                    }
                    else if (arr[1] == "5")
                    {
                        displayQuantity = "½" + displayQuantity;
                    }
                    else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                    {
                        displayQuantity = "⅔" + displayQuantity;
                    }
                    else if (arr[1] == "75")
                    {
                        displayQuantity = "¾" + displayQuantity;
                    }
                    else
                    {
                        displayQuantity = this.quantity.ToString();
                        while (displayQuantity.EndsWith("0"))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                        if (displayQuantity.EndsWith(",") || displayQuantity.EndsWith("."))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                    }
                }
                return string.Format("{0} {1} {2}", displayQuantity, unit, name);
            }
        }

        private void UpdateQuantity()
        {
            string x = "0";
            if (_quantity1 != null && _quantity1 != "0" && _quantity1 != "")
            {
                x = _quantity1.Split(new string[] { ",", "." }, StringSplitOptions.RemoveEmptyEntries)[1];
            }

            string a = string.Format("{0},{1}", _quantity0 == null ? "0" : _quantity0, x);
            quantity = Convert.ToDecimal(a);
        }
    }

    public class IngrediantModelContainer
    {
        public List<IngrediantModel> ingrediants { get; set; }
    }
}