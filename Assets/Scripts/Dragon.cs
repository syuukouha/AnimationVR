using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Dragon : MonoBehaviour
{
    private AudioSource audioSource;
    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        transform.DORotate(new Vector3(90f, 180f, 0f), 1f);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
