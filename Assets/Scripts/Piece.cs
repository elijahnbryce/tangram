using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    [SerializeField] private Shape shape;
    [SerializeField] private LayerMask layerMask;

    private Vector3 grabPos;
    private float zPos;

    private Vector3 Mouse2World() {
        Vector3 v = Input.mousePosition;
        v.z = zPos;
        v = Camera.main.ScreenToWorldPoint(v);
        v.y = 0;
        return v;
    }

    public void OnMouseDown()
    {
        Debug.Log($"grabbed {gameObject.name}");
        zPos = Camera.main.WorldToScreenPoint(transform.position).z;
        grabPos = transform.position - Mouse2World();
    }

    public void OnMouseDrag()
    {
        Debug.Log($"dragging {gameObject.name}");
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
            Debug.Log($"hit {hitData.collider.gameObject.name}");
        }
        else
        {
            Debug.Log("nothing hit");
        }
    }

    private void Rotate(int r = 1)
    {
        transform.Rotate(0, 45 * r, 0);
    }
}
