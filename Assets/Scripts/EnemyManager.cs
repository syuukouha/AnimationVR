using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject[] Goblins;
    private Enemy[] enemys;
    public int HP = 5;

    private bool timerStart = false;
    private float timer = 0;
    private float attackTimer = 5.0f;
    private int index = 0;
    private int enemyID = 0;
    private bool isSpawn = false;

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
    void Start () {
		
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
        switch (enemyID)
        {
            case 0:
                enemys[index++].Attack();
                if (index >= enemys.Length)
                    index = 0;
                break;
            default:
                break;
        }
    }
    private IEnumerator SpawnEnemy(GameObject[] go)
    {
        enemys = new Enemy[go.Length];
        for (int i = 0; i < go.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            enemys[i] = Instantiate(go[i]).GetComponent<Enemy>();
        }
    }
    public void Damage()
    {
        if (HP <= 0)
        {
            HP = 5;
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].Dead();
            }
            StartCoroutine(StageController.Instance.ChangeStage());
            timerStart = false;
        }
        else
        {
            HP -= 1;
        }

    }
}
