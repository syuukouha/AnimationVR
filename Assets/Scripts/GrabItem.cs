using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class GrabItem : VRTK_InteractableObject
{
    public VRTK_ControllerActions swordControllerActions;
    public VRTK_ControllerActions shieldControllerActions;

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        if (gameObject.name == "Sword")
        {
            GameManager.Instance.IsHaveSword = true;
            swordControllerActions = currentGrabbingObject.GetComponent<VRTK_ControllerActions>();
        }
        else if (gameObject.name == "Shield")
        {
            GameManager.Instance.IsHaveShield = true;
            shieldControllerActions = currentGrabbingObject.GetComponent<VRTK_ControllerActions>();
        }
        Destroy(currentGrabbingObject.transform.parent.Find("hand").gameObject, 0.5f);
    }
}
