using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class Monster_Goblin : Monster
{
    static string GOBLIN_NAME = "Goblin";
    static int GOBLIN_DAMAGE = 15;
    static int GOBLIN_HEALTH = 150;

    public Animator animator;

    public Text attackIndicator;
    public Text healthIndicator;

    public Monster_Goblin() : base(GOBLIN_NAME, GOBLIN_DAMAGE, GOBLIN_HEALTH, GOBLIN_HEALTH) { }

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
