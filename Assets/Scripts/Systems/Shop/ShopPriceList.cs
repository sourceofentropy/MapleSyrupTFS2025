using System.Collections.Generic;

public class ShopPriceList
{
    public Dictionary<ItemTypes.Types, float> Prices { get; private set; }

    public ShopPriceList()
    {
        ResetToDefault();
    }

    // Loads prices from the base dictionary
    public void ResetToDefault()
    {
        Prices = new Dictionary<ItemTypes.Types, float>(DefaultItemCosts.BasePrices);
    }

    // Change price of a specific item
    public void SetPrice(ItemTypes.Types item, float newPrice)
    {
        if (Prices.ContainsKey(item))
            Prices[item] = newPrice;
        else
            Prices.Add(item, newPrice);
    }

    // Safe getter
    public float GetPrice(ItemTypes.Types item)
    {
        return Prices.TryGetValue(item, out float price) ? price : 0f;
    }
}