using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float shootTime = 5f;
    public int towerRange = 2;
    public int towerDamage = 10;
    public bool isAOE;
    public Color attackColor;
    public Light crystalLight;
    public AudioSource attackSound;
    public ParticleSystem attackVisual;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SphereCollider>().radius = towerRange;
    }
    public void upgradeRadius()
    {
        GetComponent<SphereCollider>().radius = towerRange;
    }
}