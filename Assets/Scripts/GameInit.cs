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
        ResourcesManager.Instance.Load("Sounds/KinghtAttack", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDeath", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage1", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/PlayerDamage2", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Victory", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Defence", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Sounds/Start", typeof(AudioClip), this);
        ResourcesManager.Instance.Load("Characters/Knight", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Characters/Sword", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Characters/Wizard", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/Magic", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/Shield", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Items/Sword", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/HitSword", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/HitWizard", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/DefenceKnight", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/AttackSword", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/AttackWizard", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Effects/MagicRing", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Materials/SwordFade", typeof(GameObject), this);
        ResourcesManager.Instance.Load("Materials/ShieldFade", typeof(GameObject), this);


    }
}
