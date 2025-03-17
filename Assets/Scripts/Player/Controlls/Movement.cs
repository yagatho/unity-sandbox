using Project.Systems.StateMachine;
using UnityEngine;

namespace Project.Player.Controlls
{
    public class Move : State
    {
        //Variables
        private InputHandler input;
        private Rigidbody targetBody;
        private Animator anim;

        private Vector3 currentVelocity;


        //Initialize
        public Move(InputHandler _myInput, Rigidbody _targetBody, Animator _anim)
        {
            input = _myInput;
            targetBody = _targetBody;
            anim = _anim;

            Lookup.playerRb = targetBody;
        }



        //STATE MACHINE
        public override void Enter()
        {
        }

        public override void Update()
        {
            if (input.canRun && Vector2.Angle(Vector2.up, input.moveValue.normalized) <= 45)
                RunLogic(input.moveValue, targetBody);
            else
                MoveLogic(input.moveValue, targetBody);

        }

        public override State Exit()
        {
            if (input.jump == true)
                return input.jumpState;

            if (input.moveValue == Vector2.zero)
                return input.idleState;

            return this;
        }



        //FUNCTIONS
        private void MoveLogic(Vector2 movementDelta, Rigidbody body)
        {
            MovePhysics(movementDelta, body);
        }

        private void RunLogic(Vector2 movementDelta, Rigidbody body)
        {
            RunPhysics(movementDelta, body);
        }

        private void MovePhysics(Vector2 movementDelta, Rigidbody body)
        {
            AcceleratedPosition(new Vector3(movementDelta.x * Settings.maxWalkingVelocity, body.linearVelocity.y, movementDelta.y * Settings.maxWalkingVelocity), body);
        }
        private void RunPhysics(Vector2 movementDelta, Rigidbody body)
        {
            AcceleratedPositionRun(new Vector3(movementDelta.x * Settings.maxRuningVelocity, body.linearVelocity.y, movementDelta.y * Settings.maxRuningVelocity), body);
        }

        private void AcceleratedPosition(Vector3 vectorToAccelerate, Rigidbody body)
        {
            float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float test = 2 + (Mathf.Cos(13 * time) + 1) * 3;

            Vector3 targetVelocity = body.transform.TransformDirection(vectorToAccelerate);
            Vector3 velocityChange = (targetVelocity - body.linearVelocity) * test;
            body.AddForce(velocityChange, ForceMode.Acceleration);
        }
        private void AcceleratedPositionRun(Vector3 vectorToAccelerate, Rigidbody body)
        {
            float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            float test = 2 + (Mathf.Sin(13 * time) + 1) * 3;

            Vector3 targetVelocity = body.transform.TransformDirection(vectorToAccelerate);
            Vector3 velocityChange = (targetVelocity - body.linearVelocity) * test;
            body.AddForce(velocityChange, ForceMode.Acceleration);

        }

    }

    public class Idle : State
    {
        private InputHandler input;

        //Initialize
        public Idle(InputHandler _myInput)
        {
            input = _myInput;
        }

        //STATE MACHINE
        public override void Enter()
        {
        }

        public override void Update()
        {
        }

        public override State Exit()
        {
            if (input.jump == true)
                return input.jumpState;


            if (input.moveValue != Vector2.zero)
                return input.moveState;

            return this;
        }
    }
    public class Jump : State
    {
        private InputHandler input;
        private Rigidbody myRigidbody;
        private Animator anim;
        private bool isGrounded = true;
        private bool canGroundAgain = false;

        private static float hitGroundDistance = 0.9f;

        //Initialize
        public Jump(InputHandler _myInput, Rigidbody _myRigidbody, Animator _myAnim)
        {
            input = _myInput;
            myRigidbody = _myRigidbody;
            anim = _myAnim;
        }

        //STATE MACHINE
        public override void Enter()
        {
            if (isGrounded)
                myRigidbody.AddForce(Vector3.up * 500 * Settings.jumpForce, ForceMode.Impulse);

            isGrounded = false;

            //Anim
            anim.SetBool("Jump", true);
        }

        public override void Update()
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Ground");

            if (Physics.SphereCast(myRigidbody.transform.position, .5f, Vector3.down, out hit, Mathf.Infinity, mask) && hit.distance < hitGroundDistance && canGroundAgain)
            {
                input.jump = false;
                canGroundAgain = false;
                isGrounded = true;

                input.ChangePlayerState();
            }

            //Check if player reached enought height to ground him again
            if (hit.distance > hitGroundDistance + .1f)
                canGroundAgain = true;
        }

        public override State Exit()
        {
            if (input.moveValue != Vector2.zero && !input.jump)
            {
                anim.SetBool("Jump", false);
                return input.moveState;
            }
            if (input.moveValue == Vector2.zero && !input.jump)
            {
                anim.SetBool("Jump", false);
                return input.idleState;
            }


            return this;
        }
    }


}
