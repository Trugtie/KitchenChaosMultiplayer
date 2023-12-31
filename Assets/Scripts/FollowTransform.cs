using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    private Transform targetTransform;

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    private void LateUpdate()
    {
        if (this.targetTransform == null) return;
        this.transform.position = this.targetTransform.position;
        this.transform.rotation = this.targetTransform.rotation;
    }
}
