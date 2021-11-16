using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    GetKey,
    GetKeyDown,
    GetKeyUp
}

[Serializable]
public class Keybind
{
    public string bindName;
    public KeyCode keyCode;
    public TriggerType trigger;
    public event Action keyEvent;

    public Keybind(string _name, KeyCode _key, TriggerType _type, Action method)
    {
        bindName = _name;
        keyCode = _key;
        trigger = _type;

        keyEvent += method;
    }

    public void TriggerEvent() { keyEvent?.Invoke(); }
}

public class PlayerInput : MonoBehaviour
{
    [Header("Refinements")]
    public float groundCheckOffset;
    public LayerMask groundLayer;

    [Header("Components")]
    public CharacterController controller;

    public List<Keybind> keybinds = new List<Keybind>();

    Vector3 prevPosition;

    float displacement;
    float velocity;

    private void Start()
    {
        prevPosition = transform.position;
    }

    public Vector3 GetDirectionalInput()
    {

        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.forward * zInput + transform.right * xInput;

        return direction.normalized;
    }

    public Vector2 GetMouseInput(Vector2 mouseSens)
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSens.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens.y * Time.deltaTime;
        Vector3 mouseDir = new Vector2(mouseX, mouseY);

        return mouseDir;
    }

    private void Update()
    {
        KeyBindMethod();
        CalculateVelocity();

        prevPosition = transform.position;

    }

    public void CalculateVelocity()
    {
        displacement = Vector3.Distance(transform.position, prevPosition);
        velocity = displacement / Time.deltaTime;
    }

    private void KeyBindMethod()
    {
        foreach (Keybind keybind in keybinds)
        {
            switch (keybind.trigger)
            {
                case TriggerType.GetKey:
                    if (Input.GetKey(keybind.keyCode))
                    {
                        keybind.TriggerEvent();
                    }
                    break;

                case TriggerType.GetKeyDown:
                    if (Input.GetKeyDown(keybind.keyCode))
                    {
                        keybind.TriggerEvent();
                    }
                    break;

                case TriggerType.GetKeyUp:
                    if (Input.GetKeyUp(keybind.keyCode))
                    {
                        keybind.TriggerEvent();
                    }
                    break;

                default:
                    break;
            }
        }
    }

    //Registers a function to a keybind
    public void RegisterKeyBind(Action function, 
                                string bindName = "Unnamed Keybind", 
                                KeyCode keyCode = KeyCode.None, 
                                TriggerType triggerType = TriggerType.GetKey
                                )
    {
        keybinds.Add(new Keybind(bindName, keyCode, triggerType, function));
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, controller.height * groundCheckOffset);
    }

    public bool isGrounded(out RaycastHit hit)
    {
        bool value = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, controller.height * groundCheckOffset);
        hit = hitInfo;
        return value;
    }
    public float GetVelocity() { return velocity; }

    #region Relative Raycasting
    public GameObject RaycastForward(float distance = Mathf.Infinity)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hitInfo, distance);

        return hitInfo.collider.gameObject;
    }

    public GameObject RaycastForward(LayerMask layer, float distance = Mathf.Infinity)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hitInfo, distance, layer);

        return hitInfo.collider.gameObject;
    }

    public GameObject RaycastForward(Vector3 origin, Vector3 direction, float distance = Mathf.Infinity)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(origin, direction);
        Physics.Raycast(ray, out hitInfo, distance);

        return hitInfo.collider.gameObject;
    }

    public GameObject RaycastForward(Vector3 origin, Vector3 direction, LayerMask layerMask, float distance = Mathf.Infinity)
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(origin, direction);
        Physics.Raycast(ray, out hitInfo, distance, layerMask);

        return hitInfo.collider.gameObject;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down * (controller.height * groundCheckOffset));
    }
}
