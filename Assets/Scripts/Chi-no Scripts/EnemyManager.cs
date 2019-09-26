using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] targets;
    public GameObject EnemyPrefab;
    List<EnemieAgent> enemies;
    public int enemyn = 0;
    bool[] ocupado;
    private void Start()
    {
        enemies = new List<EnemieAgent>();
        ocupado = new bool[targets.Length];
        for(int j = 0; j < ocupado.Length; j++)
        {
            ocupado[j] = false;
        }

        for(int i = 0; i< enemyn; i++)
        {
            GameObject GO = Instantiate(EnemyPrefab, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
            GO.name = GO.name + i.ToString();
            enemies.Add(GO.GetComponent<EnemieAgent>());
            enemies[i].miManager = this;
            enemies[i].InitializeEnemies(targets);
        }
        
    }

    public bool RequestPosition(int _r)
    {
        if (ocupado[_r])
        {
            return false;
        }

        ocupado[_r] = true;
        return true;
    }

    public void LeavePosition(int _l)
    {
        ocupado[_l] = false;
    }

    private void Update()
    {
        
    }
}
