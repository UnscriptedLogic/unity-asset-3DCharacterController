using UnityEngine;
public class Sprint : MoveModBase
{
    float speed;

    public Sprint(float sprintSpeed)
    {
        speed = sprintSpeed;
    }

    public override void Trigger()
    {
        throw new System.NotImplementedException();
    }    
    
    public override void Trigger(ref float var)
    {
        var *= speed;
    }
}
