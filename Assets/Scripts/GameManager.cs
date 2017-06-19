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

    public bool DragonDebut
    {
        get
        {
            return dragonDebut;
        }

        set
        {
            dragonDebut = value;
        }
    }

    public bool PlayerDebut
    {
        get
        {
            return playerDebut;
        }

        set
        {
            playerDebut = value;
        }
    }

    public bool IsChoised;
    public int DragonHP;
    public int PlayerHP;

    // Use this for initialization
    void Start ()
    {
        isHaveShield = false;
        isHaveSword = false;
        playerDebut = false;
        dragonDebut = false;
        IsChoised = false;
        DragonHP = 5;
        PlayerHP = 3;
    }

    // Update is called once per frame
    void Update () {
		if(isHaveSword && isHaveShield)
        {
            isHaveShield = false;
            isHaveSword = false;
            weaponPlatform.SetActive(false);
            PlayerDebut = true;
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
            DragonController.Instance.Death();
        }
        if (PlayerHP <= 0)
        {
            PlayerController.Instance.Death();
        }

    }

    public void Choise()
    {
        IsChoised = false;
        ChoiseController.Instance.ShowChoise();
    }
}


