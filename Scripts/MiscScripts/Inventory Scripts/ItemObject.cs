using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class ItemObject : MonoBehaviour 
{
    public ItemScriptable itemScriptable;
    public ItemBaseProperties baseProperties;

    public MeshRenderer[] meshRenderer;
    public Behaviour[] disableNotViewed;

    public void InitProperties()
    {
        transform.localScale = baseProperties.scale;
    }

    public void SaveProperties()
    {
        baseProperties.scale = transform.localScale;
    }
}