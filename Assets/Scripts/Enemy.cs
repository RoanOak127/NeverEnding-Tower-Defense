using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameControl gameCtrl;
    private NavMeshAgent agent;

    public int health = 100;
    public int attack;
    public int killCash;
    [SerializeField] ParticleSystem deathStars;
    private bool isDead = false;

    [SerializeField] private float speed = 1f;
    public GameObject prev;
    public GameObject next;
    // Start is called before the first frame update
    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        prev = PathGenerator.path[0];
        transform.position = prev.transform.position;
       // Debug.Log("current position: " + transform.position);
        //Debug.Log("Supposed to be at : " + prev.transform.position);
        next = PathGenerator.path[1];
        transform.rotation = Quaternion.LookRotation((next.transform.position - prev.transform.position).normalized);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("go into if? " + (Convert.ToInt32(prev.name) < PathGenerator.path.Count - 1));
        if (Convert.ToInt32(prev.name) < PathGenerator.path.Count - 1)
        {
            next = PathGenerator.path[Convert.ToInt32(prev.name) + 1];

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((next.transform.position - prev.transform.position).normalized), 15);
            agent.SetDestination(next.transform.position);

            /*
            transform.position += (next.transform.position - transform.position).normalized * speed * Time.deltaTime;
            transform.position =  Vector3.Slerp(transform.position, (next.transform.position - transform.position).normalized * speed * Time.deltaTime, 1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((next.transform.position - prev.transform.position).normalized), 15);
            */
            //Debug.Log("Disdtance: " + Vector3.Distance(transform.position, next.transform.position));
            if (Vector3.Distance(transform.position, next.transform.position) < 0.18f)
            {
                prev = next;
            }
        }
        else
        {
            // Apply Damage to the end "wall"
            gameCtrl.health -= attack;
            gameCtrl.healthBar.currentPercent = gameCtrl.health;
            gameCtrl.totalEnemiesAlive--;
            gameCtrl.EnemiesLeftDisplay.text = "Enemies Left: " + gameCtrl.totalEnemiesAlive;
            Destroy(gameObject);
        }

        if(health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(DieScene());
            
        }
    }

    IEnumerator DieScene()
    {
        deathStars.Play();
        gameCtrl.totalEnemiesAlive--;
        gameCtrl.EnemiesLeftDisplay.text = "Enemies Left: " + gameCtrl.totalEnemiesAlive;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        agent.speed = 0;
        Destroy(gameObject.GetComponent<Rigidbody>());
        yield return new WaitForSeconds(2.5f);
        deathStars.Stop();

        Destroy(gameObject);
        gameCtrl.cash += killCash;
        gameCtrl.showCash.text = "$" + gameCtrl.cash;
    }
}