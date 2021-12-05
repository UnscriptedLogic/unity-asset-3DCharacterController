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

[Serializable]
public class MouseBind
{
    public string bindName;
    public int button;
    public TriggerType trigger;
    public event Action mouseEvent;

    public MouseBind(string bindName, int button, TriggerType trigger, Action function)
    {
        this.bindName = bindName;
        this.button = button;
        this.trigger = trigger;

        mouseEvent += function;
    }

    public void TriggerEvent() { mouseEvent?.Invoke(); }

    public bool hasSubcribers()
    {
        return mouseEvent?.GetInvocationList().Length > 0;
    }
}

public class PlayerInput : MonoBehaviour
{
    [Header("Components")]
    public CharacterController controller;
    public Camera cam;

    public List<Keybind> keybinds = new List<Keybind>();
    public List<MouseBind> mouseBinds = new List<MouseBind>();

    public Vector3 GetDirectionalInput()
    {

        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.forward * zInput + transform.right * xInput;

        return direction.normalized;
    }

    public Vector2 GetMouseInput(Vector2 mouseSens)
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens.x * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens.y * Time.deltaTime;
        Vector3 mouseDir = new Vector2(mouseX, mouseY);

        return mouseDir;
    }

    public virtual void Update()
    {
        KeyBindMethod();
        MouseBindMethod();

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

    public void MouseBindMethod()
    {
        foreach (MouseBind mouseBind in mouseBinds)
        {
            switch (mouseBind.trigger)
            {
                case TriggerType.GetKey:
                    if (Input.GetMouseButton(mouseBind.button))
                    {
                        mouseBind.TriggerEvent();
                    }
                    break;
                case TriggerType.GetKeyDown:
                    if (Input.GetMouseButtonDown(mouseBind.button))
                    {
                        mouseBind.TriggerEvent();
                    }
                    break;
                case TriggerType.GetKeyUp:
                    if (Input.GetMouseButtonUp(mouseBind.button))
                    {
                        mouseBind.TriggerEvent();
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

    public void RegisterMouseBind(Action function, string bindName = "Unnamed Bind", int buttonSide = 0, TriggerType trigger = TriggerType.GetKeyDown)
    {
        mouseBinds.Add(new MouseBind(bindName, buttonSide, trigger, function));
    }

    public void UnRegisterMousebind(Action method, string bindName)
    {
        for (int i = 0; i < mouseBinds.Count; i++)
        {
            if (mouseBinds[i].bindName == bindName)
            {
                mouseBinds[i].mouseEvent -= method;

                if (!mouseBinds[i].hasSubcribers())
                {
                    mouseBinds.RemoveAt(i);
                }
            }
        }
    }

    public Ray GetCamMouseRay()
    {
        return cam.ScreenPointToRay(Input.mousePosition);
    }

    public bool FindComponentRaycast<T>(out T component, float distance = Mathf.Infinity)
    {
        GameObject lookAt = RaycastCamera(distance);
        if (lookAt)
        {
            if (lookAt.GetComponent<T>() != null)
            {
                component = lookAt.GetComponent<T>();
                return true;
            }
        }

        component = transform.GetComponent<T>();
        return false;
    }

    public bool FindComponentRaycast<T>(out T component, out GameObject target,float distance = Mathf.Infinity)
    {
        GameObject lookAt = RaycastCamera(distance);
        if (lookAt)
        {
            if (lookAt.GetComponent<T>() != null)
            {
                component = lookAt.GetComponent<T>();
                target = lookAt;
                return true;
            }
        }

        component = transform.GetComponent<T>();
        target = null;
        return false;
    }

    #region Camera Raycasting

    public GameObject RaycastCamera(float distance = Mathf.Infinity)
    {
        if (Physics.Raycast(GetCamMouseRay(), out RaycastHit hit, distance))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    public GameObject RaycastCamera(LayerMask layer, float distance = Mathf.Infinity)
    {
        if (Physics.Raycast(GetCamMouseRay(), out RaycastHit hit, distance, layer))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    public GameObject RaycastCamera(out RaycastHit newHit, float distance = Mathf.Infinity)
    {
        if (Physics.Raycast(GetCamMouseRay(), out newHit, distance))
        {
            return newHit.collider.gameObject;
        }

        return null;
    }

    #endregion

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
}
