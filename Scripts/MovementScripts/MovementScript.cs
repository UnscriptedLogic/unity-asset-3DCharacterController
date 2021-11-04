using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementScript
{
    public abstract void PerformMove(float speed);

    public abstract void PerformMove(float speed, Vector3 direction);
}
