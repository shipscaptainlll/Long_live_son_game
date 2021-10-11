using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAlchemy : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem paritclesAlchemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopAlchemy()
    {
        anim.speed = 1;
    }

    public void startParticles()
    {
        paritclesAlchemy.gameObject.SetActive(true);
        
    }


}
