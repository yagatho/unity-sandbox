using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.CameraC
{
    public class CameraController
    {
        //VARS
        private List<Transform> virtualCameras;
        private Camera myCamera;
        public int currentlyTrackedCamera = 0;
        private float lerpT = 1;
        private float lerpMult = 1;


        //INIT
        public CameraController(List<Transform> _vCams, Camera _myCam){
            virtualCameras = _vCams;
            myCamera = _myCam;
        }



        //FUNCTIONS
        public void SwitchADS()
        {
            if(currentlyTrackedCamera == 1)
                currentlyTrackedCamera = 0;
            else
                currentlyTrackedCamera = 1;

            lerpT = 0;
            lerpMult = Player.adsSpeed;
        }

        public void LerpToCamera(int index){
            currentlyTrackedCamera = index;
            lerpT = 0;
            lerpMult = 1;
        }

        public void LateUpdate(){
            //If there is lerp call finish the lerp before syncing position
            if(lerpT >= 1){
                myCamera.transform.position = virtualCameras[currentlyTrackedCamera].position;
                myCamera.transform.rotation = virtualCameras[currentlyTrackedCamera].rotation;
            }
            //Else just sync position and rotation with v camera
            else{
                myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, virtualCameras[currentlyTrackedCamera].position, lerpT);

                myCamera.transform.rotation = Quaternion.Slerp(myCamera.transform.rotation, virtualCameras[currentlyTrackedCamera].rotation, lerpT);

                lerpT +=Time.deltaTime * lerpMult;
            }
        }
    }
}