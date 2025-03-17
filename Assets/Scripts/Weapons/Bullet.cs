using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Guns/Bullet")]
public class Bullet : ScriptableObject
{
    public string bulletName;
    public Caliber caliber;
}

public enum Caliber
{
    Nato556,
}
