using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shootClip;
    public AudioClip reloadClip;

    public float damage = 25f;
    public int startAmmoRemain = 100;
    public int magCapacity = 25;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;

    public float fireDistance = 50f;
}
