using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragonController : Singleton<DragonController> {

    private Animator dragonAnimator;
    private Rigidbody rigid;
    private bool dragonStartComplete;
    private int index;
    private float thinkTimer;
    public Transform AttackSpawnPos;
    private GameObject AttackEffect;
    // Use this for initialization
    void Start ()
    {
        dragonAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        dragonStartComplete = false;
        thinkTimer = 4f;
    }

    private IEnumerator DragonDeath()
    {
        yield return new WaitForSeconds(7.0f);
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }
    public void Attack()
    {
        dragonAnimator.SetTrigger("Attack");
    }
    public void Roar()
    {
        dragonAnimator.SetTrigger("Roar");
    }
    public void Death()
    {
        StartCoroutine(DragonDeath());
    }

    private void Update()
    {
        if (GameManager.Instance.DragonStart)
        {
            DragonStart();
        }
        if (dragonStartComplete)
        {

            thinkTimer -= Time.deltaTime;
            if(thinkTimer <= 0)
            {
                int index = Random.Range(0, 2);
                if (index == 0)
                {
                    Attack();
                    thinkTimer = 8f;
                }
                else
                {
                    Roar();
                    thinkTimer = 4f;
                }
            }
        }
    }
    void DragonStart()
    {
        transform.DOMoveY(-5f, 5f).OnComplete(() => {
            GameManager.Instance.DragonStart = false;
            dragonStartComplete = true;
            GameManager.Instance.Choise();

        });
    }
    public void SpawnEffect(GameObject effect)
    {
        AttackEffect = Instantiate(effect, AttackSpawnPos.position, effect.transform.rotation);
    }
    public void DestroyEffect()
    {
        Destroy(AttackEffect);
        PlayerController.Instance.DefenceEnd(true);
        Debug.Log("DefenceEnd");
    }
}
