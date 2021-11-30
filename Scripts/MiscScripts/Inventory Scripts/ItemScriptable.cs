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

    [TextArea(15,20)]
    public string desc;

    public GameObject myself;
}
