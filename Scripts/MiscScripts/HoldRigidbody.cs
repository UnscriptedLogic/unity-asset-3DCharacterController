using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldRigidbody : MonoBehaviour
{
    [Header("Settings")]
    public float range = 3f;
    public float radius = 3f;
    public float force = 10f;
    public float breakDistance = 6f;

    [Space]
    public KeyCode activate = KeyCode.E;
    public LayerMask pickUpLayer;

    [Header("Components")]
    public Transform holdPos;
    public Transform raycastFrom;

    [Space(30)]
    public bool showDebugLines = true;

    float currentForce;

    PlayerInput playerInput;
    private GameObject currentlyHeld;
    private Rigidbody heldRb;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.RegisterKeyBind(PickUp, "Pick Up Object", activate, TriggerType.GetKeyDown);
        playerInput.RegisterKeyBind(Release, "Release Held Object", activate, TriggerType.GetKeyUp);
    }

    private void PickUp()
    {
        if (Physics.SphereCast(raycastFrom.position, radius, raycastFrom.forward, out RaycastHit hit, range, pickUpLayer))
        {
            currentlyHeld = hit.collider.transform.root.gameObject;
            heldRb = currentlyHeld.GetComponent<Rigidbody>();

            heldRb.useGravity = false;
        }
    }

    public void FixedUpdate()
    {
        if (currentlyHeld != null)
        {
            float dist = Vector3.Distance(holdPos.position, currentlyHeld.transform.position);
            Vector3 direction = holdPos.position - currentlyHeld.transform.position;

            currentForce = Mathf.SmoothStep(currentForce, force * 10f, dist / breakDistance);
            heldRb.velocity = direction.normalized * (currentForce * dist) * Time.fixedDeltaTime;

            //if we are grounded and if what we are standing on is the object we are holding, true, all else, false
            bool standingOnHeld = playerInput.isGrounded(out RaycastHit hit) ? hit.collider.gameObject == currentlyHeld : false;
            bool isTooFar = dist > breakDistance;

            if (standingOnHeld || isTooFar)
            {
                Release();
            }
        }
    }

    private void Release()
    {
        if (currentlyHeld != null)
        {
            heldRb.useGravity = true;

            currentlyHeld = null;
            heldRb = null;

            currentForce = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showDebugLines) { return; }
            
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.DrawSphere(holdPos.position, 0.25f);
    }
}
