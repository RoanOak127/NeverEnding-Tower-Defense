using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject easyEnemyPrefab;
    [SerializeField] private GameObject medEnemyPrefab;
    [SerializeField] private GameObject hardEnemyPrefab;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] GameControl gameCtrl;
    public int numEasy;
    public int numMed;
    public int numHard;
    private GameObject g;


    private void Start()
    {
        numEasy = gameCtrl.numEasyEnemies;
        numMed = gameCtrl.numMedEnemies;
        numHard = gameCtrl.numHardEnemies;

        //StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (numEasy > 0)
        {

            
            g = Instantiate(easyEnemyPrefab, transform);

            NavMeshHit closestNavPos;
            if (NavMesh.SamplePosition(transform.position, out closestNavPos, 10, NavMesh.AllAreas))
            g.transform.position = closestNavPos.position;
            NavMeshAgent n = g.AddComponent<NavMeshAgent>();
            n.speed = 8;
            n.angularSpeed = 120;
            n.acceleration = 8;
            

            //Instantiate(easyEnemyPrefab, transform);
            numEasy--;
            yield return new WaitForSeconds(spawnTime);
        }
        while (numMed > 0)
        {
            g = Instantiate(medEnemyPrefab, transform);

            NavMeshHit closestNavPos;
            if (NavMesh.SamplePosition(transform.position, out closestNavPos, 10, NavMesh.AllAreas))
                g.transform.position = closestNavPos.position;
            NavMeshAgent n = g.AddComponent<NavMeshAgent>();
            n.speed = 5;
            n.angularSpeed = 120;
            n.acceleration = 5;

            numMed--;
            yield return new WaitForSeconds(spawnTime * 1.5f);
        }
        while (numHard > 0)
        {
            g = Instantiate(hardEnemyPrefab, transform);

            NavMeshHit closestNavPos;
            if (NavMesh.SamplePosition(transform.position, out closestNavPos, 10, NavMesh.AllAreas))
                g.transform.position = closestNavPos.position;
            NavMeshAgent n = g.AddComponent<NavMeshAgent>();
            n.speed = 3;
            n.angularSpeed = 120;
            n.acceleration = 3;

            numHard--;
            yield return new WaitForSeconds(spawnTime*2f);
        }

        gameCtrl.isNextLevelReady = true;
    }
}