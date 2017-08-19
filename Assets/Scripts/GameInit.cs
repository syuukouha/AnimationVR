using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour,IResourceListener

{
    public void OnLoaded(string assetPath, object asset)
    {

    }
    private void Awake()
    {
        ResourcesManager.Instance.Load("Sounds/Roar", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDeath", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage1", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage2", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Victory", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Defence", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Start", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Characters/Knight", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Characters/Swordsman", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Characters/Wizard", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/BlueSphere", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/RedSphere", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/GoldSphere", typeof(GameObject), this);
    }
}
