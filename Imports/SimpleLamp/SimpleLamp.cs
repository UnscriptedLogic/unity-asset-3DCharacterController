using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLamp : ItemObject
{
    public Light lightsource;

    public override void Interact()
    {
        lightsource.enabled = !lightsource.enabled;
    }

    public void SetLight(bool active)
    {
        lightsource.enabled = active;
    }
}
