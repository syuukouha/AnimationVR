using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : Singleton<GameManager> {
    private bool isHaveSword;
    private bool isHaveShield;

    private bool playerStart;
    private bool dragonStart;

    [SerializeField]
    private GameObject weaponPlatform;
    [SerializeField]
    private GameObject choise;
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

    public bool DragonStart
    {
        get
        {
            return dragonStart;
        }

        set
        {
            dragonStart = value;
        }
    }

    public bool PlayerStart
    {
        get
        {
            return playerStart;
        }

        set
        {
            playerStart = value;
        }
    }

    public bool IsChoised;
    
    // Use this for initialization
    void Start ()
    {
        isHaveShield = false;
        isHaveSword = false;
        playerStart = false;
        dragonStart = false;
        IsChoised = false;
    }

    // Update is called once per frame
    void Update () {
		if(isHaveSword && isHaveShield)
        {
            isHaveShield = false;
            isHaveSword = false;
            weaponPlatform.SetActive(false);
            PlayerStart = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isHaveShield = true;
            isHaveSword = true;
        }
    }

    public void Choise()
    {
        IsChoised = false;
        choise.transform.DOMoveZ(0.7f, 1f);
    }
}


