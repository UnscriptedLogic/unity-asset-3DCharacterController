using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    float xRotation = 0f;

    public void RotateCamera(Vector2 mouseDirection, Vector2 mouseClamp, Transform cam)
    {
        xRotation -= mouseDirection.y;
        xRotation = Mathf.Clamp(xRotation, mouseClamp.x, mouseClamp.y);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseDirection.x);
    }
}
