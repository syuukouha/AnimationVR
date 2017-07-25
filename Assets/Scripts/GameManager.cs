using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager> {
    private bool isHaveSword;
    private bool isHaveShield;
    public LightController[] Lights;
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

    public bool IsChoised;

    public GameObject ChoisePrefab;
    private Player knight;
    private Player swordsman;
    private Player wizard;

    // Use this for initialization
    void Start ()
    {
        isHaveShield = false;
        isHaveSword = false;
        IsChoised = false;
    }

    // Update is called once per frame
    void Update () {
		if(isHaveSword && isHaveShield)
        {
            isHaveShield = false;
            isHaveSword = false;
            weaponPlatform.transform.DOMoveY(-1f, 1f);
            StartCoroutine(SpawnPlayer());
            Choise();
        }
        #region TestCode
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            isHaveShield = true;
            isHaveSword = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Lights[0].ChangeLightIntensity(0, 3,0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!IsChoised)
            {
                Attack();
                IsChoised = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!IsChoised)
            {
                Defence();
                IsChoised = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!IsChoised)
            {
                Magic();
                IsChoised = true;
            }
        }
        #endregion
    }

    public void Choise()
    {
        IsChoised = false;
        //Instantiate(ChoisePrefab);
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(3f);
        knight = Instantiate(ResourcesManager.Instance.GetAsset("Character/Knight") as GameObject).GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        wizard = Instantiate(ResourcesManager.Instance.GetAsset("Character/Wizard") as GameObject).GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        swordsman = Instantiate(ResourcesManager.Instance.GetAsset("Character/Swordsman") as GameObject).GetComponent<Player>();
    }
    public void Attack()
    {
        swordsman.RotatePanel();
    }
    public void Defence()
    {
        knight.RotatePanel();
    }
    public void Magic()
    {
        wizard.RotatePanel();
    }
    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(4f);
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


