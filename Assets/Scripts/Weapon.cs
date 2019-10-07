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

        LineRenderer linea;

        Interactable interactable;

        public Transform Barrel;

        public float rango = 1000f;
        public float continuarDisparando = 0f;
        public float FireRate = 10f;
        public float FuerzaDeImpacto = 30f;
        public ParticleSystem fire, smoke;

        public GameObject dubugObj, Player, SpawnSpot, AimDot;
        Vector3 endPoint;
        public bool IsGrap = false;

        Vector3 distance;
        float maxDistanceToPlayer = 50f;


        public float WeaponDamage;

        private void Awake()
        {
            //Pose = ;
            interactable = GetComponent<Interactable>();
            linea = transform.GetChild(0).GetComponent<LineRenderer>(); 
                //GetComponent<LineRenderer>();
            
        }

        private void Start()
        {
            linea.material = new Material(Shader.Find("Sprites/Default"));
            linea.SetColors(Color.red, Color.red);
            AimDot.SetActive(false);
        }

        private void Update()
        {
            //Debug.DrawRay(Barrel.position, Barrel.forward, Color.yellow);
            //Debug.DrawLine(Barrel.position, Barrel.position + new Vector3(0,0,Barrel.position.z+10f), Color.red);
             if (interactable.IsAttached)
             {
                AimDot.SetActive(true);
                linea.SetPosition(0,new Vector3(Barrel.position.x, Barrel.position.y, Barrel.position.z));
                endPoint = new Vector3(Barrel.position.x, Barrel.position.y, Barrel.position.z + rango);
                linea.SetPosition(1, endPoint);
                AimDot.transform.position = endPoint;
                /*linea.startColor = Color.red;
                linea.endColor = Color.red;*/
                 //Debug.Log("Se presiono");
                 if (Disparar.GetStateDown(GetComponentInParent<SteamVR_Behaviour_Pose>().inputSource) && Time.time >= continuarDisparando)
                 {
                     continuarDisparando = Time.time + 1f / FireRate;
                     Shoot();
                 }
            }
            else
            {
                AimDot.SetActive(false);
                linea.SetPosition(0, Vector3.zero);
                linea.SetPosition(1, Vector3.zero );
               /* linea.startColor = Color.red;
                linea.endColor = Color.red;*/
            }

            
            if (Input.GetButtonDown("Fire1"))
            {
                //Instantiate(dubugObj, Barrel);
                Shoot();
            }
            /*Vector3 foward = Barrel.TransformDirection(Barrel.forward) * 10f;
            Debug.DrawRay(Barrel.position, foward, Color.magenta);*/

            distance = this.transform.position - Player.transform.position;
            if(distance.sqrMagnitude > maxDistanceToPlayer){
                this.transform.position = SpawnSpot.transform.position;
            }
          

        }

        void Shoot()
        {
            //Projectile target = projectilePool.mProjectil[0];
            fire.Play();
            //smoke.Play();
            RaycastHit hit;
            if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, /*Mathf.Infinity*/rango))
            {
                Debug.DrawRay(Barrel.position, Barrel.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Le pegaste a: "+hit.collider.name);
                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * FuerzaDeImpacto);
                    if (hit.transform.CompareTag("Enemie"))
                    {
                        hit.transform.GetComponent<EnemieAgent>().TakeDamage(WeaponDamage);
                    }
                    
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
