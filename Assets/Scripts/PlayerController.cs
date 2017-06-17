using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
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
    private void Update()
    {
        if (GameManager.Instance.PlayerStart)
        {
            rigid.useGravity = true;
            GameManager.Instance.PlayerStart = false;
        }
    }

    void PlayerStart()
    {
        rigid.useGravity = false;
        GetComponent<CapsuleCollider>().isTrigger = true;
        Walk(true);
        transform.DOMove(new Vector3(-3f, 0f, transform.position.z), 3f).OnComplete(()=> {
            GameManager.Instance.DragonStart = true;
            Walk(false);
        });
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Platform")
        {
            PlayerStart();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
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
}
