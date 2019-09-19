using System;

namespace MyBuyList.Shared
{
    public partial class ingredients
    {
        private string _measureUnitName;
        private string _foodName;

        public string DISPLAY_NAME
        {
            get
            {
                string displayQuantity = "";
                string[] arr = Quantity.ToString().Split(new string[] { ".", "," }, StringSplitOptions.RemoveEmptyEntries);
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
                        displayQuantity = Quantity.ToString();
                        while (displayQuantity.EndsWith("0"))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                        if (displayQuantity.EndsWith(".") || displayQuantity.EndsWith(","))
                        {
                            displayQuantity = displayQuantity.Remove(displayQuantity.Length - 1, 1);
                        }
                    }
                }
                return string.Format("{0} {1} {2} {3}", displayQuantity,
                                     measurementunits.UnitName,
                                     food.FoodName, string.IsNullOrEmpty(this.Remarks) ? "" : this.Remarks);
            }

        }
        public string FoodName
        {
            get
            {
                if (food != null)
                {
                    return food.FoodName;
                }
                return _foodName;
            }
            set
            {
                _foodName = value;
            }
        }
        public string MeasureUnitName
        {
            get
            {
                if (measurementunits != null)
                {
                    return measurementunits.UnitName;
                }
                return _measureUnitName;
            }
            set
            {
                _measureUnitName = value;
            }

        }
    }

    public class FlatIngredient
    {
        public int IngredientId { get; set; }
        public int RecipeId { get; set; }
        public int FoodId { get; set; }
        public int MeasurementUnitId { get; set; }
        public decimal Quantity { get; set; }
        public string Remarks { get; set; }
        public int SortOrder { get; set; }
        public string FoodName { get; set; }
        public string MeasureUnitName { get; set; }
        public string FractionDisplay
        {
            get
            {
                string fraction = "";
                string[] seperator = new string[] { ",","." };
                string[] arr = this.Quantity.ToString().Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                //if (arr.Length > 0)
                //{
                //    if (arr[0] != "0")
                //        fraction = arr[0];
                //}
                if (arr.Length > 1 && arr[1] != "")
                {
                    while (arr[1].EndsWith("0"))
                    {
                        arr[1] = arr[1].Remove(arr[1].Length - 1, 1);
                    }

                    if (arr[1] == "25")
                    {
                        fraction = "¼";
                    }
                    else if (arr[1] == "3" || arr[1] == "33" || arr[1] == "34")
                    {
                        fraction = "⅓";
                    }
                    else if (arr[1] == "5")
                    {
                        fraction = "½";
                    }
                    else if (arr[1] == "6" || arr[1] == "66" || arr[1] == "67")
                    {
                        fraction = "⅔";
                    }
                    else if (arr[1] == "75")
                    {
                        fraction = "¾";
                    }
                }
                return fraction;
            }
        }
    }
}
