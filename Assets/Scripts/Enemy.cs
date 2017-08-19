using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Attack()
    {
        transform.DORotate(new Vector3(90f, -180f, 0f), 1f);
        transform.DORotate(new Vector3(90f, 0, 0f), 1f).SetDelay(3f);
    }
}
