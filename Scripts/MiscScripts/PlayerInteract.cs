using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInteract : MonoBehaviour
{
    PlayerController controller;
    public KeyCode interactKey = KeyCode.E;
    public float interactDistance = 2f;

    private void Start()
    {
        controller = GetComponent<PlayerController>();

        controller.playerInput.RegisterKeyBind(AttemptInteract, "Interact", interactKey, TriggerType.GetKeyDown);
    }

    private void AttemptInteract()
    {
        if (controller.playerInput.FindComponentRaycast(out ItemObject component, interactDistance))
        {
            component.Interact();
        }
    }
}