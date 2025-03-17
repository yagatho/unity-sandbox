using UnityEngine;
using System.Collections;
using Project.Player;
using UnityEngine.InputSystem;
using Project.Player.Controlls;

public class Shooting
{
    //Vars
    private Gun currGun;
    private Bullet currBullet;

    private Coroutine mainCo;
    private Player myPlayer;
    private MouseLook mouseLook;
    private float waitF;
    private static Vector3[] trajectory;
    private RaycastHit lastHitObject;
    private RaycastHit noneHit;
    private LayerMask mask;

    //Reload
    private float currReloadTime;
    private bool isReloading;

    //Weapon
    private int currAmmo;

    //Debug
    private static LineRenderer lr;

    //Init
    public Shooting(Player _myPlayer, MouseLook _mouseLook)
    {
        currGun = new M4A1();

        //TEMP
        GameObject gunObj = GameObject.FindGameObjectWithTag("Gun");
        currGun.SetObjectRef(gunObj);
        currAmmo = currGun.ammo;
        currBullet = _myPlayer.dedugBullet;


        mouseLook = _mouseLook;
        myPlayer = _myPlayer;
        mask = LayerMask.GetMask("Hitable");

        lr = currGun.barrelOut.GetComponent<LineRenderer>();
        GetTrajectory();
    }


    //Public functions
    public void Shoot(InputAction.CallbackContext context)
    {
        if (mainCo != null)
            myPlayer.StopCoroutine(mainCo);

        waitF = 1f / (currGun.fireRate / 60f);
        mainCo = myPlayer.StartCoroutine(ShootCo());
    }

    public void ShootStop(InputAction.CallbackContext context)
    {
        myPlayer.StopCoroutine(mainCo);
    }

    //Debug
    public static void GenerateTrajectory()
    {
        lr.positionCount = 100;
        lr.SetPositions(trajectory);
    }

    public static void HideTrajectory()
    {
        lr.positionCount = 0;
    }

    //Main balistic calc function
    public static float Balistics(float v, float g, float x)
    {
        return -g * ((x * x) / (2 * v * v));
    }

    //Main Coroutine
    private IEnumerator ShootCo()
    {
        while (true)
        {
            if (isReloading)
                yield return new WaitUntil(() => isReloading == false);

            //Check ammo
            if (currAmmo <= 0)
            {
                myPlayer.StartCoroutine(ReloadCo());
                yield return new WaitUntil(() => isReloading == false);
            }

            //Recoil
            mouseLook.Recoil();

            //Shoot
            ShootingLogic();

            //Wait the firerate
            yield return new WaitForSeconds(waitF);
        }

    }

    private IEnumerator ReloadCo()
    {
        isReloading = true;
        currReloadTime = currGun.reloadTime;

        while (currReloadTime >= 0)
        {
            currReloadTime -= Time.deltaTime;
            yield return null;
        }

        currAmmo = currGun.ammo;
        isReloading = false;
    }

    //Private functions
    private void ShootingLogic()
    {
        //Decrement the ammo
        currAmmo--;

        //Loop throght all the points
        for (int i = 0; i < trajectory.Length - 1; i++)
        {
            Vector3 point1 = currGun.barrelOut.TransformPoint(trajectory[i]);
            Vector3 point2 = currGun.barrelOut.TransformPoint(trajectory[i + 1]);

            Vector3 dir = point2 - point1;

            //Obj hit
            if (Physics.Raycast(point1, dir, out lastHitObject, 10, mask))
            {
                Hit();
                Lookup.hitObj = lastHitObject;
                break;
            }

            Lookup.hitObj = noneHit;
        }
    }

    private void Hit()
    {
        Rigidbody rb = lastHitObject.collider.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
    }

    private void GetTrajectory()
    {
        //reset arrays
        trajectory = new Vector3[100];

        for (int i = 0; i < 100; i += 1)
        {
            //calculate traj point
            Vector3 pos = new Vector3(0, Balistics(400f, 9.2f, i * 10), -i * 10);

            //Save the trajectory and pointer traj
            trajectory[i] = pos;
        }
    }

}
