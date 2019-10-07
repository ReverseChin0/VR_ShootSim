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
    public float Distance = 2, rotationspeed =  1f, Accuerror = 2.0f;
    bool arrived = false;
    public bool dead = false;
    float healthpoints = 100f, initialheight,initiialY;
    GameManager gm;

    Quaternion targetRotation;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        particools.Stop();
    }

    public void InitializeEnemies(Transform[] _t)
    {
        healthpoints = 100f;
        targets = _t;
        miAnim = transform.GetChild(0).GetComponent<Animator>();
        dead = false;
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
            transform.LookAt(ObjectiveAim.position);

            if(time-2.0f > 0.0f)
            {
                StartCoroutine(WaitToShoot((time - 2.0f)*0.4f));
            }

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
        gm.SetCont();
        miAnim.SetTrigger("Die");
        StopAllCoroutines();
        particools.Stop();
        agent.SetDestination(transform.position);
        miManager.LeavePosition(current);
        StartCoroutine(RespawnMePlease(5.0f));
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

    public IEnumerator RespawnMePlease(float secs)
    {
        yield return new WaitForSeconds(secs);
        miManager.RespawnAgent(this);
    }

    public IEnumerator WaitToShoot(float time)
    {
        yield return new WaitForSeconds(time);
        Disparar();
    }

    void Disparar()
    {
        RaycastHit hit;
        Vector3 innacurateTarget = (ObjectiveAim.position + new Vector3(Random.Range(-Accuerror, Accuerror), Random.Range(-Accuerror, Accuerror), Random.Range(-Accuerror, Accuerror))) - transform.position;
        innacurateTarget.Normalize();
        if (Physics.Raycast(particools.transform.position, innacurateTarget, out hit, 60f))
        {
            Debug.DrawRay(particools.transform.position, innacurateTarget * 60.0f, Color.red, 3f);
            Debug.Log("Le di a... " + hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<CameraTestPlayer>().TakeDMG(15f);
            }
        }
        else
        {
            Debug.DrawRay(particools.transform.position, innacurateTarget * 60.0f, Color.yellow, 3f);
            Debug.Log("No le di...");
        }
    }
}
