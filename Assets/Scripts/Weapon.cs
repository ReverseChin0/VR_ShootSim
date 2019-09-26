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

        public float rango = 1000f;
        public float continuarDisparando = 0f;
        public float FireRate = 10f;
        public float FuerzaDeImpacto = 30f;
        public ParticleSystem fire, smoke;

        public GameObject dubugObj;



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
                 //Debug.Log("Se presiono");
                 if (Disparar.GetStateDown(GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource) && Time.time >= continuarDisparando)
                 {
                     continuarDisparando = Time.time + 1f / FireRate;
                     Shoot();
                 }
             }

            
            if (Input.GetButtonDown("Fire1"))
            {
                //Instantiate(dubugObj, Barrel);
                Shoot();
            }
            /*Vector3 foward = Barrel.TransformDirection(Barrel.forward) * 10f;
            Debug.DrawRay(Barrel.position, foward, Color.magenta);*/
        }

        void Shoot()
        {
            //Projectile target = projectilePool.mProjectil[0];
            fire.Play();
            smoke.Play();
            RaycastHit hit;
            if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, /*Mathf.Infinity*/rango))
            {
                Debug.DrawRay(Barrel.position, Barrel.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Le pegaste a: "+hit.collider.name);
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * FuerzaDeImpacto);
                }
            }
            else
            {
                Debug.DrawRay(Barrel.position, Barrel.TransformDirection(Vector3.forward) * 100f, Color.red);
                Debug.Log("No le pegaste");
            }        

            
            //Debug.Log("Se disparoooo");
           // target.Disparar(this);
        }


    }
}
