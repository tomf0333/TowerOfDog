using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Ohno : Monster
{

    static string OHNO_NAME = "Ohno";
    static int OHNO_DAMAGE = 9;
    static int OHNO_HEALTH = 999;

    public Animator animator;

    public Text attackIndicator;
    public Text healthIndicator;

    public Monster_Ohno() : base(OHNO_NAME, OHNO_DAMAGE, OHNO_HEALTH, OHNO_HEALTH) { }

    // Update is called once per frame
    void Update()
    {
        attackIndicator.text = "Atk: " + this.damage.ToString();
        healthIndicator.text = "Hp: " + this.currentHealth.ToString();
        if (!this.alive)
        {
            animator.SetTrigger("trigger_death");
        }
        if (animator.GetBool("unalive") == true)
        {
            Destroy(gameObject);
        }
    }
}
