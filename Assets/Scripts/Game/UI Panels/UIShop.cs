using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using EventData;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : BaseUI
{
    public GameEvent onBuyClick;

    public GameObject content;

    // List Data
    private List<ShopItem> _listItems = new();
    public List<ShopItem> listCharacters = new();

    public GameObject itemPrefab;

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

    public void OnCharacterCategoryClick()
    {
    }

    public void OnItemCategoryClick()
    {
    }

    public void OnSkinCategoryClick()
    {
    }

    private void LoadShop()
    {
        foreach (var item in _listItems)
        {
            var go = Instantiate(itemPrefab);
            go.transform.parent = content.transform;
        }
    }

    protected override void OnShow(UIParam param = null)
    {
        base.OnShow(param);
        for (var i = 0; i < 22; i++)
        {
            var go = Instantiate(itemPrefab, content.transform, false);
            go.GetComponentInChildren<Button>().onClick.AddListener(OnItemClick);
        }
    }

    public void UpdateShop(Component sender, object data)
    {
        Debug.Log("Hello");
        Debug.Log(data);
        // check sender
        var temp = (GameData)data;
        _listItems = temp.ListShopItems;
        LoadShop();
        // Character
    }
}