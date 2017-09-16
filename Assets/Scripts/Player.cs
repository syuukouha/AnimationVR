using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int PlayerID;
    public AudioClip AttackClip;
    public AudioClip DeathClip;
    private AudioSource audioSource;
    private bool isAttacking;

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
        isAttacking = false;
    }
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            if (PlayerID != 2)
                EnemyManager.Instance.Damage();
            transform.Find("Attack").gameObject.SetActive(true);
            transform.Find("Idle").gameObject.SetActive(false);
            audioSource.PlayOneShot(AttackClip);
            yield return new WaitForSeconds(3.0f);
            transform.Find("Attack").gameObject.SetActive(false);
            transform.Find("Idle").gameObject.SetActive(true);
            if (PlayerID != 0)
                GameManager.Instance.EnabledItem(PlayerID);
            isAttacking = false;
        }
    }
    private void CreateEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        Destroy(go, 3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "projectile")
        {
            switch (PlayerID)
            {
                case 0:
                    CreateEffect(ResourcesManager.Instance.GetAsset("Effects/HitWizard") as GameObject);
                    GameObject go1 = Instantiate(ResourcesManager.Instance.GetAsset("Effects/AttackWizard") as GameObject);
                    GameObject go2 = Instantiate(ResourcesManager.Instance.GetAsset("Effects/MagicRing") as GameObject);
                    Destroy(go1,3.0f);
                    Destroy(go2, 3.0f);
                    break;
                case 1:
                    CreateEffect(ResourcesManager.Instance.GetAsset("Effects/HitSword") as GameObject);
                    CreateEffect(ResourcesManager.Instance.GetAsset("Effects/AttackSword") as GameObject);
                    break;
                case 2:
                    CreateEffect(ResourcesManager.Instance.GetAsset("Effects/DefenceKnight") as GameObject);
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            StartCoroutine(Attack());
        }
    }
    public void Dead()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        transform.Find("DeathCollider").gameObject.SetActive(true);
        Destroy(this.gameObject, 3f);
    }

}
