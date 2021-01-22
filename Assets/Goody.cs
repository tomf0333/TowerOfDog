using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class Goody : MonoBehaviour
{

    public double maxHealth = 100;
    public double currentHealth = 100;
    public bool alive = true;

    public double damage = 40;

    public GameObject firePoint;
    public GameObject firePointS;
    public GameObject firePointM;
    Animator attackAnimator;
    public Animator moveAnimator;
    public Text healthIndicator;

    Vector3 startPosition;
    Vector3 startScale;
    Vector3 pS;
    Vector3 sS;
    Vector3 pM;
    Vector3 sM;

    double x;
    double y;

    public int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        pS = firePointS.transform.position;
        sS = firePointS.transform.localScale;
        pM = firePointM.transform.position;
        sM = firePointM.transform.localScale;
        attackAnimator = firePoint.GetComponent<Animator>();
        startPosition = firePoint.transform.position;
        startScale = firePoint.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        healthIndicator.text = "Hp: " + this.currentHealth;
        if (!this.alive)
        {
            moveAnimator.SetTrigger("trigger_death");
        }
        if (attackAnimator.GetBool("returned") == true)
        {
            attackAnimator.SetBool("returned", false);
            firePoint.transform.localScale = startScale;
            firePoint.transform.position = startPosition;
        }
    }

    public void GenAttack(int type)
    {
        switch(type)
        {
            case 1:
                attack();
                break;
            case 2:
                firePoint.transform.position = pS;
                firePoint.transform.localScale = sS;
                attack();
                break;
            case 3:
                firePoint.transform.position = pM;
                firePoint.transform.localScale = sM;
                attack();
                break;
            case 4:
                // shield animation
                break;
            case 5:
                // heal animation
                break;
        }
    }

    public void attack()
    {
        attackAnimator.SetTrigger("attack");
    }

    public void TakeDamage(double damage)
    {
        currentHealth = Math.Round(currentHealth - damage, 2);
        if (currentHealth <= 0)
        {
            this.alive = false;
        }
    }

    public void Heal(double heal)
    {
        currentHealth = Math.Round(currentHealth + heal, 2);
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
