using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : Singleton<PlayerController>
{
    private Animator playerAnimator;
    [SerializeField]
    private Transform cube;
    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Walk(true);
        transform.DOMoveX(-2.0f, 5.0f).OnComplete(()=> {
            Walk(false);
            DragonController.Instance.Attack(true);
            cube.transform.DOMoveZ(1.0f, 1.0f).SetDelay(3.0f);
        });
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
        playerAnimator.SetTrigger("Defence");
    }

}
