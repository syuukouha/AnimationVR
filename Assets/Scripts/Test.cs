using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Animator PlayerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            PlayerAnimator.SetTrigger("Attack");
        }
        if (other.name == "Shield")
        {
            PlayerAnimator.SetTrigger("Defence");

        }
    }
}
