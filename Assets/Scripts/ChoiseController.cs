using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChoiseController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            PlayerController.Instance.Attack();
            DragonController.Instance.Attack(false);
            DragonController.Instance.Death();
        }
        if (other.name == "Shield")
        {
            PlayerController.Instance.Defence();
        }
        gameObject.SetActive(false);
        transform.DOMoveZ(18.0f, 1.0f).SetDelay(3.0f); ;
    }
}
