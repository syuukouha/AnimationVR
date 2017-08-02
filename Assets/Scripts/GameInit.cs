using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour,IResourceListener

{
    public void OnLoaded(string assetPath, object asset)
    {

    }
    private void Start()
    {
        ResourcesManager.Instance.Load("Sounds/Roar", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDeath", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage1", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage2", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Victory", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Defence", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Start", typeof(AudioClip), this);
        //ResourcesManager.Instance.Load("Effect/RotatePanelEffect", typeof(GameObject), this);
        //ResourcesManager.Instance.Load("Effect/SpawnEffect", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Character/Knight", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Character/Swordsman", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Character/Wizard", typeof(GameObject), this);

    }
}
