using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleLamp))]
[DisallowMultipleComponent]
public class LampAffectee : Affectee
{
    SimpleLamp simpleLamp;

    public override void Start()
    {
        base.Start();

        simpleLamp = GetComponent<SimpleLamp>();
    }

    public override void ListenTrigger()
    {
        simpleLamp.SetLight(true);
    }

    public override void ListenUntrigger()
    {
        simpleLamp.SetLight(false);
    }
}
