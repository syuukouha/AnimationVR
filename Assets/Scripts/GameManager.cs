using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager> {
    private bool isHaveSword;
    private bool isHaveShield;

    private bool playerDebut;
    private bool dragonDebut;

    [SerializeField]
    private GameObject weaponPlatform;
    public bool IsHaveSword
    {
        get
        {
            return isHaveSword;
        }

        set
        {
            isHaveSword = value;
        }
    }

    public bool IsHaveShield
    {
        get
        {
            return isHaveShield;
        }

        set
        {
            isHaveShield = value;
        }
    }

    public PlayerController PlayerController
    {
        get
        {
            return playerController;
        }

        set
        {
            playerController = value;
        }
    }

    public DragonController DragonController
    {
        get
        {
            return dragonController;
        }

        set
        {
            dragonController = value;
        }
    }

    public bool IsChoised;
    public int DragonHP;
    public int PlayerHP;
    private PlayerController playerController;
    private DragonController dragonController;
    public GameObject PlayerPrefab;
    public GameObject DragonPrefab;
    // Use this for initialization
    void Start ()
    {
        isHaveShield = false;
        isHaveSword = false;
        playerDebut = false;
        dragonDebut = false;
        IsChoised = false;
        DragonHP = 7;
        PlayerHP = 3;
    }

    // Update is called once per frame
    void Update () {
		if(isHaveSword && isHaveShield)
        {
            isHaveShield = false;
            isHaveSword = false;
            weaponPlatform.SetActive(false);
            SpawnPlayer();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isHaveShield = true;
            isHaveSword = true;
        }
        if (DragonHP <= 0)
        {
            DragonHP = 7;
            StartCoroutine(Victory());
        }
        if (PlayerHP <= 0)
        {
            PlayerHP = 3;
            PlayerController.Death();
            StartCoroutine(ReStart());
        }

    }

    public void Choise()
    {
        IsChoised = false;
        ChoiseController.Instance.ShowChoise();
    }
    public void SpawnPlayer()
    {
        GameObject player = Instantiate(PlayerPrefab);
        playerController = player.GetComponent<PlayerController>();
    }
    public void SpawnDragon()
    {
        GameObject dragon = Instantiate(DragonPrefab);
        dragonController = dragon.GetComponent<DragonController>();
    }

    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(4f);
        dragonController.Death();
        yield return new WaitForSeconds(3f);
        SoundManager.Instance.PlaySE(ResourcesManager.Instance.GetAsset("Sounds/Victory") as AudioClip);
        StartCoroutine(ReStart());
    }
    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}


