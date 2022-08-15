using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCompare : IComparer
{
    Transform compareTrans;

    public DistanceCompare(Transform compTrans)
    {
        compareTrans = compTrans;
    }
    public int Compare(object x, object y)
    {
        Collider xCollider = x as Collider;
        Collider yCollider = y as Collider;

        Vector3 offSet = xCollider.transform.position - compareTrans.transform.position;
        float xDistance=offSet.sqrMagnitude;

        offSet = yCollider.transform.position - compareTrans.transform.position;
        float yDistance = offSet.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}
