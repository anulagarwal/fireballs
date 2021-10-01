using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField]
    float power = 1;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    private Vector3[] originalVertices;
    Mesh planeMesh;
    public float digRadius;

    [SerializeField]
    Vector3 digVector;
    public enum SURFACE_TYPE
    {
        PAPER,
        WOODEN,
        WAX
    }

    private float shrinkPercentage = 0; 
    public SURFACE_TYPE currentSurface;
    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
        originalVertices = vertices;
        shrinkPercentage = GetShrinkPercentage();
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private float GetShrinkPercentage() {
        float shrinkAmount = 10f;
        switch (currentSurface)
        {
            case SURFACE_TYPE.PAPER:
            shrinkAmount = 15f;
            break;
            case SURFACE_TYPE.WAX:
            shrinkAmount = 10f;
            break;
            case SURFACE_TYPE.WOODEN:
            shrinkAmount = 25f;
            break;
            default:
            break;
        }
        return shrinkAmount;
    }
    private void OnCollisionStay(Collision other) {
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Ball") ) {
                other.gameObject.GetComponent<Ball>().Shrink(shrinkPercentage);
                DeformMesh(new Vector3(contact.point.x, contact.point.y, 0), Mathf.Max(other.transform.localScale.x, 0.65f));
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
                vertices[i] -= (digVector * power); 
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
