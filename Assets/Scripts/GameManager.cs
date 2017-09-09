using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using VRTK;
public class GameManager : Singleton<GameManager>
{
    public Light PointLight;
    public Light SpotLight;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Title;
    [SerializeField]
    private VRTK_ControllerEvents[] controllerEvents;
    private GrabItem[] GrabItems = new GrabItem[3];

    private List<Player> playerList = new List<Player>();
    private bool isGameStart = false;
    // Use this for initialization
    void Start ()
    {
        PointLight.DOIntensity(1, 3.0f);
        SpotLight.DOIntensity(1, 3.0f).SetDelay(3.0f);
        for (int i = 0; i < controllerEvents.Length; i++)
        {
            controllerEvents[i].TriggerPressed += GameManager_TriggerPressed;
        }
    }

    private void GameManager_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (!isGameStart)
        {
            GameStart();
        }
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

        yield return new WaitForSeconds(1f);
        ReSpawnGrabItem(0);
        ReSpawnGrabItem(1);
        ReSpawnGrabItem(2);
        EnabledGrab(true);
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
    public void ReSpawnGrabItem(int id)
    {
        switch (id)
        {
            case 0:
                GrabItems[0] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Magic") as GameObject).GetComponent<GrabItem>();            
                break;
            case 1:
                GrabItems[1] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Sword") as GameObject).GetComponent<GrabItem>();
                break;
            case 2:
                GrabItems[2] = Instantiate(ResourcesManager.Instance.GetAsset("Items/Shield") as GameObject).GetComponent<GrabItem>();
                break;
            default:
                break;
        }
    }
    private void GameStart()
    {
        isGameStart = true;
        Title.transform.DOMoveY(25.0f, 10.0f).OnComplete(() => {
            EnemyManager.Instance.IsSpawn = true;
            StartCoroutine(SpawnPlayer());
            Title.SetActive(false);
        });
    }
    public void EnabledGrab(bool isEnabled)
    {
        for (int i = 0; i < GrabItems.Length; i++)
        {
            GrabItems[i].isGrabbable = isEnabled;
            if(GrabItems[i].isGrabbable)
            {
                GrabItems[i].touchHighlightColor = new Color(0, 255, 255,0);
            }else
            {
                GrabItems[i].touchHighlightColor = new Color(0, 0, 0, 0);

            }

        }
    }
    public void PlayerDamage()
    {
        foreach (var item in playerList)
        {
            item.HP -= 1;
            item.transform.DOShakePosition(0.5f);
            item.transform.DOShakeRotation(0.5f);
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < controllerEvents.Length; i++)
        {
            controllerEvents[i].TriggerPressed -= GameManager_TriggerPressed;
        }
    }

}


