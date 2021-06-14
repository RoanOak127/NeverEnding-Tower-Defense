using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerShoot : MonoBehaviour
{
    private GameControl gameCtrl;
    private Tower tower;
    private List<GameObject> currentTargets = new List<GameObject>();
    private float nextShootTime = 0;
    private Color attackColor;
    private Light crystalLight;
    private AudioSource attackSound;
    private ParticleSystem attackVisual;

    // Start is called before the first frame update
    void Start()
    {
        gameCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        tower = GetComponent<Tower>();
        attackColor = tower.attackColor;
        attackSound = tower.attackSound;
        attackVisual = tower.attackVisual;
        crystalLight = tower.crystalLight;

        if (attackColor.r == 1) //idk why the laser won't accept the color passed from tower)
        {
            attackColor = Color.red;
        }
        else
            attackColor = Color.green;


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextShootTime && currentTargets.Count != 0)
        {

            if(currentTargets.Count == 0)
            {
                return;
            }

            for (int i = 0; i < currentTargets.Count; i++)
            {
                if (currentTargets[i] == null)
                {
                    currentTargets.RemoveAt(i);
                    i--;
                }
            }

            if (!tower.isAOE)
            {
                int selectedTarget = UnityEngine.Random.Range(0, currentTargets.Count);
                if (currentTargets[selectedTarget] != null)
                {
                    ShootLaser(currentTargets[selectedTarget].transform.position);
                    currentTargets[selectedTarget].GetComponent<Enemy>().health -= tower.towerDamage;


                    if (currentTargets[selectedTarget].GetComponent<Enemy>().health <= 0)
                    {
                        currentTargets.RemoveAt(selectedTarget);
                    }
                }
            }
            else
            {
                ShootAOE();
            }

            nextShootTime = Time.time + tower.shootTime;
        }
    }

    private void ShootLaser(Vector3 position)
    {
        //do particle effect
        //play sound
        //flash light
        crystalLight.enabled = true;
        attackSound.Play();
        attackVisual.Play();
        //Debug.Log("In shoot laser");
       // Debug.Log("Light is: " + crystalLight.name);


        GameObject gameObj = new GameObject("laser");
        LineRenderer laser = gameObj.AddComponent<LineRenderer>();
        laser.material.color = attackColor;
        laser.widthMultiplier = 0.2f;
        laser.SetPosition(0, transform.position + Vector3.up);
        laser.SetPosition(1, position);
        Destroy(laser, 1);
        Destroy(gameObj, 1);
        //crystalLight.enabled = false;

    }

    private void ShootAOE()
    {
        //do particle effect
        //play sound
        //flash light
        crystalLight.enabled = true;
        attackSound.Play();
        attackVisual.Play();

        foreach (GameObject enemy in currentTargets)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().health -= tower.towerDamage;

                if (enemy.GetComponent<Enemy>().health <= 0)
                {
                    currentTargets.Remove(enemy);
                }
            }

        }
        //wait();
        //crystalLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTargets.Remove(other.gameObject);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }
}