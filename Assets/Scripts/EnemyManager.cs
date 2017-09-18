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
    public GameObject[] EnemyHPHearts;
    [HideInInspector]
    public bool IsDeath;

    private Enemy[] enemys;
    private int hp = 5;
    private bool enemyTimerStart;
    private float timer = 0;
    private float attackTimer = 5.0f;
    private int enemyID = 0;
    private bool isSpawn;
    public int MagicPower;
    private Dragon dragon;

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
            return enemyTimerStart;
        }

        set
        {
            enemyTimerStart = value;
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

    public int HP
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        isSpawn = false;
        IsDeath = false;
        enemyTimerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
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
                    SpawnDragon();
                    break;
                default:
                    break;
            }
        }
        if (enemyTimerStart)
            timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            enemyTimerStart = false;
            timer = 0;
            if (Random.Range(0, 2) > 0)
            {
                if (enemys[0] == null)
                    return;
                enemys[0].Attack();
                GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/DefenceEnemy") as GameObject);
                effect.transform.position = enemys[0].transform.position;
                Destroy(effect, 3f);
            }
            else
            {
                if (enemys[2] == null)
                    return;
                enemys[2].Attack();
                GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/AttackEnemy") as GameObject);
                effect.transform.position = enemys[2].transform.position;
                Destroy(effect, 3f);
            }
        }

        if (hp <= 0 && !IsDeath)
        {
            hp = 0;
            IsDeath = true;

            if (enemyID == 3)
            {
                dragon.Dead();
            }
            else
            {
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].Dead();
                }
                StartCoroutine(StageController.Instance.ChangeStage());
                enemyTimerStart = false;
                enemyID += 1;
            }
        }
    }
    private IEnumerator SpawnEnemy(GameObject[] go)
    {
        hp = 5;
        StartCoroutine(InitHPHeart());
        enemys = new Enemy[go.Length];
        for (int i = 0; i < go.Length; i++)
        {
            yield return new WaitForSeconds(1f);
            enemys[i] = Instantiate(go[i]).GetComponent<Enemy>();
            if (i == go.Length - 1)
            {
                yield return new WaitForSeconds(1f);
                enemyTimerStart = true;
                IsDeath = false;
                GameManager.Instance.ClearAllTemp();
            }
        }
        MagicPower = 0;
    }
    public void Damage()
    {
        if (IsDeath)
            return;
        if (enemyID == 3)
        {
            GameManager.Instance.MagicPower += 1;
            hp -= 1;
            EnemyHPHearts[hp].SetActive(false);
            dragon.transform.DOShakePosition(0.5f, 0.5f);
        }
        else
        {
            if (enemys[0].IsAttack)
                return;
            GameManager.Instance.MagicPower += 1;
            hp -= 1;
            EnemyHPHearts[hp].SetActive(false);
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].transform.DOShakePosition(0.5f, 0.5f);
            }
        }
    }
    public void MagicDamage()
    {
        if (IsDeath)
            return;
        int index = hp;
        hp -= 2;
        if (index > 1)
        {
            EnemyHPHearts[index - 1].SetActive(false);
            EnemyHPHearts[hp].SetActive(false);
        }
        else
        {
            EnemyHPHearts[index - 1].SetActive(false);
        }

        if (enemyID == 3)
        {
            dragon.transform.DOShakePosition(0.5f, 0.5f);
        }
        else
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].transform.DOShakePosition(0.5f, 0.5f);
            }
        }

    }
    public void MagicAttack()
    {
        MagicPower = 0;
        GameManager.Instance.MagicDamage();
        enemys[1].Attack();
        GameObject effect = Instantiate(ResourcesManager.Instance.GetAsset("Effects/MagicEnemy") as GameObject);
        Destroy(effect, 5f);
    }
    IEnumerator InitHPHeart()
    {
        for (int i = 0; i < hp; i++)
        {
            if (!EnemyHPHearts[i].activeInHierarchy)
                EnemyHPHearts[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void SpawnDragon()
    {
        hp = 8;
        StartCoroutine(InitHPHeart());
        dragon = Instantiate(Dragon).GetComponent<Dragon>();
        IsDeath = false;
        GameManager.Instance.ClearAllTemp();
        MagicPower = 0;
    }

}
