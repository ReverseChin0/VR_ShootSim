using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieAgent : MonoBehaviour
{
    Transform[] targets;

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

    Quaternion targetRotation;

    private void Start()
    {
      
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

        current = next;
        currentDestination = targets[next].position;
        agent.SetDestination(currentDestination);
        miAnim.SetBool("moving", true);
   
    }

    private void Update()
    {
        Vector3 direccion = currentDestination - transform.position;
        if(direccion.sqrMagnitude < Distance && !arrived)
        {
            arrived = true;
            miAnim.SetBool("moving", false);
            StartCoroutine(Arrived(Random.Range(2.0f, 10.0f)));
        }
    }

    public IEnumerator Arrived(float time)
    {
        if(Random.Range(0,2)==0)//0 es disparar, 1 es esperar
        {
            print("pium "+transform.name);
            yield return new WaitForSeconds(1.0f);
            miAnim.SetBool("Shooting", true);
            //targetRotation = Quaternion.LookRotation(ObjectiveAim.position);
            transform.LookAt(ObjectiveAim.position);// = Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(time);
        miAnim.SetBool("Shooting", false);
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
        current = next;
    }
}
