using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    [Header("Shape")]
    public Shape.geometry silhouette;
    [SerializeField] private LayerMask layerMask;

    private Transform spawnParent;
    private Quaternion spawnRot;
    private Vector3 spawnPos, grabPos;
    private float zPos;
    public bool inPlace = false;

    private GameManager gm;

    private void Start()
    {
        gm = GameManager._Instance;

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
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(transform.position));
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layerMask))
        {
            transform.parent = hitData.transform;
            gm.CheckFinish();
        }
        else Respawn();
    }


    private void Rotate(int r = 1)
    {
        transform.Rotate(0, 45 * r, 0);
    }
}
