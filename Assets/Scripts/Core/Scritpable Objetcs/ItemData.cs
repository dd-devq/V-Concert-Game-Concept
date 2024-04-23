using System.Collections.Generic;
using UnityEngine;
using GameData;

[CreateAssetMenu(menuName = "Scriptable Object/Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemPath;
    public List<Item> ListItem;
}