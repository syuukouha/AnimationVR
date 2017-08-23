using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {
    public AudioClip AttackClip;
    private AudioSource audioSource;
    private bool isDead = false;
    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).OnComplete(() =>
        {
            transform.Find("Attack").gameObject.SetActive(true);
        });
	}
	
    public void Attack()
    {
        if (isDead)
            return;
        audioSource.PlayOneShot(AttackClip);
        transform.DORotate(new Vector3(90f, 0f, 0f), 1f);
        transform.DORotate(new Vector3(90f, 0, 180f), 1f).SetDelay(3f).OnComplete(()=> {
            GameManager.Instance.PlayerDamage();
            EnemyManager.Instance.TimerStart = true;
        });
    }
    public void Dead()
    {
        isDead = true;
        transform.DOMoveY(-5f, 5f);
        Destroy(this.gameObject, 5f);
    }


}
