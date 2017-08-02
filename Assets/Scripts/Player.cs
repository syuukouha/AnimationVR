using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).OnComplete(() => {
            transform.Find("Attack").gameObject.SetActive(true);
        }); ;
        //CreateEffect(ResourcesManager.Instance.GetAsset("Effect/SpawnEffect") as GameObject);

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void RotatePanel()
    {
        transform.DORotate(new Vector3(90f, 360f, 0f), 1f);
        //CreateEffect(ResourcesManager.Instance.GetAsset("Effect/RotatePanelEffect") as GameObject);
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f).SetDelay(3f).OnComplete(()=> {
            if (GameManager.Instance.IsChoised)
            {
                GameManager.Instance.Choise();
                Debug.Log("Choise");
            }
        });
    }
    private void CreateEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        Destroy(go, 2f);
    }

}
