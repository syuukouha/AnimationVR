using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using VRTK;

public class ChoiseController : MonoBehaviour
{
    private Tweener tweener;
    private void Start()
    {
        //tweener = transform.DOScale(Vector3.one, 1f);
        //tweener = transform.DOMoveZ(1f, 1f);
        //tweener.Pause();
        //tweener.SetAutoKill(false);
        transform.DOMoveZ(1f, 1f);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.name == "Sword")
    //    {
    //        GameManager.Instance.PlayerController.Attack();
    //        GameManager.Instance.DragonHP--;
    //    }
    //    if (other.name == "Shield")
    //    {
    //        GameManager.Instance.PlayerController.Defence();
    //    }
    //    GameManager.Instance.IsChoised = true;
    //    HideChoise();
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.IsChoised)
        {
            if (collision.gameObject.name == "Sword")
            {
                //GameManager.Instance.PlayerController.Attack();
                Player.Instance.Attack();
                GameManager.Instance.DragonHP--;
            }
            if (collision.gameObject.name == "Shield")
            {
                //GameManager.Instance.PlayerController.Defence();
            }
            GameManager.Instance.IsChoised = true;
            HideChoise();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //GameManager.Instance.PlayerController.Attack();

            GameManager.Instance.IsChoised = true;
            HideChoise();

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //GameManager.Instance.PlayerController.Defence();
            GameManager.Instance.IsChoised = true;
            HideChoise();
        }
    }
    public void ShowChoise()
    {
        tweener.PlayForward();
    }
    public void HideChoise()
    {
        //tweener.PlayBackwards();
    }
}
