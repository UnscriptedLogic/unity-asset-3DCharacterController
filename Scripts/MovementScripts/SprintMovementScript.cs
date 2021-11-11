using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintMovementScript : MovementScript
{
    InputScript inputScript;

    List<MoveModBase> moveMods = new List<MoveModBase>();

    public SprintMovementScript(InputScript _inputScript, Rigidbody _rb, float sprintSpeed = 2f, float jumpforce = 10f)
    {
        inputScript = _inputScript;

        moveMods.Add(new Sprint(sprintSpeed));
        moveMods.Add(new Jump(_rb, jumpforce));

        inputScript.Register(moveMods[1].Trigger, 1);
    }

    public override void MoveCharacter(Transform _transform, float speed, Vector3 direction, Rigidbody rb)
    {
        if (inputScript.inputs.Count > 0)
        {
            if (inputScript.inputs[0].keyActive)
            {
                moveMods[0].Trigger(ref speed);
            }
        }

        base.MoveCharacter(_transform, speed, direction, rb);
    }
}
