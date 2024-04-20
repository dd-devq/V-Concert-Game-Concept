using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemPath;
    public List<Item> ListItem;
}