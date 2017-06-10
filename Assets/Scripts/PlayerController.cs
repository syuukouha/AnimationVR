using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private Animator dragonAnimator;
    [SerializeField]
    private Transform cube;
    // Use this for initialization
    void Start()
    {
        playerAnimator.SetBool("Walk", true);
        transform.DOMoveX(-2.0f, 5.0f).OnComplete(()=> {
            playerAnimator.SetBool("Walk", false);
            dragonAnimator.SetBool("Attack", true);
            cube.transform.DOMoveZ(1.0f, 1.0f).SetDelay(3.0f);
        });
	}

}
