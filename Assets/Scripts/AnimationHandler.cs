using UnityEngine;

namespace Project.Player.Animations
{
    public class AnimationHandler
    {
        //VARS
        private Animator myAnimator;



        //INIT
        public AnimationHandler(Animator anim)
        {
            myAnimator = anim;
        }



        //FUNCTIONS
        public void MoveAnim(Vector2 moveDelta, bool run)
        {
            myAnimator.SetFloat("Speed", moveDelta.magnitude);
            myAnimator.SetFloat("MoveDeltaX", moveDelta.x);
            myAnimator.SetFloat("MoveDeltaY", moveDelta.y);

            if (run && Vector2.Angle(Vector2.up, moveDelta.normalized) <= 45)
            {
                myAnimator.SetBool("Run", true);
            }
            else
                myAnimator.SetBool("Run", false);
        }

        public void PeekAnim(float peekDelta)
        {
            myAnimator.SetFloat("Peek", peekDelta);
        }

        public void Zoom()
        {
            myAnimator.SetFloat("ADSSpeed", Settings.adsSpeed);

            if (myAnimator.GetBool("ADS"))
                myAnimator.SetBool("ADS", false);
            else
                myAnimator.SetBool("ADS", true);
        }
    }
}
