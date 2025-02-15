using UnityEngine;

public class RagdollTest : MonoBehaviour
{
    public Rigidbody forceSr;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            RagdollStart();
        }
    }


    private void RagdollStart()
    {
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();

        foreach (var r in rb)
        {
            r.isKinematic = false;
        }

        forceSr.AddForce(Vector3.up * 200, ForceMode.Impulse);
    }
}
