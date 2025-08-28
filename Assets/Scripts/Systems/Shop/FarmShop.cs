using System;
using UnityEngine;
using System.Collections.Generic;

public class FarmShop : MonoBehaviour, IDropOffHandler, INPCPickUpHandler, IStructureUI
{
    [Header("Item Requirement Icon")]
    public Sprite inputIcon;

    [Header("Production Variables")]    
    //UI Prefabs
    public GameObject dropOffRequirementUIPrefab;
    [SerializeField] private ShopInventory inventory;
    [SerializeField] private ShopInventory inventoryPartDeux;
    private ItemTypes shopItemTypes;
    //private ItemCosts priceList; //old, remove me once new code works
    private ShopPriceList shopPriceList;
    [SerializeField] float priceMultiplier = 1.0f;
    private FarmStandDropOffRequirementUI dropOffUI;
    private int maxSyrupRequired = 100;
    [SerializeField] private int minCustomerItems = 0;
    [SerializeField] private int maxCustomerItems = 10;
    private ItemTypes.Types itemToBuyKey;
    private string itemToBuyStr;
    [SerializeField] private Wallet playerWallet;
    public Dictionary<ItemTypes.Types, float> costDict = new Dictionary<ItemTypes.Types, float>();
    [Header("Item Requirement Icon")]
    public Sprite sapIcon; //Assign in the Inspector

    private Canvas mainCanvas;

    private void Start()
    {
        shopItemTypes = new ItemTypes();
        shopPriceList = new ShopPriceList();

        /*
        //hard code cost list for bug workaround
        priceList.costDict[ItemTypes.Types.Sap] = 1f;
        priceList.costDict[ItemTypes.Types.Sap] = 1f;
        priceList.costDict[ItemTypes.Types.SyrupUnfiltered] = 6f;
        priceList.costDict[ItemTypes.Types.SyrupFiltered] = 12f;
        priceList.costDict[ItemTypes.Types.SyrupBottleUnfiltered] = 12f;
        priceList.costDict[ItemTypes.Types.SyrupBottleFiltered] = 24f;
        priceList.costDict[ItemTypes.Types.TaffyTray] = 8f;
        priceList.costDict[ItemTypes.Types.TaffyBox] = 10f;
        priceList.costDict[ItemTypes.Types.IceCreamBucket] = 25f;
        
        inventory.currentStockDict[ItemTypes.Types.Sap] = 0;
        inventory.currentStockDict[ItemTypes.Types.SyrupUnfiltered] = 0;
        inventory.currentStockDict[ItemTypes.Types.SyrupFiltered] = 0;
        inventory.currentStockDict[ItemTypes.Types.SyrupBottleUnfiltered] = 0;
        inventory.currentStockDict[ItemTypes.Types.SyrupBottleFiltered] = 0;
        inventory.currentStockDict[ItemTypes.Types.TaffyTray] = 0;
        inventory.currentStockDict[ItemTypes.Types.TaffyBox] = 0;
        inventory.currentStockDict[ItemTypes.Types.IceCreamBucket] = 0;*/
        //currentStockDict[ItemTypes.Types.SnowCandySingle] = 0;
        //currentStockDict[ItemTypes.Types.SnowCandyBox] = 0;
        //costDict[ItemTypes.Types.SnowCandySingle] = 1f;
        //costDict[ItemTypes.Types.SnowCandyBox] = 1f;
        foreach (ItemTypes.Types itemType in System.Enum.GetValues(typeof(ItemTypes.Types)))
        {
            //var numHeld = playerInventory.GetItemCountByTag(itemType.ToString());
            //playerInventory.DropOffItems(numHeld, itemType.ToString(), this.transform);

            //put me back - inventory.currentStockDict[itemType] += 0;
            //UnityEngine.Debug.Log($"{itemType} start test at farm stand! Current inventory: {inventory.currentStockDict[itemType]}");
        }

        mainCanvas = GameObject.FindObjectOfType<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogError("No Canvas found in the scene!");
            return;
        }

        GameObject dropOffUIObject = Instantiate(dropOffRequirementUIPrefab, mainCanvas.transform);
        dropOffUI = dropOffUIObject.GetComponent<FarmStandDropOffRequirementUI>();
        
        if (dropOffUI == null)
        {
            UnityEngine.Debug.LogError("FarmStandDropOffRequirementUI component not found on prefab!");
            return;
        }

