using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;
using Random = System.Random;

public class BattleSystem : MonoBehaviour
{
    public class Attack
    {
        public string _name;
        public double _damage;
        public int _type;
        public double _shield;
        public double _heal;

        public Attack()
        {
            this._name = "";
            this._damage = 0;
            this._type = 0;
            this._shield = 0;
            this._heal = 0;
        }
        public Attack(string name, double damage, int type, double shield, double heal)
        {
            this._name = name;
            this._damage = damage;
            this._type = type;
            this._shield = shield;
            this._heal = heal;
        }
    }

    static char UP = 'u';
    static char DOWN = 'd';
    static char LEFT = 'l';
    static char RIGHT = 'r';
    static char A = 'a';
    static char B = 'b';

    static double NORMAL_MULTI = 1;
    static double SUPER_MULTI = NORMAL_MULTI + 0.5;
    static double MEGA_MULTI = SUPER_MULTI + 1;

    static int X_FROM_GOODY = 500;

    string presses;

    //static int SECOND = 256;
    static int SECOND = 256;

    int maxTime = 1 * SECOND;
    int currentTime;

    public Timer timer;
    //bool timerActive = true;

    public Goody goody;
    public Monster monster;
    public Monster_Blob blob;
    public Monster_Goblin goblin;
    public Monster_Cyght cyght;
    public Monster_Ohno ohno;

    Dictionary<string, Attack> attackMap;

    public GameObject win;
    public GameObject lose;

    public Text damageDealtText;

    static bool continue_fight;

    void Start()
    {
        // initiallize
        presses = "";
        continue_fight = true;
        win.SetActive(false);
        lose.SetActive(false);
        double damage = goody.damage;

        attackMap = new Dictionary<string, Attack>()
        {
            {"ab", new Attack("air kick", damage * NORMAL_MULTI, 1, 0, 0)},
            {"abab", new Attack("super air kick", damage * SUPER_MULTI, 2, 0, 0)},
            {"ababab", new Attack("mega super air kick", damage * MEGA_MULTI, 3, 0, 0)},
            {"ba", new Attack("uppercut", damage * NORMAL_MULTI, 1, 0, 0)},
            {"baab", new Attack("super uppercut", damage * SUPER_MULTI, 2, 0, 0)},
            {"udlr", new Attack("ultimate", damage * 100, 3, 0, 0)},
            {"lrab", new Attack("shield", 0, 4, 10, 0)},
            {"uuub", new Attack("heal", 0, 5, 0, 10)}
        };

        currentTime = maxTime;
        timer.setMax(maxTime);

        // monster creation
        Random rnd = new Random();
        Vector3 vec = new Vector3(goody.transform.localScale.x + X_FROM_GOODY, goody.transform.localScale.y, goody.transform.localScale.z);
        switch (rnd.Next(1, 5))
        {
            case 1:
                monster = Instantiate(blob, vec, Quaternion.identity) as Monster;
                /*monster = blob;*/
                break;
            case 2:
                monster = Instantiate(goblin, vec, Quaternion.identity) as Monster;
                /*monster = goblin;*/
                break;
            case 3:
                monster = Instantiate(cyght, vec, Quaternion.identity) as Monster;
                /*monster = Cyght;*/
                break;
            case 4:
                monster = Instantiate(ohno, vec, Quaternion.identity) as Monster;
                /*monster = Ohno;*/
                break;
        }
        /*Debug.Log("hp : " + monster.currentHealth + ", damage : " + monster.damage);*/

    }

    bool check_end()
    {
        bool end = false;
        if (!monster.alive && goody.alive)
        {
            // you won
            win.SetActive(true);
            end = true;

            // experimental
            goody.level++;
            
        }
        else if (!goody.alive)
        {
            // you lose
            lose.SetActive(true);
            end = true;
        }
        if (end)
        {
            continue_fight = false;
            return true;

            /*gameObject.SetActive(false);*/

            //goody.Disable();
            //monster.Disable();
            //timer.Disable();
        }
        return false;
    }

    void continueFight()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            presses += LEFT;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            presses += RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            presses += UP;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            presses += DOWN;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            presses += A;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            presses += B;
        }
        // move timer
        //if (currentTime == maxTime)
        //{
        //    timerActive = true;
        //}
        //if (timerActive == false)
        //{
        //    presses = "";
        //}
        moveTimer(1);
        if (currentTime <= 0)
        {
            Act();
            //timerActive = false;
            //currentTime = maxTime * 2;
            currentTime = maxTime;
        }
    }

    void Update()
    {
        /*Random rnd = new Random();
        Vector3 vec = new Vector3(goody.transform.localScale.x + X_FROM_GOODY, goody.transform.localScale.y, goody.transform.localScale.z);
        switch (rnd.Next(1, 3))
        {
            case 1:
                monster = Instantiate(blob, vec, Quaternion.identity) as Monster;
                *//*monster = blob;*//*
                break;
            case 2:
                monster = Instantiate(goblin, vec, Quaternion.identity) as Monster;
                *//*monster = goblin;*//*
                break;
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (continue_fight)
        {
            continueFight();
        }
        if (!continue_fight && Input.GetKeyDown(KeyCode.N) && goody.alive)
        {
            monster.Disable();
            Start();
        }

    }

    // in Act first the player attacks and then if the monster is alive then it attacks
    void Act()
    {
        // player attack
        double shi = 0;
        if (attackMap.ContainsKey(presses) && (presses.Length <= goody.level * 2))
        {
            double dm = attackMap[presses]._damage/* + ((goody.level - 1) * 15)*/;
            string nam = attackMap[presses]._name;
            int ty = attackMap[presses]._type;
            shi = attackMap[presses]._shield;
            double he = attackMap[presses]._heal;
            goody.GenAttack(ty);
            Debug.Log(dm + " " + nam);
            monster.TakeDamage(dm);
            goody.Heal(he);
            Animator a = damageDealtText.GetComponent<Animator>();
            a.SetTrigger("fade");
            damageDealtText.text = dm.ToString();
        }
        else
        {
            Debug.Log("no attack");
        }
        // check
        if (!check_end())
        {
            presses = "";
            // monster attack
            goody.TakeDamage(monster.damage - shi);
            // another check
            check_end();
        }
    }

    void moveTimer(int time)
    {
        currentTime -= time;
        timer.set(currentTime);
    }
}
