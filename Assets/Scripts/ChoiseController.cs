using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChoiseController : Singleton<ChoiseController>
{
    private Tweener tweener;
    private void Start()
    {
        tweener = transform.DORotate(new Vector3(0f, 180f, 0f), 0.3f);
        tweener.Pause();
        tweener.SetAutoKill(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            PlayerController.Instance.Attack();
            GameManager.Instance.DragonHP--;
        }
        if (other.name == "Shield")
        {
            PlayerController.Instance.Defence();
        }
        GameManager.Instance.IsChoised = true;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 15f);
        HideChoise();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerController.Instance.Attack();
            GameManager.Instance.IsChoised = true;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 15f);
            HideChoise();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerController.Instance.Defence();
            GameManager.Instance.IsChoised = true;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 15f);
            HideChoise();
        }
    }
    public void ShowChoise()
    {
        tweener.PlayForward();
    }
    public void HideChoise()
    {
        tweener.PlayBackwards();
    }
}
