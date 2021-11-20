using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGUI : MonoBehaviour
{
    PlayerController pController;

    public KeyCode settingsKey = KeyCode.LeftAlt;

    public GameObject settingsPage;
    public GameObject debugPage;

    public TextMeshProUGUI debugTxt;

    private void Start()
    {
        pController = GetComponent<PlayerController>();
        settingsPage.SetActive(false);

        pController.playerInput.RegisterKeyBind(ToggleSettings, "Toggle Settings Menu", settingsKey, TriggerType.GetKeyDown);
    }

    private void Update()
    {
        if (debugPage.activeInHierarchy)
        {
            debugTxt.text =
                "Speed: " + pController.playerMovement.GetSpeed().ToString() +
                "\nJump: " + pController.playerMovement.GetJump().ToString() +
                "\nMovement State: " + pController.movementState.ToString() +
                "\nisGrounded: " + pController.playerInput.isGrounded().ToString();
        }
    }

    private void ToggleSettings()
    {
        settingsPage.SetActive(!settingsPage.activeInHierarchy);
        pController.LockCursor();
    }

    public void ShowDebug()
    {
        debugPage.SetActive(!debugPage.activeInHierarchy);
    }
}
