using Project.Systems.StateMachine;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Player.Controlls{
    public class Move: State
    {
        //Variables
        private InputHandler input;
        private Rigidbody targetBody;


        //Initialize
        public Move(InputHandler _myInput, Rigidbody _targetBody)
        {
            input = _myInput;
            targetBody = _targetBody;
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
            AcceleratedPosition(new Vector3(movementDelta.x * Player.maxWalkingVelocity, body.linearVelocity.y, movementDelta.y * Player.maxWalkingVelocity), body);
        }
        private void RunPhysics(Vector2 movementDelta, Rigidbody body)
        {
            AcceleratedPositionRun(new Vector3(movementDelta.x * Player.maxRuningVelocity, body.linearVelocity.y, movementDelta.y * Player.maxRuningVelocity), body);
        }

        private void AcceleratedPosition(Vector3 vectorToAccelerate, Rigidbody body){
            input.interpolationStep += Time.deltaTime * Player.acceleration;
            body.linearVelocity = Vector3.Lerp(input.interpolationBase, targetBody.transform.TransformDirection(vectorToAccelerate), input.interpolationStep);
        }
        private void AcceleratedPositionRun(Vector3 vectorToAccelerate, Rigidbody body)
        {
            input.interpolationStep += Time.deltaTime * Player.runAcceleration;
            body.linearVelocity = Vector3.Lerp(input.interpolationBase, targetBody.transform.TransformDirection(vectorToAccelerate), input.interpolationStep);
        }
    }

    public class Idle: State{
        private InputHandler input;

        //Initialize
        public Idle(InputHandler _myInput){
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
            if(input.moveValue != Vector2.zero)
                return input.moveState;
            
            return this;
        }
    }

}