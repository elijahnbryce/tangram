using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    public Shape.geometry silhouette;
    [SerializeField] private LayerMask layerMask;

    private Transform spawnParent;
    private Quaternion spawnRot;
    private Vector3 spawnPos, grabPos;
    private float zPos;


    private void Start()
    {
        spawnParent = transform.parent;
        spawnPos = transform.position;
        spawnRot = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        transform.parent = spawnParent;
        transform.position = spawnPos;
        transform.rotation = spawnRot;
    }

    private Vector3 Mouse2World() {
        Vector3 v = Input.mousePosition;
        v.z = zPos;
        v = Camera.main.ScreenToWorldPoint(v);
        v.y = 0;
        return v;
    }

    public void OnMouseDown()
    {
        transform.parent = null;
        zPos = Camera.main.WorldToScreenPoint(transform.position).z;
        grabPos = transform.position - Mouse2World();
    }

    public void OnMouseDrag()
    {
        transform.position = Mouse2World() + grabPos;
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(-1);
        }
    }

    public void OnMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layerMask))
        {
            transform.parent = hitData.transform;
            bool b = CheckPosition();
            Debug.Log($"{gameObject.name} in correct pos: {b}");
        }
    }

    public bool CheckPosition()
    {
        Shape comp = transform.parent.GetComponent<Shape>();
        if (silhouette == comp.silhouette)
        {
            Vector3 dist = transform.localPosition;
            dist.y = 0;
            float f = Vector3.Distance(dist, Vector3.zero);
            if (f < comp.pos_tolerance)
            {
                f = (transform.rotation.y % comp.r_equivalance);
                return (Mathf.Abs(f) < 1f);
            }
        }
        return false;
    }

    private void Rotate(int r = 1)
    {
        transform.Rotate(0, 45 * r, 0);
    }
}
