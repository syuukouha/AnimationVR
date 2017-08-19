using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;

public class ItemGrabAttach : VRTK_BaseGrabAttach
{
    public Vector3 AttachPosition;
    public Vector3 AttachRotation;
    public OvrAvatar LocalAvatar;
    public GrabItem grabItem;
    private bool checkStart = false;
    private List<Vector3> vectorTemp = new List<Vector3>(10);
    private float distance;
    protected override void Initialise()
    {
        tracked = false;
        climbable = false;
        kinematic = true;
    }
    /// <summary>
    /// The StartGrab method sets up the grab attach mechanic as soon as an object is grabbed. It is also responsible for creating the joint on the grabbed object.
    /// </summary>
    /// <param name="grabbingObject">The object that is doing the grabbing.</param>
    /// <param name="givenGrabbedObject">The object that is being grabbed.</param>
    /// <param name="givenControllerAttachPoint">The point on the grabbing object that the grabbed object should be attached to after grab occurs.</param>
    /// <returns>Is true if the grab is successful, false if the grab is unsuccessful.</returns>
    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
    {
        if (base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint))
        {
            this.transform.SetParent(grabbingObject.transform.parent);
            this.transform.localPosition = AttachPosition;
            this.transform.localRotation = Quaternion.Euler(AttachRotation);
            //grabbedObject.transform.parent.Find("Model").gameObject.SetActive(false);
            LocalAvatar.ShowControllers(false);     
            grabbedObjectScript.isKinematic = true;
            checkStart = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// The StopGrab method ends the grab of the current object and cleans up the state.
    /// </summary>
    /// <param name="applyGrabbingObjectVelocity">If true will apply the current velocity of the grabbing object to the grabbed object on release.</param>
    public override void StopGrab(bool applyGrabbingObjectVelocity)
    {
        //grabbedObject.transform.parent.Find("Model").gameObject.SetActive(true);
        LocalAvatar.ShowControllers(true);
        ReleaseObject(applyGrabbingObjectVelocity);
        base.StopGrab(applyGrabbingObjectVelocity);
    }

    private void Update()
    {
        if (!grabItem.IsGrabbed())
            return;
        if (checkStart)
        {
            checkStart = false;
            vectorTemp.Clear();
            for (int i = 0; i < 10; i++)
            {
                vectorTemp.Add(this.transform.position);
            }
            return;
        }
        for (int i = 9; i > 0; i--)
        {
            vectorTemp.Insert(i, vectorTemp[i - 1]);
        }
        vectorTemp.Insert(0, this.transform.position);
        distance = 0;
        for (int i = 0; i < 10; i++)
        {
            distance += Mathf.Abs(Vector3.Distance(vectorTemp[i], vectorTemp[i + 1]));
        }
        if ((int)(distance *100)> 30)
        {
            //TODO
            Debug.LogWarning("futa!");
        }
        Debug.Log((int)(distance * 100));

    }
}
