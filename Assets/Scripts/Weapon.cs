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

        public Transform Barrel;

        public float rango = 100f;
        public float continuarDisparando = 0f;
        public float FireRate = 10f;

        public ParticleSystem fire, smoke;



        private void Awake()
        {
            //Pose = ;
            interactable = GetComponent<Interactable>();
            
        }

        private void Update()
        {
            //Debug.DrawRay(Barrel.position, Barrel.forward, Color.yellow);
            //Debug.DrawLine(Barrel.position, Barrel.position + new Vector3(0,0,Barrel.position.z+10f), Color.red);
             if (interactable.IsAttached)
             {
                 Debug.Log("Se presiono");
                 if (Disparar.GetStateDown(GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource) && Time.time >= continuarDisparando)
                 {
                     continuarDisparando = Time.time + 1f / FireRate;
                     Shoot();
                 }
             }

            /*
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }*/
        }

        void Shoot()
        {
            //Projectile target = projectilePool.mProjectil[0];
            fire.Play();
            smoke.Play();
            RaycastHit hit;
            if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, Mathf.Infinity /*rango*/))
            {
                Debug.DrawRay(Barrel.position, Barrel.forward, Color.red);
                Debug.Log(hit.collider.name);                
            }           

            Debug.Log("Se disparoooo");
           // target.Disparar(this);
        }


    }
}
