using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : Singleton<PlayerController>
{
    private Animator playerAnimator;
    private Rigidbody rigid;
    private bool isDefence;

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isDefence = false;
    }
    public void Walk(bool value)
    {
        playerAnimator.SetBool("Walk", value);
    }
    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
    }
    public void Defence()
    {
        isDefence = true;
        playerAnimator.SetTrigger("Defence");
    }
    public void DefenceEnd(bool value)
    {
        isDefence = false;
        playerAnimator.SetBool("DefenceEnd", value);
    }
    public void Death()
    {
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }
    private void Update()
    {
        if (GameManager.Instance.PlayerDebut)
        {
            rigid.useGravity = true;
            GameManager.Instance.PlayerDebut = false;
        }
    }

    void PlayerDebut()
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;
        Walk(true);
        transform.DOMove(new Vector3(-2f, 0.8f, transform.position.z), 3f).OnComplete(()=> {
            GameManager.Instance.DragonDebut = true;
            Walk(false);
        });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            PlayerDebut();
            Debug.Log("PlayerDebut");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Platform")
        {
            PlayerDebut();
            Destroy(other.gameObject);
            Debug.Log("PlayerDebut");
        }
        if (other.tag == "DragonAttack")
        {
            if (isDefence)
            {
                DefenceEnd(false);
                Debug.Log("Defence");
            }
            else
            {
                Debug.Log("Damage");
                GameManager.Instance.PlayerHP--;
            }
        }
    }

    public void Choise()
    {
        if (GameManager.Instance.IsChoised)
        {
            GameManager.Instance.Choise();
            Debug.Log("Choise");
        }
    }
    public void AttackEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        Destroy(go, 2f);
    }
}
