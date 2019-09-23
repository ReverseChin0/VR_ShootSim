using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = _weapon.transform.position;
        transform.rotation = _weapon.transform.rotation;

        gameObject.SetActive(true);

        MyRigidBody.AddRelativeForce(Vector3.forward * 10,ForceMode.Impulse);
        StartCoroutine(LifeTimeDuration());
    }

    IEnumerator LifeTimeDuration()
    {
        yield return new WaitForSeconds(LifeTime);
        SetEnable();
    }

    void SetEnable()
    {
        MyRigidBody.velocity = Vector3.zero;
        MyRigidBody.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }
}
