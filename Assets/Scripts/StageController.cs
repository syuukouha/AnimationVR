using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StageController : Singleton<StageController> {
    public Transform Forest;
    public Transform Castle;
    public Transform Mountain;
    public Transform Plain;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {


    }
    public IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(3f);
        Forest.DORotate(Mountain.rotation.eulerAngles, 3.0f);
        Castle.DORotate(Forest.rotation.eulerAngles, 3.0f);
        Mountain.DORotate(Plain.rotation.eulerAngles, 3.0f);
        Plain.DORotate(Castle.rotation.eulerAngles, 3.0f);
    }
}
