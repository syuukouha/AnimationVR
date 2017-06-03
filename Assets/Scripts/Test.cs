using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Sword")
        {
            Debug.Log("Attack");
        }
        if (other.name == "Shield")
        {
            Debug.Log("Defens");
        }
    }
}
