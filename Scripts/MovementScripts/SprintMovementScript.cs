using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintMovementScript : MovementScript
{
    Sprint sprint;
    InputScript inputScript;
    int inputIndex;

    public SprintMovementScript(InputScript _inputScript, int _inputIndex)
    {
        sprint = new Sprint();
        inputScript = _inputScript;
        inputIndex = _inputIndex;
    }

    public override void MoveCharacter(Transform _transform, float speed, Vector3 direction, Rigidbody rb)
    {
        if (inputScript.inputs[inputIndex].keyActive)
        {
            sprint.Trigger(ref speed);
        }

        base.MoveCharacter(_transform, speed, direction, rb);
    }
}
