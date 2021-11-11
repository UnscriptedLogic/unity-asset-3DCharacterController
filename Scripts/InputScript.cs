using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    [Serializable]
    public class Inputs
    {
        public string keybindName;
        public KeyCode triggerKey;
        [HideInInspector] public bool keyActive;
        public event Action eventTrigger;

        public void TriggerEvent()
        {
            eventTrigger?.Invoke();
        }
    }

    public List<Inputs> inputs = new List<Inputs>();

    private void Update()
    {
        for (int i = 0; i < inputs.Count; i++)
        {
            bool value = Input.GetKey(inputs[i].triggerKey);
            inputs[i].keyActive = value;
            if (value)
            {
                inputs[i].TriggerEvent();
            }
        }

        GatherInputAxis();
    }

    public Vector2 GatherMouseAxis(Vector2 mouseSens)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens.x * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens.y * Time.deltaTime;
        Vector2 mouseDir = new Vector3(mouseX, mouseY);
        return mouseDir;
    }

    public Vector3 GatherInputAxis()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.forward * zInput + transform.right * xInput;
        return direction;
    }

    public void Register(Action method, int index)
    {
        inputs[index].eventTrigger += method;
    }
}
