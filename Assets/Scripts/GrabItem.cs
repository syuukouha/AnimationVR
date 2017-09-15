using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using DG.Tweening;


public enum ItemType
{
    Magic,
    Sword,
    Shield,
}
public class GrabItem : VRTK_InteractableObject
{
    public ItemType itemType;
    public Material FadeMaterial;
    private float impactMagnifier = 120f;
    private float collisionForce = 0f;
    private float maxCollisionForce = 4000f;
    private VRTK_ControllerReference controllerReference;
    private Vector3 dropPosition;
    private Quaternion dropRotation;
    //private bool canUse;
    //public bool Reset;

    //public bool CanUse
    //{
    //    get
    //    {
    //        return canUse;
    //    }
    //}

    private void Start()
    {
        dropPosition = this.transform.position;
        dropRotation = this.transform.rotation;
    }
    public float CollisionForce()
    {
        return collisionForce;
    }
    public override void Grabbed(VRTK_InteractGrab grabbingObject)
    {
        base.Grabbed(grabbingObject);
        //canUse = true;
        controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
        if (grabbingObject.name.Contains("Left"))
            GameManager.Instance.ShowHand(true, false);
        else
            GameManager.Instance.ShowHand(false, false);
        transform.Find("Wave_02").gameObject.SetActive(false);
        switch (itemType)
        {
            case ItemType.Magic:
                break;
            case ItemType.Sword:
                GameManager.Instance.IsSwordGrabbed = true;
                break;
            case ItemType.Shield:
                GameManager.Instance.IsShieldGrabbed = true;
                break;
            default:
                break;
        }
    }
    public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
    {
        base.Ungrabbed(previousGrabbingObject);
        controllerReference = null;
        //if (previousGrabbingObject.name.Contains("Left"))
        //    GameManager.Instance.ShowHand(true, true);
        //else
        //    GameManager.Instance.ShowHand(false, true);
        transform.DOMove(dropPosition, 0.3f);
        transform.DORotateQuaternion(dropRotation, 0.3f);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        controllerReference = null;
        interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
        {
            collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
            var hapticStrength = collisionForce / maxCollisionForce;
            VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.5f, 0.01f);
        }
        else
        {
            collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
        }
    }
    public void Haptic()
    {
        VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, 1.0f, 0.2f, 0.01f);
    }

}
