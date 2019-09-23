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
    bool arrived = false;

    private void Start()
    {
        currentDestination = target[current].position;
        ntargets = target.Length;
        agent.SetDestination(currentDestination);
        miAnim = transform.GetChild(0).GetComponent<Animator>();
        miAnim.SetBool("moving", true);
    }

    private void Update()
    {
        Vector3 direccion = currentDestination - transform.position;
        if(direccion.sqrMagnitude < Distance && !arrived)
        {
            arrived = true;
            StartCoroutine(Arrived(Random.Range(2.0f, 10.0f)));
        }
    }

    public IEnumerator Arrived(float time)
    {
        miAnim.SetBool("moving", false);
        yield return new WaitForSeconds(time);
        GoToDestination();
    }
    public void GoToDestination()
    {
        arrived = false;
        miAnim.SetBool("moving", true);
        current = Random.Range(0, ntargets);
        currentDestination = target[current].position;
        agent.SetDestination(currentDestination);
    }
}
