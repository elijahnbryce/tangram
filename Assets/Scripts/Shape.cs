using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Shape : ScriptableObject
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
    public float rot_tolerance;
    public float pos_tolerance;
}
