using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        transform.DOScale(Vector3.one * 0.5f, 0.3f);
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void RotatePanel()
    {
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
        RotatePanelEffect(ResourcesManager.Instance.GetAsset("Effect/RotatePanelEffect") as GameObject);
        transform.DORotate(new Vector3(90f, 0f, 0f), 1f).SetDelay(3f).OnComplete(()=> {
            if (GameManager.Instance.IsChoised)
            {
                GameManager.Instance.Choise();
                Debug.Log("Choise");
            }
        });
    }
    private void RotatePanelEffect(GameObject effect)
    {
        GameObject go = Instantiate(effect);
        go.transform.position = this.transform.position;
        Destroy(go, 2f);
    }

}
