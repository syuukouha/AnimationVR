using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRTK;

public class ChoiseController : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveZ(1f, 1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.IsChoised)
        {
            if (collision.gameObject.name == "Sword")
            {
                GameManager.Instance.Attack();
            }
            if (collision.gameObject.name == "Shield")
            {
                GameManager.Instance.Defence();
            }
            GameManager.Instance.IsChoised = true;
        }
    }
}
