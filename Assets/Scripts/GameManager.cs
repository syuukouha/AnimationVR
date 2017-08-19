﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameObject LeftHand;
    public GameObject RightHand;
    public GrabItem[] GrabItems;

    private List<Player> playerList = new List<Player>();
    // Use this for initialization
    void Start ()
    {
        EnabledGrab(false);
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update ()
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
        EnabledGrab(true);
        EnemyManager.Instance.TimerStart = true;
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
    public void ReSpawn(int id)
    {
        switch (id)
        {
            case 0:
                StartCoroutine( CreateItem(ResourcesManager.Instance.GetAsset("Items/Magic") as GameObject));
                break;
            case 1:
                StartCoroutine(CreateItem(ResourcesManager.Instance.GetAsset("Items/Sword") as GameObject));

                break;
            case 2:
                StartCoroutine(CreateItem(ResourcesManager.Instance.GetAsset("Items/Shield") as GameObject));
                break;
            default:
                break;
        }
    }
    IEnumerator CreateItem(GameObject item)
    {
        yield return new WaitForSeconds(3f);
        Instantiate(item);
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        LightController.Instance.OpenLight();
        yield return new WaitForSeconds(1f);
        EnemyManager.Instance.IsSpawn = true;
        StartCoroutine(SpawnPlayer());
    }
    public void EnabledGrab(bool isEnabled)
    {
        for (int i = 0; i < GrabItems.Length; i++)
        {
            GrabItems[i].isGrabbable = isEnabled;
        }
    }
    public void PlayerDamage()
    {
        foreach (var item in playerList)
        {
            item.HP -= 1;
        }
    }

}


