using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affector : ItemObject
{
    public Action onTrigger;
    public Action onUntrigger;

    public bool toggle;
    bool triggered;

    public override void Interact()
    {
        if (toggle)
        {
            if (triggered)
            {
                Untrigger();
            }
            else
            {
                Trigger();
            }

            triggered = !triggered;
        } else
        {
            Trigger();
        }
    }

    public void Trigger()
    {
        onTrigger?.Invoke();
    }

    public void Untrigger()
    {
        onUntrigger?.Invoke();
    }

    public void RegisterTrigger(Action method)
    {
        onTrigger += method;
    }
    public void RegisterUntrigger(Action method)
    {
        onUntrigger += method;
    }
}
