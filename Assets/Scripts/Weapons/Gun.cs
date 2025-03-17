using UnityEngine;
using System.Collections.Generic;

public abstract class Gun
{
    public GameObject objectRefrence { get; set; }
    public Transform barrelOut { get; set; }

    //Abstracions
    public abstract int fireRate { get; }
    public abstract int ammo { get; }
    public abstract int reloadTime { get; }

    //Functions
    public void SetObjectRef(GameObject obj)
    {
        objectRefrence = obj;

        List<Transform> childWithTag = Utils.GetChildWithTag(obj, "BarrelOut");
        barrelOut = childWithTag[0];
    }

}


public class M4A1 : Gun
{
    public override int fireRate => 800;
    public override int ammo => 30;
    public override int reloadTime => 2;
}
