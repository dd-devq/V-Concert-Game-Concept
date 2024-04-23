using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIInventory : BaseUI
{
    public GameEvent onEquipClick;
    public GameEvent onUnEquipClick;

    public GameObject equipSlot;

    public GameObject contentDrawer;

    public AssetReference itemSlotRef;

    private List<ItemInstance> _inventory = new();

    protected override void OnShow(UIParam param = null)
    {
        base.OnShow(param);
        if (_inventory.Count == PlayFabPlayerDataController.Instance.Inventory.Count) return;

        _inventory = PlayFabPlayerDataController.Instance.Inventory;

        var prefab = ResourceManager.LoadPrefabAsset(itemSlotRef);
        foreach (var item in _inventory)
        {
            var invItemGameObject = Instantiate(prefab, contentDrawer.transform, false);
            var invItemImage = invItemGameObject.GetComponent<Image>();
            var invItemAmount = invItemGameObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>();

            invItemAmount.SetText((item.RemainingUses ?? 0).ToString());

            //Load and Set Image

            invItemGameObject.GetComponent<Button>().onClick.AddListener(() => { OnEquipClick(invItemImage); });
        }
    }

    private void OnEquipClick(Image itemImage)
    {
        var equipItemImage = equipSlot.GetComponent<Image>();
        if (equipItemImage.sprite)
        {
        }

        equipItemImage.sprite = itemImage.sprite;
        ReloadInventory();
        onEquipClick.Invoke(this, null);
    }

    public void OnUnEquipClick()
    {
        onUnEquipClick.Invoke(this, null);
    }

    private void ReloadInventory()
    {
    }
}