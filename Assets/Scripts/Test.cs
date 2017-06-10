using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Test : MonoBehaviour {
    public Animator PlayerAnimator;
    [SerializeField]
    private Animator dragonAnimator;
    public GameObject shibo;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            PlayerAnimator.SetTrigger("Attack");
            dragonAnimator.SetBool("Attack", false);
            Invoke("DragonDeath", 7.0f);
        }
        if (other.name == "Shield")
        {
            PlayerAnimator.SetTrigger("Defence");
        }
        gameObject.SetActive(false);
        transform.DOMoveZ(18.0f, 1.0f).SetDelay(3.0f); ;
    }

    private void DragonDeath()
    {
        Rigidbody2D[] rigids = dragonAnimator.gameObject.GetComponentsInChildren<Rigidbody2D>();
        for (int i = 0; i < rigids.Length; i++)
        {
            rigids[i].gravityScale = 1;
        }
        shibo.SetActive(false);
    }
}
