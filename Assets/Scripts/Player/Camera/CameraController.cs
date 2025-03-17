using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.CameraC
{
    public class CameraController
    {
        //VARS
        private List<Transform> virtualCameras;
        private Camera myCamera;
        private Player myPlayer;

        public int currentlyTrackedCamera = 0;
        private float lerpT = 1;
        private float lerpMult = 1;


        //INIT
        public CameraController(List<Transform> _vCams, Camera _myCam, Player _myPlayer)
        {
            virtualCameras = _vCams;
            myCamera = _myCam;
            myPlayer = _myPlayer;

        }



        //FUNCTIONS
        public void LerpToCamera(int index)
        {
            currentlyTrackedCamera = index;
            lerpT = 0;
            lerpMult = 1;
        }

        public void LateUpdate()
        {
            //Switch case for the all movement models
            switch (myPlayer.characterControllerType)
            {

                case ControllerType.FPS:
                    FpsCameraManagement();
                    break;

                case ControllerType.Soulslike:
                    TpsCameraManagement();
                    break;
            }
        }


        //Functions for each movement model
        //FPS
        private void FpsCameraManagement()
        {
            //If there is lerp call finish the lerp before syncing position
            if (lerpT >= 1)
            {
                myCamera.transform.position = virtualCameras[currentlyTrackedCamera].position;
                myCamera.transform.rotation = virtualCameras[currentlyTrackedCamera].rotation;
            }
            //Else just sync position and rotation with v camera
            else
            {
                myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, virtualCameras[currentlyTrackedCamera].position, lerpT);

                myCamera.transform.rotation = Quaternion.Slerp(myCamera.transform.rotation, virtualCameras[currentlyTrackedCamera].rotation, lerpT);

                lerpT += Time.deltaTime * lerpMult;
            }
        }

        public void SwitchADS()
        {
            if (currentlyTrackedCamera == 1)
                currentlyTrackedCamera = 0;
            else
                currentlyTrackedCamera = 1;

            lerpT = 0;
            lerpMult = Settings.adsSpeed;
        }


        //Souls-Like
        private void TpsCameraManagement()
        {
            //If there is lerp call finish the lerp before syncing position
            if (lerpT >= 1)
            {
                myCamera.transform.position = virtualCameras[currentlyTrackedCamera].position;
                myCamera.transform.rotation = virtualCameras[currentlyTrackedCamera].rotation;
            }
            //Else just sync position and rotation with v camera
            else
            {
                myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, virtualCameras[currentlyTrackedCamera].position, lerpT);

                myCamera.transform.rotation = Quaternion.Slerp(myCamera.transform.rotation, virtualCameras[currentlyTrackedCamera].rotation, lerpT);

                lerpT += Time.deltaTime * lerpMult;
            }
        }
    }
}
