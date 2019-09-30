namespace MyBuyListShare.Models
{
    public class IngrediantModel
    {
        public string name { get; set; }
        public decimal quantity { get; set; }
        public string measure { get; set; }

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
                return string.Format("{0} {1} {2}", displayQuantity, measure, name);
            }
        }
    }
}