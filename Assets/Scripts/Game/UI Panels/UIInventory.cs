using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIInventory : BaseUI
{
    public GameEvent onEquipClick;
    public GameEvent onUnEquipClick;

    public GameObject equipSlot;

    public GameObject contentDrawer;

    public AssetReference itemSlotRef;

    private List<ItemInstance> _inventory = new();

    private void OnEnable()
    {
        if (_inventory.Count == PlayfabPlayerDataController.Instance.Inventory.Count) return;

        _inventory = PlayfabPlayerDataController.Instance.Inventory;
        // GameObject prefab = null;
        //
        // for (int i = 0; i < 3; i++) // Retry 3 times
        // {
        //     prefab = await ResourceManager.Instance.LoadPrefabAsset(itemSlotRef);
        //     if (prefab != null)
        //         break; // If loaded successfully, exit loop
        //     else
        //         await Task.Delay(1000); // Wait for 1 second before retrying
        // }

        var prefab = ResourceManager.LoadPrefabAsset(itemSlotRef);
        foreach (var item in _inventory)
        {
            var itemGo = Instantiate(prefab, contentDrawer.transform, false);
            var invItem = itemGo.GetComponent<UIInventoryItem>();
            invItem.SetAmount(item.RemainingUses ?? 0);
        }
    }

    public void OnEquipClick()
    {
    }

    public void ReloadInventory()
    {
    }
}