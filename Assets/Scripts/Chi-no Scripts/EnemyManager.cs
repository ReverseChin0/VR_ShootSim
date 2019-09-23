using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] targets;
    public EnemieAgent[] enemies;
    int ntargets;

    private void Start()
    {
        ntargets = targets.Length;
    }

    private void Update()
    {
        
    }
}
