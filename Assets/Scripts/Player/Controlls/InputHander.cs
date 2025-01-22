using Project.Player.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player.Controlls{
    public class InputHandler
    {
        //VARS
        private PlayerControlls controlsActions;
        private Player myPlayer;

        //--States
        public Move moveState;
        public Idle idleState;
        private MouseLook mouseLook;
        private AnimationHandler animHandler;

        //--Inputs Move
        public Vector2 moveValue = Vector2.zero;
        public bool canRun = false;
        public Vector3 interpolationBase = Vector3.zero;
        public float interpolationStep = 0;



        //FUNCTIONS
        public void InitializeInput(Player _player)
        {
            controlsActions = new PlayerControlls();
            controlsActions.Player.Enable();

            myPlayer = _player;
            InitializeStates();

            controlsActions.Player.Move.started += MovementStartAction;
            controlsActions.Player.Move.performed += MovementAction;
            controlsActions.Player.Move.canceled += MovementStopAction;

            controlsActions.Player.Run.started += RunButtonHold;
            controlsActions.Player.Run.canceled += RunButtonHoldStop;

            controlsActions.Player.ADS.started += ADS;
        }

        private void InitializeStates(){
            moveState = new Move(this, myPlayer.myRigidbody);
            idleState = new Idle(this);
            mouseLook = new MouseLook(myPlayer);
            animHandler = new AnimationHandler(myPlayer.myAnimator);

            myPlayer.stateMachine.Start(idleState);
        }

        public void LateUpdate()
        {
            mouseLook.LookAround(controlsActions.Player.Look.ReadValue<Vector2>());
        }



        //INPUT ACTIONS
        private void MovementStartAction(InputAction.CallbackContext context)
        {
            //Grab old rigidbody velocity as interpolation anchor
            interpolationStep = 0;
            interpolationBase = myPlayer.myRigidbody.linearVelocity;

            //Start new movement call coroutine
            moveValue = context.ReadValue<Vector2>();
            myPlayer.stateMachine.ChangeState();
        }
        private void MovementAction(InputAction.CallbackContext context)
        {
            //Grab old rigidbody velocity as interpolation anchor
            interpolationStep = 0;
            interpolationBase = myPlayer.myRigidbody.linearVelocity;

            //Start new movement call coroutine
            moveValue = context.ReadValue<Vector2>();
            animHandler.MoveAnim(moveValue, canRun);
        }
        private void MovementStopAction(InputAction.CallbackContext context)
        {
            moveValue = Vector2.zero;
            myPlayer.stateMachine.ChangeState();
            animHandler.MoveAnim(Vector2.zero, canRun);
        }

        private void RunButtonHold(InputAction.CallbackContext context)
        {
            canRun = true;
            animHandler.MoveAnim(moveValue, canRun);
        }
        private void RunButtonHoldStop(InputAction.CallbackContext context)
        {
            canRun = false;
            animHandler.MoveAnim(moveValue, canRun);
        }

        private void ADS(InputAction.CallbackContext context)
        {
            myPlayer.cameraController.SwitchADS();
            animHandler.Zoom();
        }
    }
} 