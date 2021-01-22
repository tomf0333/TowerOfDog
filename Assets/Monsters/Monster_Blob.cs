using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class Monster_Blob : Monster
{
    static string BLOB_NAME = "Blob";
    static int BLOB_DAMAGE = 10;
    static int BLOB_HEALTH = 100;

    public Animator animator;

    public Text attackIndicator;
    public Text healthIndicator;

    public Monster_Blob() : base(BLOB_NAME, BLOB_DAMAGE, BLOB_HEALTH, BLOB_HEALTH) { }

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
