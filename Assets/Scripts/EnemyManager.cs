using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyManager : Singleton<EnemyManager>
{
    public GameObject[] Goblins;
    public GameObject[] Humans;
    public GameObject[] Wolfs;
    public GameObject Dragon;
    [HideInInspector]
    public bool IsDeath;

    private Enemy[] enemys;
    private int HP = 5;

    private bool timerStart = false;
    private float timer = 0;
    private float attackTimer = 5.0f;
    private int enemyID = 0;
    private bool isSpawn = false;
    public int MagicPower;

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
        IsDeath = false;
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
            if (Random.Range(0, 2) > 0)
            {
                enemys[0].Attack();
                GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/DefenceEnemy") as GameObject);
                effect.transform.position = enemys[0].transform.position;
                Destroy(effect, 3f);
            }
            else
            {
                enemys[2].Attack();
                GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/AttackEnemy") as GameObject);
                effect.transform.position = enemys[0].transform.position;
                effect.transform.DOMoveX(effect.transform.position.x - 10f, 1f);
                Destroy(effect, 3f);
            }
        }

        if (HP <= 0)
        {
            HP = 5;
            IsDeath = true;
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].Dead();
            }
            if (enemyID != 3)
                StartCoroutine(StageController.Instance.ChangeStage());
            timerStart = false;
            enemyID += 1;
        }
        if(MagicPower >= 2)
        {
            enemys[1].Attack();
            GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/AttackEnemy") as GameObject);
            Destroy(effect, 3f);
        }

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
                IsDeath = false;
            }
        }
        MagicPower = 0;
    }
    public void Damage()
    {
        if (enemys[0].IsAttack)
            return;
        HP -= 1;
        GameManager.Instance.MagicPower += 1;
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].transform.DOShakePosition(0.5f);
            enemys[i].transform.DOShakeRotation(0.5f);
        }
    }
}
