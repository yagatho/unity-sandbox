using Project.Player.Animations;
using Project.SettingsGroup;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Project.Player.Controlls
{
    public class InputHandler
    {
        //VARS
        private PlayerControlls controlsActions;
        private Player myPlayer;

        //--States
        public Move moveState;
        public Idle idleState;
        public Jump jumpState;
        private MouseLook mouseLook;
        private AnimationHandler animHandler;

        //--Inputs Move
        public Vector2 moveValue = Vector2.zero;
        public bool canRun = false;

        //--Jumping
        public bool jump = false;

        //--Helers
        private Coroutine moveAnimInterpolation;
        private Coroutine peekAnimInterpolation;
        private float currentMoveX = 0;
        private float currentPeek = 0;


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

            controlsActions.Player.Jump.started += Jump;

            //Movement model exclusive actions
            switch (myPlayer.characterControllerType)
            {
                case ControllerType.FPS:

                    controlsActions.Player.ADS.started += ADS;

                    controlsActions.Player.Peek.started += Peek;
                    controlsActions.Player.Peek.canceled += PeekStop;
                    break;

                case ControllerType.Soulslike:
                    break;
            }
        }

        private void InitializeStates()
        {
            moveState = new Move(this, myPlayer.myRigidbody, myPlayer.myAnimator);
            idleState = new Idle(this);
            jumpState = new Jump(this, myPlayer.myRigidbody, myPlayer.myAnimator);
            mouseLook = new MouseLook(myPlayer, this);
            animHandler = new AnimationHandler(myPlayer.myAnimator);

            myPlayer.stateMachine.Start(idleState);
        }

        //Used currently only for mouse movement
        public void LateUpdate()
        {
            mouseLook.LookAround(controlsActions.Player.Look.ReadValue<Vector2>());
        }


        //INPUT ACTIONS
        private void MovementStartAction(InputAction.CallbackContext context)
        {
            //Start new movement call coroutine
            moveValue = context.ReadValue<Vector2>();
            myPlayer.stateMachine.ChangeState();
        }

        private void MovementAction(InputAction.CallbackContext context)
        {
            //Start new movement call coroutine
            moveValue = context.ReadValue<Vector2>();
            MoveAnimLerp(moveValue, canRun);
        }

        private void MovementStopAction(InputAction.CallbackContext context)
        {
            moveValue = Vector2.zero;
            myPlayer.stateMachine.ChangeState();
            MoveAnimLerp(moveValue, canRun);
        }

        private void RunButtonHold(InputAction.CallbackContext context)
        {
            canRun = true;
            MoveAnimLerp(moveValue, canRun);
        }

        private void RunButtonHoldStop(InputAction.CallbackContext context)
        {
            canRun = false;
            MoveAnimLerp(moveValue, canRun);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            jump = true;

            myPlayer.stateMachine.ChangeState();
        }


        //FPS exclusive
        private void ADS(InputAction.CallbackContext context)
        {
            myPlayer.cameraController.SwitchADS();
            animHandler.Zoom();
        }

        private void Peek(InputAction.CallbackContext context)
        {
            float peekDelta = context.ReadValue<float>();
            PeekAnimLerp(peekDelta);
        }

        private void PeekStop(InputAction.CallbackContext context)
        {
            PeekAnimLerp(0);
        }

        //Souls-like exclusive


        //Interpolation for move animation
        private void MoveAnimLerp(Vector2 moveDelta, bool canRun)
        {
            //Interrupt
            if (moveAnimInterpolation != null)
                myPlayer.StopCoroutine(moveAnimInterpolation);

            moveAnimInterpolation = myPlayer.StartCoroutine(MoveAnimLerpCo(moveDelta, canRun));
        }

        private IEnumerator MoveAnimLerpCo(Vector2 moveDelta, bool canRun)
        {
            float t = 0;

            while (t < 1)
            {
                currentMoveX = Mathf.Lerp(currentMoveX, moveDelta.x, t);
                animHandler.MoveAnim(new Vector2(currentMoveX, moveDelta.y), canRun);

                t += Time.deltaTime;
                yield return null;
            }
        }

        //Interpolation for peek animation
        private void PeekAnimLerp(float peekDelta)
        {
            //Interrupt
            if (peekAnimInterpolation != null)
                myPlayer.StopCoroutine(peekAnimInterpolation);

            peekAnimInterpolation = myPlayer.StartCoroutine(PeekAnimLerpCo(peekDelta));
        }

        private IEnumerator PeekAnimLerpCo(float peekDelta)
        {
            float t = 0;

            while (t < 1)
            {
                currentPeek = Mathf.Lerp(currentPeek, peekDelta, t);
                animHandler.PeekAnim(currentPeek);

                t += Time.deltaTime * Settings.peekSpeed;
                yield return null;
            }
        }

        //Force change player state
        public void ChangePlayerState()
        {
            myPlayer.stateMachine.ChangeState();
        }
    }
}
