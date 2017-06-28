using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ChoiseController : Singleton<ChoiseController>
{
    private Tweener tweener;
    private void Start()
    {
        tweener = transform.DOScale(Vector3.one, 1f);
        tweener.Pause();
        tweener.SetAutoKill(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sword")
        {
            GameManager.Instance.PlayerController.Attack();
            GameManager.Instance.DragonHP--;
            other.GetComponent<GrabItem>().swordControllerActions.TriggerHapticPulse(5000f, 0.1f, 0.005f);

        }
        if (other.name == "Shield")
        {
            GameManager.Instance.PlayerController.Defence();
            other.GetComponent<GrabItem>().shieldControllerActions.TriggerHapticPulse(5000f, 0.5f, 0.005f);
        }
        GameManager.Instance.IsChoised = true;
        HideChoise();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.Instance.PlayerController.Attack();

            GameManager.Instance.IsChoised = true;
            //transform.position = new Vector3(transform.position.x, transform.position.y, 15f);
            HideChoise();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameManager.Instance.PlayerController.Defence();
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
