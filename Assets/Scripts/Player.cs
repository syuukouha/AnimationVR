using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public int HP = 5;

    // Use this for initialization
    void Start ()
    {
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).OnComplete(() =>
        {
            transform.Find("Attack").gameObject.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update ()
    {
        if (HP <= 0)
        {
            Dead();
        }
    }
    public void RotatePanel()
    {
        transform.DORotate(new Vector3(90f, 360f, 0f), 1f);
        //CreateEffect(ResourcesManager.Instance.GetAsset("Effect/RotatePanelEffect") as GameObject);
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).SetDelay(3f).OnComplete(()=> {
            EnemyManager.Instance.Damage();
        });
    }
    private void CreateEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        Destroy(go, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "projectile")
        {
            CreateEffect(ResourcesManager.Instance.GetAsset("Effect/Hit") as GameObject);
            Destroy(other.gameObject);
            RotatePanel();
        }
    }
    private void Dead()
    {
        GameManager.Instance.EnabledGrab(false);
        transform.DOScale(0, 0.3f).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }

}
