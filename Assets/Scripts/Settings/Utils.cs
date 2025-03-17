using UnityEngine;
using System.Collections.Generic;

public static class Utils
{
    //Lock 
    public static void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Settings.canMoveCamera = true;
    }

    public static void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Settings.canMoveCamera = false;
    }

    public static List<Transform> GetChildWithTag(GameObject obj, string tag)
    {
        List<Transform> childWithTag = new List<Transform>();
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).CompareTag(tag))
                childWithTag.Add(obj.transform.GetChild(i));
        }

        return childWithTag;
    }
}
