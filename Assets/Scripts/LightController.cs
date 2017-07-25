using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LightController : MonoBehaviour {
    private Light light;
    [SerializeField]
    private float endIntensity;
    [SerializeField]
    float duration;
    [SerializeField]
    float delay;
    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
        ChangeLightIntensity(endIntensity,duration,delay);
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void ChangeLightIntensity(float endIntensity,float duration,float delay)
    {
        light.DOIntensity(endIntensity, duration).SetDelay(delay);
    }
}
