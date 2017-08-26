using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StageController : Singleton<StageController>
{
    public Transform Forest;
    public Transform Castle;
    public Transform Mountain;
    public Transform Plain;

    public AudioClip[] BGM;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {


    }
    public IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.Instance.StopBGM();
        Forest.DORotate(Mountain.rotation.eulerAngles, 7.0f);
        Castle.DORotate(Forest.rotation.eulerAngles, 7.0f);
        Mountain.DORotate(Plain.rotation.eulerAngles, 7.0f);
        Plain.DORotate(Castle.rotation.eulerAngles, 7.0f).OnComplete(()=> {
            SoundManager.Instance.PlayBGM(BGM[EnemyManager.Instance.EnemyID]);
            EnemyManager.Instance.IsSpawn = true;
        });
    }
}
