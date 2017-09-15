using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using VRTK;
public class GameManager : Singleton<GameManager>
{
    public int HP = 35;
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
    public int MagicPower = 0;
    public bool isResetItem = false;
    public Material[] Normals;
    public Material[] Fades;


    private GrabItem[] grabItems = new GrabItem[3];

    private List<Player> playerList = new List<Player>();
    private bool isGameStart;
    // Use this for initialization
    void Start ()
    {
        IsSwordGrabbed = false;
        IsShieldGrabbed = false;
        isGameStart = false;
        IsDeath = true;
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
        if (ResourcesManager.Instance.IsComplete)
        {
            if (grabItems[1] == null)
                SpawnGrabItem(1);
            if (grabItems[2] == null)
                SpawnGrabItem(2);
            PointLight.DOIntensity(1, 3.0f);
            SpotLight.DOIntensity(1, 3.0f).SetDelay(3.0f);
        }
        if (IsSwordGrabbed && IsShieldGrabbed)
            GameStart();
        if (MagicPower >= 2 && grabItems[0] == null)
        {
            SpawnGrabItem(0);
            Destroy(grabItems[1]);
            Destroy(grabItems[2]);
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
        IsDeath = false;
    }
    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(4f);
        yield return new WaitForSeconds(3f);
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
                grabItems[0] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Magic") as GameObject).GetComponent<GrabItem>();            
                break;
            case 1:
                grabItems[1] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Sword") as GameObject).GetComponent<GrabItem>();
                break;
            case 2:
                grabItems[2] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Shield") as GameObject).GetComponent<GrabItem>();
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
            Title.SetActive(false);
        });
    }
    public void EnabledItem(int index)
    {
        if (grabItems[index] == null)
            return;
        isResetItem = true;
    }
    public void PlayerDamage()
    {
        foreach (var item in playerList)
        {
            if (item.PlayerID == 2 && item.IsAttacking)
                return;
            HP -= 1;
            if (HP <= 0)
            {
                HP = 0;
                IsDeath = true;
                item.Dead();
            }
            item.transform.DOShakePosition(0.5f);
            item.transform.DOShakeRotation(0.5f);
        }
    }
    public void ChangeMaterial(bool isEnable)
    {
        //switch (itemType)
        //{
        //    case ItemType.Magic:
        //        break;
        //    case ItemType.Sword:
        //        if (isEnable)
        //            GetComponentInChildren<MeshRenderer>().material = normalMaterial;
        //        else
        //            GetComponentInChildren<MeshRenderer>().material = FadeMaterial;
        //        break;
        //    case ItemType.Shield:
        //        if (isEnable)
        //            GetComponent<MeshRenderer>().material = normalMaterial;
        //        else
        //            GetComponent<MeshRenderer>().material = FadeMaterial;
        //        break;
        //    default:
        //        break;
        //}
        
    }

}


