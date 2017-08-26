using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int PlayerID;
    public int HP = 35;
    public AudioClip AttackClip;
    public AudioClip DeathClip;
    private AudioSource audioSource;
    private bool isAttacking;
    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
        isAttacking = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (HP <= 0)
        {
            HP = 0;
            Dead();
        }
    }
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            EnemyManager.Instance.Damage();
            transform.Find("Attack").gameObject.SetActive(true);
            transform.Find("Idle").gameObject.SetActive(false);

            audioSource.PlayOneShot(AttackClip);
            yield return new WaitForSeconds(3.0f);
            transform.Find("Attack").gameObject.SetActive(false);
            transform.Find("Idle").gameObject.SetActive(true);  
            GameManager.Instance.ReSpawnGrabItem(PlayerID);
            isAttacking = false;
        }

    }
    private void CreateEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        Destroy(go, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "projectile")
        {
            CreateEffect(ResourcesManager.Instance.GetAsset("Effects/Hit") as GameObject);
            Destroy(other.gameObject);
            StartCoroutine(Attack());
        }
    }
    private void Dead()
    {
        GameManager.Instance.EnabledGrab(false);
        transform.DOMoveY(-5f, 5f);
        Destroy(this.gameObject, 5f);
    }

}
