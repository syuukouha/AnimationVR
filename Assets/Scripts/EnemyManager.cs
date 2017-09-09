﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject[] Goblins;
    public GameObject[] Humans;
    public GameObject[] Wolfs;
    public GameObject[] Dragon;

    private Enemy[] enemys;
    private int HP = 5;

    private bool timerStart = false;
    private float timer = 0;
    private float attackTimer = 5.0f;
    private int index;
    private int enemyID = 0;
    private bool isSpawn = false;
    private bool damageShaking = false;

    public int EnemyID
    {
        get
        {
            return enemyID;
        }

        set
        {
            enemyID = value;
        }
    }

    public bool TimerStart
    {
        get
        {
            return timerStart;
        }

        set
        {
            timerStart = value;
        }
    }

    public bool IsSpawn
    {
        get
        {
            return isSpawn;
        }

        set
        {
            isSpawn = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isSpawn)
        {
            isSpawn = false;
            switch (enemyID)
            {
                case 0:
                    StartCoroutine(SpawnEnemy(Goblins));
                    break;
                case 1:
                    StartCoroutine(SpawnEnemy(Humans));
                    break;
                case 2:
                    StartCoroutine(SpawnEnemy(Wolfs));
                    break;
                case 3:
                    StartCoroutine(SpawnEnemy(Dragon));
                    break;
                default:
                    break;
            }
        }
        if (timerStart)
            timer += Time.deltaTime;
        if(timer>=attackTimer)
        {
            timerStart = false;
            timer = 0;
            Attack();
        }
	}
    public void Attack()
    {
        enemys[index].Attack();
        index++;
        if (index >= enemys.Length)
            index = 0;
    }
    private IEnumerator SpawnEnemy(GameObject[] go)
    {
        enemys = new Enemy[go.Length];
        for (int i = 0; i < go.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            enemys[i] = Instantiate(go[i]).GetComponent<Enemy>();
            if (i == go.Length - 1)
            {
                yield return new WaitForSeconds(1f);
                timerStart = true;
                GameManager.Instance.IsPlayerCanAttack = true;
            }
        }
        index = 0;
    }
    public void Damage()
    {
        if (HP <= 0)
        {
            HP = 5;
            GameManager.Instance.IsPlayerCanAttack = false;
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].Dead();
            }
            if (enemyID != 3)
                StartCoroutine(StageController.Instance.ChangeStage());
            timerStart = false;
            enemyID += 1;
        }
        else
        {
            HP -= 1;
            if(!damageShaking)
            {
                damageShaking = true;
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].transform.DOShakePosition(0.5f);
                    enemys[i].transform.DOShakeRotation(0.5f).OnComplete(()=> {
                        damageShaking = false;
                    });
                }
            }

        }

    }
}
