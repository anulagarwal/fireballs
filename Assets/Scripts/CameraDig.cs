using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDig : MonoBehaviour
{
    [SerializeField]
    GameObject ringPrefab;
    Camera cam;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.GetComponent<Digger>() != null) {
                    hit.collider.GetComponent<Digger>().DeformMesh(hit.point);
                }
            }
        }
    }
}