        dropOffUI.Initialize(this.transform, sapIcon); //TODO: update to syrup icon if one is available
    }

    public void HandleDropOff(PlayerObjects playerInventory)
    {
        UnityEngine.Debug.Log($"handle dropoff at farm stand");
        if (playerInventory == null)
        {
            UnityEngine.Debug.Log("Player inventory is null");
            return;
        }

        //items to collect at this farm stand
        /*
            Sap,
            SyrupUnfiltered, 
            SyrupFiltered, 
            SyrupBottleUnfiltered,
            SyrupBottleFiltered,
            TaffyTray,
            TaffyBox,
            IceCreamBucket,
        */
        foreach (ItemTypes.Types itemType in System.Enum.GetValues(typeof(ItemTypes.Types)))
        {
            var numHeld = playerInventory.GetItemCountByTag(itemType.ToString());
            if (numHeld > 0)
            {
                playerInventory.DropOffItems(numHeld, itemType.ToString(), this.transform);
                //put me back - inventory.currentStockDict[itemType] += numHeld;
                inventoryPartDeux.ModifyQuantity(itemType, numHeld);
                //UnityEngine.Debug.Log($"{itemType} dropped off at farm stand! Current inventory: {inventory.currentStockDict[itemType]}");
            }
            else
            {
                UnityEngine.Debug.Log($"No {itemType} to drop off!");
            }
        }

    }

    /* TODO: update to handle NPC pickups */
    public void HandlePickup()
    {
        int itemsWanted = UnityEngine.Random.Range(minCustomerItems, maxCustomerItems); // Generates a number between 1 and 9
        UnityEngine.Debug.Log($"NPC came to pick up {minCustomerItems}!");

        //iterate over the number of items they want
        for(int i=0; i < itemsWanted; i++)
        {
            //pick a random item
            //ItemTypes.Types itemType in System.Enum.GetValues(typeof(ItemTypes.Types))

            itemToBuyKey = shopItemTypes.GetRandomEnumKey();
            itemToBuyStr = itemToBuyKey.ToString();

            ItemTypes.Types itemKey = (ItemTypes.Types)itemToBuyKey;

            UnityEngine.Debug.Log($"NPC wants a {itemToBuyKey}! / {itemToBuyStr}");
            
            //if it's not in stock check for a different item
            if (inventoryPartDeux.GetQuantity(itemKey) == 0)
            {
                UnityEngine.Debug.Log($"NPC wants a {itemToBuyStr}: none in stock!");
                itemToBuyKey = shopItemTypes.GetRandomEnumKey();
                itemToBuyStr = itemToBuyKey.ToString();
                UnityEngine.Debug.Log($"NPC will try a {itemToBuyKey} - {itemToBuyStr} instead if it's in stock but will give up if it's not and move on to their next wanted item!");
            }

            //if there is stock buy one, but if not we only check once for an alternate and then keep moving, odd but that's the logic for now
            if (inventoryPartDeux.GetQuantity(itemKey) > 0)
            {

                Debug.Log($"attempting to get price for {itemKey}");
                foreach (KeyValuePair<ItemTypes.Types, float> p in shopPriceList.Prices)
                {
                    UnityEngine.Debug.Log($"price list {p.Key}, {p.Value}");
                }
                float price = shopPriceList.GetPrice(itemKey);
                inventoryPartDeux.ModifyQuantity(itemKey, -1);

                UnityEngine.Debug.Log($"NPC buys a {itemToBuyKey} for {price} beaver bucks!");
                //playerWallet.GetMoney(price);
                GameManager.Instance.ReceiveMoney((int)price);
            }            
            
            //iterate items wanted whether the npc got what they wanted or not

            //TODO: may wish to have items wanted progress over the course of the game in the future
        }
    }

    private void BuyItem()
    {

    }

    private void StockItem()
    {

    }

    private void GetItemPrice(ItemTypes.Types itemType)
    {
        float price = shopPriceList.GetPrice(itemType);
        Debug.Log($"{itemType} costs {price} currency.");
    }
    
    //TODO: duplicates BaseProductionMachine code. Both require this but other requirements vary. Inheritance/Composition may need to be reworked here in the future.
    public virtual void InitializeUI()
    {
        if (dropOffUI != null)
            return;

        mainCanvas = FindObjectOfType<Canvas>();
        if (!mainCanvas) { Debug.LogError("No Canvas found!"); return; }

        Transform gameUIPanel = mainCanvas.transform.Find("GameUIPanel");
        if (!gameUIPanel) { Debug.LogError("No GameUIPanel found in Canvas!"); return; }

        // Initialize Timer UI
        //GameObject timerUIObject = Instantiate(timerUIPrefab, gameUIPanel);
        //timerUI = timerUIObject.GetComponent<SapTimerUI>();
        //timerUI.Initialize(transform, isProducing);

        // Initialize Drop-Off UI
        GameObject dropOffUIObject = Instantiate(dropOffRequirementUIPrefab, gameUIPanel);
        dropOffUI = dropOffUIObject.GetComponent<FarmStandDropOffRequirementUI>();
        dropOffUI.Initialize(transform, inputIcon);
    }
}