using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField]
    float radius = 0f, power = 1;
    private Vector3 offset;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    private Vector3[] originalVertices;
    Mesh planeMesh;

    public float diggingDelay = 0.25f;

    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
        originalVertices = vertices;
    }

    private void OnCollisionStay(Collision other) {
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Ball")) {
                // if (radius == 0) {
                    // radius = contact.otherCollider.GetComponent<SphereCollider>().transform.localScale.x;
                // }
                other.gameObject.GetComponent<Ball>().Shrink();
                DeformMesh(new Vector3(contact.point.x, contact.point.y, 0), contact.otherCollider.GetComponent<SphereCollider>().transform.localScale.x);
                break;
            }
        }
    }

    public void DeformMesh(Vector3 positionHit, float _radius = 0) {
        // Debug.LogError(positionHit);
        positionHit = transform.InverseTransformPoint(positionHit);
        bool changed = false;
        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = (vertices[i] - positionHit).sqrMagnitude;

            if (distance < _radius) {
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
