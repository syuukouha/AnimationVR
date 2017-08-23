using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;
using DG.Tweening;
public class ItemGrabAttach : VRTK_BaseGrabAttach
{
    public Vector3 AttachPosition;
    public Vector3 AttachRotation;
    public GrabItem grabItem;
    public GameObject Effect;
    public Transform SpawnEffectPos;
    private bool startGrab = false;
    private List<Vector3> vectorTemp = new List<Vector3>(10);
    private float distance;
    private bool isShake = false;
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
            grabbedObjectScript.isKinematic = true;
            startGrab = true;
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
        ReleaseObject(applyGrabbingObjectVelocity);
        base.StopGrab(applyGrabbingObjectVelocity);
    }

    private void Update()
    {
        if (!grabItem.IsGrabbed())
            return;
        if (startGrab)
        {
            startGrab = false;
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
        if ((int)(distance *100)> 25)
        {
            isShake = true;
        }
        if (isShake)
            ShakedController();
    }


    void ShakedController()
    {
        Vector3 target = Vector3.zero;
        isShake = false;
        GameObject effect = Instantiate(Effect);
        effect.transform.position = SpawnEffectPos.position;
        switch (grabItem.itemType)
        {
            case ItemType.Magic:
                target = GameObject.FindGameObjectWithTag("Magic").transform.position;
                break;
            case ItemType.Sword:
                target = GameObject.FindGameObjectWithTag("Sword").transform.position;

                break;
            case ItemType.Shield:
                target = GameObject.FindGameObjectWithTag("Knight").transform.position;

                break;
            default:
                break;
        }
        if (target != null)
            effect.transform.DOMove(target, 1.0f);

        grabItem.Haptic();
        if (transform.parent.name.Contains("Left"))
            GameManager.Instance.ShowHand(true, true);
        else
            GameManager.Instance.ShowHand(false, true);
        Destroy(this.gameObject);
    }
}
