using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
public class ItemScriptable : ScriptableObject
{
    public Sprite icon;

    [TextArea(15,10)]
    public string desc;

    [Space(100)]
    public GameObject myself;
    public bool stackable = true;
    public bool droppable = true;
}
