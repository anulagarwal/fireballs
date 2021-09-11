using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField]
    float radius = 0.3f, power = 1;
    private Vector3 offset;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    private Vector3[] originalVertices;
    Mesh planeMesh;

    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
        originalVertices = vertices;
    }
    public void DeformMesh(Vector3 positionHit) {
        positionHit = transform.InverseTransformPoint(positionHit);
        bool changed = false;
        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = (vertices[i] - positionHit).sqrMagnitude;

            if (distance < radius) {
                vertices[i] -= (Vector3.up * power); 
                changed = true;
            }
        }
        if (changed) {
            planeMesh.vertices = vertices;
            meshCollider.sharedMesh = planeMesh;
        }
    }

    private void OnDestroy() {
        planeMesh.vertices = originalVertices;
    }
}
