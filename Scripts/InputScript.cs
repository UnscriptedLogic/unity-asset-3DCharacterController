using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    public delegate void ShiftPressed();
    public event ShiftPressed onShiftPressed;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            onShiftPressed?.Invoke();
        }
    }
}
