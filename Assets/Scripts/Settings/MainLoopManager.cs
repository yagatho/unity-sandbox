using UnityEngine;

public class MainLoopManager : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 999;
    }

}
