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
        }
        if (other.name == "Shield")
        {
            PlayerController.Instance.Defence();
        }
        GameManager.Instance.IsChoised = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, 15f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerController.Instance.Attack();
            GameManager.Instance.IsChoised = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 15f);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerController.Instance.Defence();
            GameManager.Instance.IsChoised = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 15f);

        }
    }
}
