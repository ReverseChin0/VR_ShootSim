using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Weapon : MonoBehaviour
{
    //en el gameObject de una arma

    public SteamVR_Action_Boolean Disparar = null;
    SteamVR_Behaviour_Pose Pose = null;

    public int Force = 10;

    public Transform Barrel;


    public GameObject ProjectilePrefab;
    ProjectilePool projectilePool = null;   

    private void Awake()
    {
        Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        projectilePool = new ProjectilePool(ProjectilePrefab, 10);
    }

    private void Update()
    {
        if (Disparar.GetStateDown(Pose.inputSource))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Projectile target = projectilePool.mProjectil[0];
        target.Disparar(this);
    }


}
