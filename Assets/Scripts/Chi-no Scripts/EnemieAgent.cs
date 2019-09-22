using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieAgent : MonoBehaviour
{
    public Transform[] target;
    public NavMeshAgent agent;
    Animator miAnim;

    Vector3 currentDestination;
    int current = 0, ntargets;
    public float Distance = 2;

    private void Start()
    {
        currentDestination = target[current].position;
        ntargets = target.Length;
        agent.SetDestination(currentDestination);
        miAnim = transform.GetChild(0).GetComponent<Animator>();    
    }

    private void Update()
    {
        Vector3 direccion = currentDestination - transform.position;
        if(direccion.sqrMagnitude < Distance)
        {
            miAnim.SetBool("moving", true);
            current++;
            if (current >= ntargets)
                current = 0;
            //print("yes");
            currentDestination = target[current].position;
            agent.SetDestination(currentDestination);
        }
    }
}
