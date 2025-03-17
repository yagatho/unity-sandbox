using UnityEngine;
using Project.Player.Controlls;
using Project.Systems.StateMachine;
using System.Collections.Generic;
using Project.Player.CameraC;

namespace Project.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        //VARS
        //-- Inspector
        [Header("Global")]
        public ControllerType characterControllerType = ControllerType.FPS;
        public List<Transform> virtualCameras;

        [Header("FPS")]
        public Transform cameraSpine;
        public Transform armsTransform;

        [Header("Souls-like")]
        public Transform SLcameraAnchor;

        [Header("TEMPORARY DEBUG")]
        public Bullet dedugBullet;

        //-- Hidden
        [HideInInspector] public Rigidbody myRigidbody;
        [HideInInspector] public Animator myAnimator;
        [HideInInspector] public Camera myCamera;
        [HideInInspector] public StateMachine stateMachine;
        [HideInInspector] public CameraController cameraController;
        private InputHandler input;


        //UNITY FUNCTIONS
        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
            myCamera = GetComponentInChildren<Camera>();
            myAnimator = GetComponentInChildren<Animator>();

            //Initialzie statemachine
            stateMachine = new StateMachine();

            //Debug
            Lookup.playerStateMachine = stateMachine;

            //Initialize input handler
            input = new InputHandler();
            cameraController = new CameraController(virtualCameras, myCamera, this);
            input.InitializeInput(this);
        }

        private void Update()
        {
            //Update the state machine
            stateMachine.Update();
        }

        private void LateUpdate()
        {
            input.LateUpdate();
            cameraController.LateUpdate();
        }
    }

    public enum ControllerType
    {
        FPS,
        Soulslike
    }
}
