using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class Weapon : MonoBehaviour
    {
        //en el gameObject de una arma

        public SteamVR_Action_Boolean Disparar = null;
        SteamVR_Behaviour_Pose Pose = null;

        Interactable interactable;


        public int Force = 10;

        public Transform Barrel;


        public GameObject ProjectilePrefab;
        ProjectilePool projectilePool = null;

        private void Awake()
        {
            //Pose = ;
            interactable = GetComponent<Interactable>();
            projectilePool = new ProjectilePool(ProjectilePrefab, 10);
        }

        private void Update()
        {
            //Debug.DrawRay(Barrel.position, Barrel.forward, Color.yellow);
            Debug.DrawLine(Barrel.position, Barrel.position + new Vector3(0,0,Barrel.position.z+10f), Color.red);
            if (interactable.IsAttached)
            {
                Debug.Log("Se presiono");
                if (Disparar.GetStateDown(GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource))
                {
                    Shoot();
                }
            }
        }

        void Shoot()
        {
            //Projectile target = projectilePool.mProjectil[0];

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(Barrel.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                
                Debug.Log("Did Hit");
            }
            

            Debug.Log("Se disparoooo");
           // target.Disparar(this);
        }


    }
}
