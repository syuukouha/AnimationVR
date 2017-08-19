using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public LightController[] Lights;

    public bool IsChoised;

    private Player knight;
    private Player swordsman;
    private Player wizard;

    // Use this for initialization
    void Start ()
    {
        IsChoised = false;
        Lights[0].OpenLight();
    }

    // Update is called once per frame
    void Update ()
    {
        #region TestCode
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(SpawnPlayer());
            Choise();
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
    }
    private IEnumerator SpawnPlayer()
    {
        yield return new WaitForSeconds(3f);
        knight = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Knight") as GameObject).GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        wizard = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Wizard") as GameObject).GetComponent<Player>();
        yield return new WaitForSeconds(1f);
        swordsman = Instantiate(ResourcesManager.Instance.GetAsset("Characters/Swordsman") as GameObject).GetComponent<Player>();
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


