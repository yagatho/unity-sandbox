using UnityEngine;

namespace Project.Player.Controlls
{
    public class MouseLook
    {
        //VARS
        //--Main rot components
        private float yaw = 0;
        private float pitch = 0;

        //--Camera refrence
        private Player myPlayer;



        //INIT
        public MouseLook(Player player)
        {
            myPlayer = player;

            CursorLock();
        }



        //FUNCTIONS
        public void LookAround(Vector2 moveDelta)
        {
            //Get yaw and pitch from the mouse movement delta
            yaw += moveDelta.x * Player.mouseSensitivity * Time.unscaledDeltaTime;
            pitch -= moveDelta.y * Player.mouseSensitivity * Time.unscaledDeltaTime;

            pitch = Mathf.Clamp(pitch, -85, 85);

            //Rotate player main object on y axis by yaw
            myPlayer.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
            //Arm sway
            if (moveDelta.x != 0)
                myPlayer.armsTransform.localRotation *= Quaternion.Euler(0f, -moveDelta.x / 100 * Player.armSwayStrenght, 0f);
            //Slerp back arms to normal position
            myPlayer.armsTransform.localRotation = Quaternion.Slerp(myPlayer.armsTransform.localRotation, Quaternion.identity, Time.deltaTime * Player.armLerpSpeed);

            //Rotate the spine bone by pitch on x axis
            myPlayer.cameraSpine.localRotation = Quaternion.Euler(pitch, myPlayer.cameraSpine.localEulerAngles.y, myPlayer.cameraSpine.localEulerAngles.z);
        }


        //Lock 
        public void CursorLock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
