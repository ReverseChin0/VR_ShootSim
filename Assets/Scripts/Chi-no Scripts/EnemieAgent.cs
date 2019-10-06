using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieAgent : MonoBehaviour
{
    Transform[] targets;

    public ParticleSystem particools;
    CapsuleCollider miColi;

    public NavMeshAgent agent;
    Animator miAnim;
    [HideInInspector]
    public EnemyManager miManager;
    [HideInInspector]
    public Transform ObjectiveAim;

    Vector3 currentDestination;
    int current = 0, ntargets, next;
    public float Distance = 2, rotationspeed =  1f;
    bool arrived = false;
    public bool dead = false;
    float healthpoints = 100f, initialheight,initiialY;


    Quaternion targetRotation;

    private void Start()
    {
        particools.Stop();
    }

    public void InitializeEnemies(Transform[] _t)
    {
        targets = _t;
        miAnim = transform.GetChild(0).GetComponent<Animator>();
        ntargets = targets.Length;
        next = Random.Range(0, ntargets);
        while (!miManager.RequestPosition(next))
        {
            next = Random.Range(0, ntargets);
        }

        miColi = GetComponent<CapsuleCollider>();
        initialheight = miColi.height;
        initiialY = miColi.center.y;

        current = next;
        currentDestination = targets[next].position;
        agent.SetDestination(currentDestination);
        miAnim.SetBool("moving", true);
        toogleColliderCrouch(false);

    }

    private void Update()
    {
        Vector3 direccion = currentDestination - transform.position;
        if(!dead && direccion.sqrMagnitude < Distance && !arrived)
        {
            arrived = true;
            miAnim.SetBool("moving", false);
            toogleColliderCrouch(true);
            StartCoroutine(Arrived(Random.Range(2.0f, 10.0f)));
        }

    }

    public IEnumerator Arrived(float time)
    {
        if(Random.Range(0,2)==0)//0 es disparar, 1 es esperar
        {
            //print("pium "+transform.name);
            yield return new WaitForSeconds(1.0f);
            miAnim.SetBool("Shooting", true);
            toogleColliderCrouch(false);
            particools.Play();
            //targetRotation = Quaternion.LookRotation(ObjectiveAim.position);
            transform.LookAt(ObjectiveAim.position);// = Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(time);
        miAnim.SetBool("Shooting", false);
        toogleColliderCrouch(true);
        particools.Stop();
        GoToDestination();
    }
    public void GoToDestination()
    {
        next = Random.Range(0, ntargets);
        while (!miManager.RequestPosition(next))
        {
            next = Random.Range(0, ntargets);
        }
        miManager.LeavePosition(current);
        arrived = false;
        currentDestination = targets[next].position;
        agent.SetDestination(currentDestination);
        miAnim.SetBool("moving", true);
        toogleColliderCrouch(false);
        current = next;
    }

    public void TakeDamage(float damage)
    {
        healthpoints -= damage;
        if (healthpoints <= 0)
        {
            FuckingDieGodDammit();
        }
    }

    public void FuckingDieGodDammit()
    {
        miAnim.SetTrigger("Die");
        StopAllCoroutines();
        particools.Stop();
        agent.SetDestination(transform.position);
    }

    void toogleColliderCrouch(bool _crouch)
    {
        if (_crouch)
        {
            miColi.height = 1.15f;
            miColi.center = new Vector3(0.0f, -0.3f, 0.0f);
        }
        else
        {
            miColi.height = initialheight;
            miColi.center = new Vector3(0.0f, initiialY, 0.0f);
        }
    }
}
