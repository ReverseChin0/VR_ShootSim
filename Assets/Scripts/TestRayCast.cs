using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRayCast : MonoBehaviour
{

    public Transform Barrel;
    private void Update()
    {
       
        
        Debug.DrawRay(Barrel.position, Barrel.forward * 1000f, Color.magenta);
    }

    void Shoot()
    {
        //Projectile target = projectilePool.mProjectil[0];
        /*fire.Play();
        smoke.Play();*/
        RaycastHit hit;
        if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, Mathf.Infinity /*rango*/))
        {
            Debug.DrawRay(Barrel.position, Barrel.forward * hit.distance, Color.red);
            Debug.Log(hit.collider.name);
        }


        //Debug.Log("Se disparoooo");
        // target.Disparar(this);
    }
}
