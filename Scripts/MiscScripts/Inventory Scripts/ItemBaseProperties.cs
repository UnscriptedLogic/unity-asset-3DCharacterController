using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ItemBaseProperties
{
    public Vector3 scale;

    public ItemBaseProperties(Vector3 _scale)
    {
        scale = _scale;
    }
}
