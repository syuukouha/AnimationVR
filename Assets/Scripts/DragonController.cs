using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : Singleton<DragonController> {

    private Animator dragonAnimator;
    private Rigidbody rigid;
    // Use this for initialization
    void Start ()
    {
        dragonAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private IEnumerator DragonDeath()
    {
        yield return new WaitForSeconds(7.0f);
        rigid.isKinematic = false;
        rigid.useGravity = true;
    }
    public void Attack(bool value)
    {
        dragonAnimator.SetBool("Attack", value);
    }
    public void Death()
    {
        StartCoroutine(DragonDeath());
    }
}
