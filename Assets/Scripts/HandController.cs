using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class HandController : MonoBehaviour
{
    public VRTK_ControllerEvents ControllerEvents;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        ControllerEvents.TriggerPressed += ControllerEvents_TriggerPressed;
        ControllerEvents.TriggerReleased += ControllerEvents_TriggerReleased;

    }

    private void ControllerEvents_TriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        animator.SetBool("Grab", false);
    }

    private void ControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        animator.SetBool("Grab", true);
    }

    private void OnDestroy()
    {
        ControllerEvents.TriggerPressed -= ControllerEvents_TriggerPressed;
        ControllerEvents.TriggerReleased -= ControllerEvents_TriggerReleased;

    }
}
