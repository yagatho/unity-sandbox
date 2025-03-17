using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class DebugManager : MonoBehaviour
{
    //Shown vars
    [Header("Console")]
    [SerializeField] private GameObject consoleObject;
    [SerializeField] private TMP_InputField consoleInput;
    [SerializeField] private TextMeshProUGUI consoleText;

    //Private vars
    [HideInInspector] public List<string> guiList = new List<string>();
    private Console console;

    //Start
    private void Start()
    {
        console = new Console(consoleObject, consoleInput, consoleText);
    }


    //Public
    public void ChangeConsoleState(InputAction.CallbackContext context)
    {

        switch (consoleObject.activeSelf)
        {
            case false:
                consoleObject.SetActive(true);
                consoleInput.ActivateInputField();
                break;

            case true:
                consoleObject.SetActive(false);
                break;
        }

    }

    //Private
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        //Draw all guis
        if (DebugGlobal.velocityOn)
        {
            float f = (Mathf.Abs(Lookup.playerRb.linearVelocity.x) + Mathf.Abs(Lookup.playerRb.linearVelocity.z));
            string msg = $"Velocity horizontal: {f:#0.00}";
            GUI.Label(new Rect(10, 10, 500, 30), msg, style);

            msg = $"Velocity vertical: {Lookup.playerRb.linearVelocity.y:#0.00}";
            GUI.Label(new Rect(10, 42, 500, 30), msg, style);
        }

        if (DebugGlobal.stateOn)
        {
            string msg = $"Player move state: {Lookup.playerStateMachine.GetState()}";
            GUI.Label(new Rect(10, 74, 500, 30), msg, style);
        }

        if (DebugGlobal.hitOn)
        {
            string msg = $"Last hit object: {Lookup.hitObj.collider?.name}";
            GUI.Label(new Rect(10, 106, 500, 30), msg, style);
        }


    }

    private void Vel()
    {
        guiList.Add("Velocity: " + Lookup.playerRb.linearVelocity);
    }

}

