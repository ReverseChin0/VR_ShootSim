using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Pointer : MonoBehaviour
{
    public float distancia = 5 - .0f;
    public GameObject punto;
    public VRControles controles;

    LineRenderer line = null;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLinea();
    }

    void UpdateLinea()
    {
        PointerEventData data = controles.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? distancia : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(targetLength);

        Vector3 endPos = transform.position + (transform.forward * targetLength);

        if(hit.collider != null)
        {
            endPos = hit.point;
        }

        punto.transform.position = endPos;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);
    }

    RaycastHit CreateRaycast(float _targetLength)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, distancia);
        return hit;
    }
}

