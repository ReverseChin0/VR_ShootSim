using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Projectile : MonoBehaviour
    {
        // en el gameObject de tipo que sea bala
        public float LifeTime = 5.0f;
        Rigidbody MyRigidBody;

        private void Awake()
        {
            MyRigidBody = GetComponent<Rigidbody>();
            SetEnable();
        }

        private void OnCollisionEnter(Collision collision)
        {
            SetEnable();
        }

        public void Disparar(Weapon _weapon)
        {
            transform.position = _weapon.Barrel.position;
            transform.rotation = _weapon.Barrel.rotation;

            gameObject.SetActive(true);

            //MyRigidBody.AddRelativeForce(Vector3.forward * _weapon.Force, ForceMode.Impulse);
            StartCoroutine(LifeTimeDuration());
        }

        IEnumerator LifeTimeDuration()
        {
            yield return new WaitForSeconds(LifeTime);
            SetEnable();
        }

        public void SetEnable()
        {
            MyRigidBody.velocity = Vector3.zero;
            MyRigidBody.angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
        }
    }
}
