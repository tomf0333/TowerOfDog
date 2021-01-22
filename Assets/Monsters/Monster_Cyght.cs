using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster_Cyght : Monster
{
    static string CYGHT_NAME = "Cyght";
    static int CYGHT_DAMAGE = 20;
    static int CYGHT_HEALTH = 300;

    public Animator animator;

    public Text attackIndicator;
    public Text healthIndicator;

    public Monster_Cyght() : base(CYGHT_NAME, CYGHT_DAMAGE, CYGHT_HEALTH, CYGHT_HEALTH) { }

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
