using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class GrabItem : VRTK_InteractableObject
{
    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        if (gameObject.name == "Sword")
            GameManager.Instance.IsHaveSword = true;
        else if (gameObject.name == "Shield")
            GameManager.Instance.IsHaveShield = true;
        Destroy(currentGrabbingObject.transform.parent.Find("hand").gameObject, 0.5f);
    }
}
