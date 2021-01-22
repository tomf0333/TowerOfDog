using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class Baddy : MonoBehaviour
{

    public double health = 100;

    public Animator animator;

    public double damage = 40;

    public GameObject everything;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("unalive") == true)
        {
            Debug.Log("horrible");
            Destroy(everything);
        }
    }

    public void TakeDamage(double damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
    }
}
