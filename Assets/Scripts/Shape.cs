using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public enum geometry
    {
        Triangle_S,
        Triangle_M,
        Triangle_L,
        Square,
        Parrallelogram,
    }

    public geometry silhouette;
    public float r_equivalance;
    public float pos_tolerance;

    public bool CheckMatch()
    {
        Transform child = transform.GetChild(0);
        geometry comp = child.GetComponent<Piece>().silhouette;
        if (silhouette == comp)
        {
            Vector3 dist = child.localPosition;
            dist.y = 0;
            if (Vector3.Distance(dist, Vector3.zero) < pos_tolerance)
            {
                return (Mathf.Abs(child.rotation.y % r_equivalance) < 1f);
            }
        }
        return false;
    }
}
