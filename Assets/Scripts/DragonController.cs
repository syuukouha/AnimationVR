using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragonController : MonoBehaviour {

    private Animator dragonAnimator;
    private Rigidbody rigid;
    private bool dragonDebutComplete;
    private int index;
    private float thinkTimer;
    public Transform AttackSpawnPos;
    private GameObject AttackEffect;
    public AudioSource audioSource;
    // Use this for initialization
    void Start ()
    {
        dragonAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        dragonDebutComplete = false;
        thinkTimer = 4f;
        DragonDebut();
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
        rigid.isKinematic = false;
        rigid.useGravity = true;
        Destroy(this.gameObject, 3f);
    }

    private void Update()
    {
        if (dragonDebutComplete)
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
    void DragonDebut()
    {
        PlaySound(ResourcesManager.Instance.GetAsset("Sounds/Roar") as AudioClip);
        transform.DOMoveY(-2f, 5f).OnComplete(() => {
            dragonDebutComplete = true;
            //GameManager.Instance.Choise();
        });
    }
    public void SpawnEffect(GameObject effect)
    {
        AttackEffect = Instantiate(effect, AttackSpawnPos.position, effect.transform.rotation);
    }
    public void DestroyEffect()
    {
        Destroy(AttackEffect);
        GameManager.Instance.PlayerController.DefenceEnd(true);
        Debug.Log("DefenceEnd");
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
