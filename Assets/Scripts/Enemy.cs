using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).OnComplete(() =>
        {
            transform.Find("Attack").gameObject.SetActive(true);
        });
	}
	
    public void Attack()
    {
        transform.DORotate(new Vector3(90f, 0f, 0f), 1f);
        transform.DORotate(new Vector3(90f, 0, 180f), 1f).SetDelay(3f).OnComplete(()=> {
            GameManager.Instance.PlayerDamage();
            EnemyManager.Instance.TimerStart = true;
        });
    }
    public void Dead()
    {
        transform.DOScale(0, 0.3f).OnComplete(()=> {
            Destroy(this.gameObject);
        });
    }


}
