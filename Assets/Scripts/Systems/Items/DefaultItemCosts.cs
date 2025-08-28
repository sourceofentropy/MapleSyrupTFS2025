using System.Collections.Generic;

public static class DefaultItemCosts
{
    public static Dictionary<ItemTypes.Types, float> BasePrices { get; private set; }

    static DefaultItemCosts()
    {
        BasePrices = new Dictionary<ItemTypes.Types, float>
        {
            { ItemTypes.Types.Sap, 1f },
            { ItemTypes.Types.SyrupUnfiltered, 6f },
            { ItemTypes.Types.SyrupFiltered, 12f },
            { ItemTypes.Types.SyrupBottleUnfiltered, 12f },
            { ItemTypes.Types.SyrupBottleFiltered, 24f },
            { ItemTypes.Types.TaffyTray, 8f },
            { ItemTypes.Types.TaffyBox, 10f },
            { ItemTypes.Types.IceCreamBucket, 25f }
            //[ItemTypes.Types.SnowCandySingle] = 1f;
            //[ItemTypes.Types.SnowCandyBox] = 1f;

        };
    }
}
