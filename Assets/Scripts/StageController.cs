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
    public GameObject BigExplosionEffect;
    public AudioClip[] BGM;

    public IEnumerator ChangeStage()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.Instance.StopBGM();
        Forest.DORotate(Mountain.rotation.eulerAngles, 7.0f);
        Castle.DORotate(Forest.rotation.eulerAngles, 7.0f);
        Mountain.DORotate(Plain.rotation.eulerAngles, 7.0f);
        Plain.DORotate(Castle.rotation.eulerAngles, 7.0f).OnComplete(()=> {
            int stageIndex = EnemyManager.Instance.EnemyID;
            SoundManager.Instance.PlayBGM(BGM[stageIndex]);
            EnemyManager.Instance.IsSpawn = true;
            if (stageIndex == 3)
                BigExplosionEffect.SetActive(true);
        });
    }
}
