using System.Collections.Generic;
using PlayFab.ClientModels;
using TMPro;
using UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIShop : BaseUI
{
    public GameEvent onBuyClick;

    public GameObject contentDrawer;

    private List<ItemInstance> _listItems = new();
    public List<ItemInstance> listCharacters = new();

    public AssetReferenceGameObject itemRef;

    private List<GameObject> _listItemGameObjects = new();
    private List<GameObject> _listCharacterGameObjects = new();

    private Dictionary<string, Sprite> _listItemSprite = new();
    private GameObject _itemPrefab;

    private bool _isLoaded;

    protected override void Awake()
    {
        base.Awake();
        _isLoaded = false;
    }

    private void OnItemClick(string itemName, string description, string currency, int price)
    {
        PlaySoundOnClick();
        UIManager.Instance.HideUI(this);
        UIManager.Instance.ShowUI(UIIndex.UIItemViewer, new UIItemViewerParam
        {
            ItemName = itemName,
            Description = description,
            Currency = currency,
            Price = price
        });
    }


    private void OnBuyClick(ItemInstance item)
    {
        PlaySoundOnClick();
        onBuyClick.Invoke(this, item);
    }

    protected override void OnShow(UIParam param = null)
    {
        base.OnShow(param);
        if (!_isLoaded)
        {
            LoadShop();
            _isLoaded = true;
        }
    }

    private void LoadShop()
    {
        var catalogItems = PlayFabGameDataController.Instance.CatalogItems;

        _itemPrefab = ResourceManager.LoadPrefabAsset(itemRef);

        foreach (var item in catalogItems)
        {
            var shopItemGameObject = Instantiate(_itemPrefab, contentDrawer.transform, false);
            var buyButton = shopItemGameObject.transform.Find("Buy Button").GetComponent<Button>();
            var itemPrice = buyButton.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var currencyIcon = buyButton.transform.Find("Icon").GetComponent<Image>();
            var viewButton = shopItemGameObject.transform.Find("View Button");


            foreach (var (currency, price) in item.VirtualCurrencyPrices)
            {
                var iconSprite = ResourceManager.LoadSprite(currency);
                var itemSprite = ResourceManager.LoadSprite(item.DisplayName);

                viewButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnItemClick(item.DisplayName, item.Description, currency, (int)price);
                });

                buyButton.onClick.AddListener(() =>
                {
                    OnBuyClick(new ItemInstance
                    {
                        ItemId = item.ItemId,
                        UnitCurrency = currency,
                        UnitPrice = price
                    });
                });

                itemPrice.SetText(price.ToString());
                currencyIcon.sprite = iconSprite;
                viewButton.GetComponent<Image>().sprite = itemSprite;

                _listItemSprite.Add(item.DisplayName, itemSprite);
            }
        }
    }

    public void OnCharacterCategoryClick()
    {
    }

    public void OnItemCategoryClick()
    {
    }
}