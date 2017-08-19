using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LightController : MonoBehaviour
{
    [SerializeField]
    private float endIntensity;
    [SerializeField]
    private  float duration;
    [SerializeField]
    private float delay;

    private Light _light;
    void Start()
    {
        _light = GetComponent<Light>();
    }
    public void OpenLight()
    {
        _light.DOIntensity(endIntensity, duration).SetDelay(delay);
    }
    public void CloseLight()
    {
        _light.DOIntensity(0f, duration);
    }

}
