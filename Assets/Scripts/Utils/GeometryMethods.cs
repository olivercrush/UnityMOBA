using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryMethods {

	public static bool IsWithinRange(Transform targetTransform, Transform actorTransform, float range)
    {
        float distance = Vector3.Distance(targetTransform.position, actorTransform.position);
        if (distance <= range) return true;

        return false;
    }
}
