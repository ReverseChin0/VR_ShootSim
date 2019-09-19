using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemieAgent : MonoBehaviour
{
    public Transform[] target;
    public NavMeshAgent agent;
    Vector3 currentDestination;
    int current = 0;
    public float Distance = 2;

    private void Start()
    {
        currentDestination = target[current].position;
        agent.SetDestination(currentDestination);
    }

    private void Update()
    {
        Vector3 direccion = currentDestination - transform.position;
        if(direccion.sqrMagnitude < Distance)
        {
            current++;
            if (current > 3)
                current = 0;
            print("yes");
            currentDestination = target[current].position;
            agent.SetDestination(currentDestination);
        }
    }
}
