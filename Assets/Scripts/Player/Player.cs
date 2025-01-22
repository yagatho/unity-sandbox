using UnityEngine;
using Project.Player.Controlls;
using Project.Systems.StateMachine;
using System.Collections.Generic;
using Project.Player.CameraC;

namespace Project.Player{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {   
        //VARS
        //--Statistics
        //---Basic Movement
        [SerializeField] public const float maxWalkingVelocity = 2f;
        [SerializeField] public const float maxRuningVelocity = 5f;
        [SerializeField] public const float acceleration = 3f;
        [SerializeField] public const float runAcceleration = 1f;
        //---Mouse Look
        [SerializeField] public const float mouseSensitivity = 6f;
        [SerializeField] public const float armLerpSpeed = 5f;
        [SerializeField] public const float armSwayStrenght = .5f;
        [SerializeField] public const float adsSpeed = 1f;

        //--Refrences
        public Transform cameraSpine;
        public Transform armsTransform;
        public List<Transform> virtualCameras;

        //--Hidden
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

            //Initialize input handler
            input = new InputHandler();
            cameraController = new CameraController(virtualCameras, myCamera);
            input.InitializeInput(this);
        }

        private void Update(){
            //Update the state machine
            stateMachine.Update();
        }

        private void LateUpdate(){
            input.LateUpdate();
            cameraController.LateUpdate();
        }
    }

}