using UnityEngine;

namespace Project.Player.Controlls
{
    public class MouseLook
    {
        //VARS
        //--Main rot components
        private float yaw = 0;
        private float pitch = 0;
        private float playerYaw = 0;
        private float playerYawVel = 0f;

        //--Camera refrence
        private Player myPlayer;
        private InputHandler myInput;

        //--Cache moveDelta
        private Vector2 moveDeltaCache;
        private Vector3 cameraColVel = Vector3.zero;
        private float cameraColTime = .3f;
        private Vector3 tarPos;
        private LayerMask mask;
        private RaycastHit colHit;


        //INIT
        public MouseLook(Player player, InputHandler input)
        {
            myPlayer = player;
            myInput = input;
            mask = LayerMask.GetMask("Ground");
            Utils.CursorLock();
        }



        //FUNCTIONS
        public void LookAround(Vector2 moveDelta)
        {
            if (!Settings.canMoveCamera)
                return;

            switch (myPlayer.characterControllerType)
            {
                case ControllerType.FPS:
                    FPSCameraMovement(moveDelta);
                    break;
                case ControllerType.Soulslike:
                    TPSCameraMovement(moveDelta);
                    break;


            }
        }

        public void Recoil()
        {
            myPlayer.armsTransform.rotation *= Quaternion.Euler(-2, 0, 0);
        }

        //Functions for FPS camera controller
        private void FPSCameraMovement(Vector2 moveDelta)
        {
            //Get yaw and pitch from the mouse movement delta
            yaw += moveDelta.x * Settings.mouseSensitivity;
            pitch -= moveDelta.y * Settings.mouseSensitivity;

            pitch = Mathf.Clamp(pitch, -85, 85);

            //Rotate player main object on y axis by yaw
            myPlayer.transform.rotation = Quaternion.Euler(0f, yaw, 0f);

            //Arm sway
            if (moveDelta.x != 0)
                myPlayer.armsTransform.localRotation *= Quaternion.Euler(0f, -moveDelta.x / 100 * Settings.armSwayStrenght, 0f);
            //Slerp back arms to normal position
            myPlayer.armsTransform.localRotation = Quaternion.Slerp(myPlayer.armsTransform.localRotation, Quaternion.identity, Time.deltaTime * Settings.armLerpSpeed);

            //Rotate the spine bone by pitch on x axis
            myPlayer.cameraSpine.localRotation = Quaternion.Euler(pitch, myPlayer.cameraSpine.localEulerAngles.y, myPlayer.cameraSpine.localEulerAngles.z);

        }

        //Functions for TPS camera controller
        private void TPSCameraMovement(Vector2 moveDelta)
        {
            //Get yaw and pitch from the mouse movement delta
            yaw += moveDelta.x * Settings.mouseSensitivity * Time.unscaledDeltaTime;

            pitch -= moveDelta.y * Settings.mouseSensitivity * Time.unscaledDeltaTime;

            pitch = Mathf.Clamp(pitch, -65, 85);

            //Rotate the camera anchor on y and x axis
            myPlayer.SLcameraAnchor.rotation = Quaternion.Euler(pitch, yaw, 0f);

            //Rotate the player body
            if (myInput.moveValue != Vector2.zero)
            {
                playerYaw = Mathf.SmoothDampAngle(playerYaw, yaw, ref playerYawVel, Settings.playerYawStrenght);
                myPlayer.transform.rotation = Quaternion.Euler(0, playerYaw, 0);

            }

            //Move cameras to player pos
            myPlayer.SLcameraAnchor.position = myPlayer.transform.position;


            //Move player camera on collision
            myPlayer.virtualCameras[0].localPosition = Vector3.SmoothDamp(myPlayer.virtualCameras[0].localPosition, tarPos, ref cameraColVel, .02f);

            //Camera collision things
            if (Physics.SphereCast(myPlayer.SLcameraAnchor.position, .5f, -myPlayer.SLcameraAnchor.forward, out colHit, Settings.cameraDistance, mask))
            {
                if (myPlayer.virtualCameras[0].localPosition.z < -Settings.cameraDistanceMin)
                {
                    tarPos = new Vector3(0, 0, -colHit.distance);
                }

            }
            else
            {
                if (myPlayer.virtualCameras[0].localPosition.z > -Settings.cameraDistance)
                {
                    tarPos = myPlayer.virtualCameras[0].localPosition - new Vector3(0, 0, Settings.cameraFalloffTime);
                }
            }
        }


    }
}
