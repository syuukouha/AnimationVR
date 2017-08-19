using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereController : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void SpawnEnergySphere()
    {
        GameObject go = Instantiate(ResourcesManager.Instance.GetAsset("Items/BlueSphere") as GameObject);
        go.transform.position = this.transform.position;
        go.GetComponent<Rigidbody>().AddForce(Vector3.up * 100f);
    }
}
