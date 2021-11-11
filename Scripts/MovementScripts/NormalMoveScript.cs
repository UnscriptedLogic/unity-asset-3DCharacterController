using UnityEngine;

public class NormalMoveScript : MovementScript
{
    public override void MoveCharacter(Transform _transform, float speed, Vector3 direction, Rigidbody rb)
    {
        base.MoveCharacter(_transform, speed, direction, rb);
    }
}
