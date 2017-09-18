using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dragon : MonoBehaviour
{
    public GameObject FireAttackEffect;
    public GameObject NormalAttackEffect;

    private bool isTimerStart;
    private AudioSource audioSource;
    private bool isDead = false;

    private float timer;
    private float attackTimer;
    // Use this for initialization
    void Start ()
    {
        timer = 0;
        attackTimer = 5;
        isTimerStart = true;
        audioSource = GetComponent<AudioSource>();
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
    }

    // Update is called once per frame
    void Update ()
    {
        if (isDead)
            return;
        if (isTimerStart)
            timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            timer = 0;
            isTimerStart = false;
            if (EnemyManager.Instance.HP <= 6)
            {
                if (Random.Range(0, 2) > 0)
                    Attack(1);
                else
                    Attack(2);
            }
            else if (EnemyManager.Instance.HP <= 3)
            {
                if (Random.Range(0, 10) > 2)
                    Attack(2);
                else
                    Attack(1);
            }
            else
            {
                Attack(1);
            }
        }

	}
    public void Attack(int attackID)
    {
        switch (attackID)
        {
            case 1:
                StartCoroutine(NormalAttack());
                break;
            case 2:
                StartCoroutine(FireAttack());
                break;
        }
    }
    IEnumerator NormalAttack()
    {
        GameManager.Instance.PlayerDamage();
        transform.Find("FireAttack").gameObject.SetActive(false);
        transform.Find("NormalAttack").gameObject.SetActive(true);
        transform.Find("Idle").gameObject.SetActive(false);
        //audioSource.PlayOneShot(AttackClip);
        yield return new WaitForSeconds(3.0f);
        transform.Find("NormalAttack").gameObject.SetActive(false);
        transform.Find("FireAttack").gameObject.SetActive(false);
        transform.Find("Idle").gameObject.SetActive(true);
        isTimerStart = true;
    }
    IEnumerator FireAttack()
    {
        GameManager.Instance.MagicDamage();
        transform.Find("FireAttack").gameObject.SetActive(true);
        transform.Find("NormalAttack").gameObject.SetActive(false);
        transform.Find("Idle").gameObject.SetActive(false);
        //audioSource.PlayOneShot(AttackClip);
        yield return new WaitForSeconds(3.0f);
        transform.Find("NormalAttack").gameObject.SetActive(false);
        transform.Find("FireAttack").gameObject.SetActive(false);
        transform.Find("Idle").gameObject.SetActive(true);
        isTimerStart = true;
    }
    public void Dead()
    {
        isDead = true;
        isTimerStart = false;
        transform.DOShakePosition(20.0f, 0.1f);
        transform.DOMoveY(-5f, 20.0f);
        Destroy(this.gameObject, 20.0f);
    }
}
