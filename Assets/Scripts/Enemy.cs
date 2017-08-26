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
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
	}
	
    public void Attack()
    {
        if (isDead)
            return;
        StartCoroutine(ChangePanel());

    }
    public void Dead()
    {
        isDead = true;
        transform.DOMoveY(-5f, 5f);
        Destroy(this.gameObject, 5f);
    }
    IEnumerator ChangePanel()
    {
        GameManager.Instance.PlayerDamage();
        transform.Find("Attack").gameObject.SetActive(true);
        transform.Find("Idle").gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        transform.Find("Attack").gameObject.SetActive(false);
        transform.Find("Idle").gameObject.SetActive(true);
        EnemyManager.Instance.TimerStart = true;
    }

}
