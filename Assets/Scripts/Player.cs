using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player>
{
    public GameObject Effect;
    // Use this for initialization
    void Start () {
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Attack()
    {
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
        AttackEffect();
        transform.DORotate(new Vector3(90f, 0f, 0f), 1f).SetDelay(3f).OnComplete(()=> {
            if (GameManager.Instance.IsChoised)
            {
                GameManager.Instance.Choise();
                Debug.Log("Choise");
            }
        });
    }
    private void AttackEffect()
    {
        GameObject effect = Instantiate(Effect);
        effect.transform.position = this.transform.position;
        Destroy(effect, 2f);
    }
    public void PlayerDebut()
    {
        transform.DOScale(Vector3.one * 0.5f, 0.3f);
    }


}
