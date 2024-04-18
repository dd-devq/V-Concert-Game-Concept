using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using EventData;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIShop : BaseUI
{
    public GameEvent onBuyClick;

    public GameObject contentDrawer;

    // List Data
    private List<ShopItem> _listItems = new();
    public List<ShopItem> listCharacters = new();

    public AssetReferenceGameObject itemRef;

    public void OnItemClick()
    {
        // Load UI
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIItemViewer);
    }

    public void OnBuyClick()
    {
        onBuyClick.Invoke(this, null);
    }

    private async void OnEnable()
    {
        var catalogItems = PlayfabGameDataController.Instance.CatalogItems;
        var prefab = ResourceManager.LoadPrefabAsset(itemRef);
        foreach (var item in catalogItems)
        {
            Instantiate(prefab, contentDrawer.transform, false);
        }
    }

    private void OnDisable()
    {
    }

    public void OnCharacterCategoryClick()
    {
    }

    public void OnItemCategoryClick()
    {
    }

    public void OnSkinCategoryClick()
    {
    }


    public void UpdateShop(Component sender, object data)
    {
        // check sender
        var temp = (GameData)data;
        _listItems = temp.ListShopItems;
    }
}