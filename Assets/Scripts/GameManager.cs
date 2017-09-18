using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using VRTK;
public class GameManager : Singleton<GameManager>
{
    private int playerHP;
    public Light PointLight;
    public Light SpotLight;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Title;
    [HideInInspector]
    public bool IsSwordGrabbed;
    [HideInInspector]
    public bool IsShieldGrabbed;
    public bool IsDeath;
    public Material[] Normals;
    public Material[] Fades;
    public int MagicPower = 0;
    public bool IsCanAttack;
    private ItemGrabAttach[] grabItems = new ItemGrabAttach[3];

    private List<Player> playerList = new List<Player>();
    private bool isGameStart;
    private bool isInit;
    public GameObject[] PlayerHPHearts;
    public GameObject Crown;
    // Use this for initialization
    void Start ()
    {
        IsSwordGrabbed = false;
        IsShieldGrabbed = false;
        isGameStart = false;
        IsDeath = false;
        isInit = true;
        IsCanAttack = false;
        playerHP = 7;
    }

    // Update is called once per frame
    void Update()
    {
        #region TestCode
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        #endregion
        if (ResourcesManager.Instance.IsComplete && isInit)
        {
            isInit = false;
            SpawnGrabItem(1);
            SpawnGrabItem(2);
            PointLight.DOIntensity(1, 3.0f);
            SpotLight.DOIntensity(1, 3.0f).SetDelay(3.0f);
        }
        if (IsSwordGrabbed && IsShieldGrabbed)
            GameStart();
        if (MagicPower >= 2 && grabItems[0] == null)
        {
            SpawnGrabItem(0);
            Destroy(grabItems[1].gameObject);
            Destroy(grabItems[2].gameObject);
            ShowHand(true, true);
            ShowHand(false, true);
        }
    }

    public void ItemRest()
    {
        MagicPower = 0;
        SpawnGrabItem(1);
        SpawnGrabItem(2);
        ShowHand(true, true);
        ShowHand(false, true);
        IsCanAttack = true;
    }
    private IEnumerator SpawnPlayer()
    {
        Player player;
        yield return new WaitForSeconds(4f);
        player = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Knight") as GameObject).GetComponent<Player>();
        playerList.Add(player);
        yield return new WaitForSeconds(1f);
        player = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Wizard") as GameObject).GetComponent<Player>(); ;
        playerList.Add(player);
        yield return new WaitForSeconds(1f);
        player = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Sword") as GameObject).GetComponent<Player>(); ;
        playerList.Add(player);
        IsCanAttack = true;
        EnabledItem(1);
        EnabledItem(2);
    }
    public IEnumerator Victory()
    {
        Instantiate(Crown);
        yield return new WaitForSeconds(4f);
        SoundManager.Instance.PlaySE(ResourcesManager.Instance.GetAsset("Sounds/Victory") as AudioClip);
        StartCoroutine(ReStart());
    }
    public IEnumerator ReStart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
    public void ShowHand(bool isLeft,bool isShow)
    {
        if (isLeft)
            LeftHand.SetActive(isShow);
        else
            RightHand.SetActive(isShow);
    }
    private void SpawnGrabItem(int id)
    {
        switch (id)
        {
            case 0:
                grabItems[0] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Magic") as GameObject).GetComponent<ItemGrabAttach>();            
                break;
            case 1:
                grabItems[1] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Sword") as GameObject).GetComponent<ItemGrabAttach>();
                break;
            case 2:
                grabItems[2] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Shield") as GameObject).GetComponent<ItemGrabAttach>();
                break;
            default:
                break;
        }
    }
    public void GameStart()
    {
        if (isGameStart)
            return;
        isGameStart = true;
        Title.transform.DOMoveY(24.0f, 10.0f).OnComplete(() => {
            EnemyManager.Instance.IsSpawn = true;
            StartCoroutine(SpawnPlayer());
            StartCoroutine(InitHPHeart());
            Title.SetActive(false);
        });
    }
    public void EnabledItem(int index)
    {
        if (grabItems[index] == null)
            return;
        ChangeMaterial(true, index);
        grabItems[index].ClearTemp();
    }
    public void PlayerDamage()
    {
        if (IsDeath)
            return;
        if(EnemyManager.Instance.EnemyID == 3)
        {
            playerHP -= 1;
            PlayerHPHearts[playerHP].SetActive(false);
            foreach (var item in playerList)
            {
                if (playerHP <= 0)
                {
                    playerHP = 0;
                    IsDeath = true;
                    item.Dead();
                }
                item.transform.DOShakePosition(0.5f, 0.5f);
            }
        }
        else
        {
            if (playerList[1].IsAttacking)
                return;
            EnemyManager.Instance.MagicPower += 1;
            playerHP -= 1;
            PlayerHPHearts[playerHP].SetActive(false);
            foreach (var item in playerList)
            {
                if (playerHP <= 0)
                {
                    playerHP = 0;
                    IsDeath = true;
                    item.Dead();
                }
                item.transform.DOShakePosition(0.5f, 0.5f);
            }
        }   
    }
    public void MagicDamage()
    {
        if (IsDeath)
            return;
        int index = playerHP;
        playerHP -= 2;
        PlayerHPHearts[index-1].SetActive(false);
        PlayerHPHearts[playerHP].SetActive(false);
        foreach (var item in playerList)
        {
            if (playerHP <= 0)
            {
                playerHP = 0;
                IsDeath = true;
                item.Dead();
            }
            item.transform.DOShakePosition(0.5f,0.5f);
        }
    }
    public void ChangeMaterial(bool isEnable,int index)
    {
        switch (index)
        {
            case 1:
                if (isEnable)
                {
                    IsCanAttack = true;
                    grabItems[index].GetComponentInChildren<MeshRenderer>().material = Normals[0];
                }
                else
                {
                    grabItems[index].GetComponentInChildren<MeshRenderer>().material = Fades[0];
                }
                break;
            case 2:
                if (isEnable)
                {
                    IsCanAttack = true;
                    grabItems[index].GetComponent<MeshRenderer>().material = Normals[1];
                }
                else
                {
                    grabItems[index].GetComponent<MeshRenderer>().material = Fades[1];
                }
                break;
            default:
                break;
        }
    }
    public void ClearAllTemp()
    {
        foreach (var item in grabItems)
        {
            if (item != null)
                item.ClearTemp();
        }
    }
    IEnumerator InitHPHeart()
    {
        for (int i = 0; i < playerHP; i++)
        {
            if (!PlayerHPHearts[i].activeInHierarchy)
                PlayerHPHearts[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void PlusHP(int hp)
    {
        playerHP += hp;
        if(playerHP>7)
        {
            playerHP = 7;
        }
        StartCoroutine(InitHPHeart());
    }
}


