using UnityEngine;
using TMPro;

public class Console
{
    private GameObject objRefrence;
    private TMP_InputField input;
    public static TextMeshProUGUI txt;

    //Public classes 
    public Console(GameObject _objRefrence, TMP_InputField _input, TextMeshProUGUI _text)
    {
        objRefrence = _objRefrence;
        input = _input;
        txt = _text;

        input.onSubmit.AddListener(EditFinished);
    }

    public static void SendConsoleMessage(string msg)
    {
        txt.text += ReturnMessage(messageType.Debug, msg);
    }


    public static string ReturnMessage(messageType m, string text)
    {
        switch (m)
        {
            case messageType.Error:
                return "<color=#ff3355>[ERROR] " + text + "\n";

            case messageType.Debug:
                return "<color=white>[DEBUG] " + text + "\n";

            case messageType.Warning:
                return "<color=#fba50f>[WARNING] " + text + "\n";

            case messageType.GUI:
                return "<color=#33e3ff>[GUI] " + text + "\n";
        }

        return null;
    }

    //Private classes
    private void EditFinished(string text)
    {
        input.text = "";

        if (text == "")
        {
            input.ActivateInputField();
            return;
        }

        //In into text
        txt.text += "<color=\"white\">> " + text + "\n";
        float floatValue = 0;

        //Main switch
        switch (text)
        {
            case "cursor":
                if (!DebugGlobal.cursorOn)
                {
                    txt.text += ReturnMessage(messageType.GUI, "Cursor: on");
                    DebugGlobal.cursorOn = true;
                    Utils.CursorUnlock();
                }
                else
                {
                    txt.text += ReturnMessage(messageType.GUI, "Cursor: off");
                    DebugGlobal.cursorOn = false;
                    Utils.CursorLock();
                }
                break;

            case "hit":
                if (!DebugGlobal.hitOn)
                {
                    txt.text += ReturnMessage(messageType.GUI, "Hit info: on");
                    DebugGlobal.hitOn = true;
                    Utils.CursorUnlock();
                }
                else
                {
                    txt.text += ReturnMessage(messageType.GUI, "Hit info: off");
                    DebugGlobal.hitOn = false;
                    Utils.CursorLock();
                }
                break;




            case "trajectory":
                if (!DebugGlobal.trajectoryOn)
                {
                    txt.text += ReturnMessage(messageType.GUI, "Bullet Trajectory: on");
                    DebugGlobal.trajectoryOn = true;
                    Shooting.GenerateTrajectory();
                }
                else
                {
                    txt.text += ReturnMessage(messageType.GUI, "Bullet Trajectory: off");
                    DebugGlobal.trajectoryOn = false;
                    Shooting.HideTrajectory();
                }
                break;


            case "state":
                if (!DebugGlobal.stateOn)
                {
                    txt.text += ReturnMessage(messageType.GUI, "Player state machine GUI: on");
                    DebugGlobal.stateOn = true;
                }
                else
                {
                    txt.text += ReturnMessage(messageType.GUI, "Player state machine GUI: off");
                    DebugGlobal.stateOn = false;
                }
                break;

            case "velocity":
                if (!DebugGlobal.velocityOn)
                {
                    txt.text += ReturnMessage(messageType.GUI, "Velocity GUI: on");
                    DebugGlobal.velocityOn = true;
                }
                else
                {
                    txt.text += ReturnMessage(messageType.GUI, "Velocity GUI: off");
                    DebugGlobal.velocityOn = false;
                }

                break;

            case string s when s.Contains("time") && float.TryParse(s.Replace("time", ""), out floatValue):
                txt.text += ReturnMessage(messageType.Debug, $"Time scale set to: {floatValue}");
                Time.timeScale = floatValue;

                break;


            default:
                txt.text += ReturnMessage(messageType.Error, "COMMAND NOT FOUND");
                break;
        }

        input.ActivateInputField();
    }
}


public enum messageType
{
    Error,
    Debug,
    GUI,
    Warning
}
