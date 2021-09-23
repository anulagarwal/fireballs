using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMelt : MonoBehaviour
{
    [SerializeField]
    float power = 1;
    private Vector3 offset;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    private Vector3[] originalVertices, displacedVertices;
    Mesh planeMesh;

    Vector3[] vertexVelocities;

    public float diggingDelay = 0.25f;

    enum SURFACE_TYPE
    {
        PAPER,
        WOODEN
    }
    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
        originalVertices = vertices;
        displacedVertices = new Vector3[originalVertices.Length];

        for (int i = 0; i < displacedVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        vertexVelocities = new Vector3[originalVertices.Length];
    }

    private void OnCollisionStay(Collision other) {
        foreach (ContactPoint contact in other.contacts)
        {
            if (contact.otherCollider.gameObject.CompareTag("Ball")) {
                Debug.LogError("other");
                // other.gameObject.GetComponent<Ball>().Shrink();
                Vector3 contactPoint = contact.point;
                contactPoint += contact.normal * -1.5f;
                DeformMesh(contactPoint, contact.otherCollider.GetComponent<SphereCollider>().transform.localScale.x);
                // break;
            }
        }
    }

    public void DeformMesh(Vector3 positionHit, float _radius = 0) {
        // Debug.LogError(positionHit);
        positionHit = transform.InverseTransformPoint(positionHit);
        bool changed = false;
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            // float distance = (vertices[i] - positionHit).sqrMagnitude;

            // if (distance < _radius) {
                // vertices[i] -= (Vector3.up * power); 
                Vector3 pointToVertext = vertices[i] - positionHit;
                float attenuationForce = 1 / (1f + pointToVertext.sqrMagnitude);
                float velocity = attenuationForce * Time.deltaTime;
                vertexVelocities[i] += pointToVertext.normalized * velocity;
                changed = true;
            // }
        }
        // if (changed) {
        //     planeMesh.vertices = vertices;
        //     meshCollider.sharedMesh = planeMesh;
        // }
    }

    private void Update() {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            Vector3 velocity = vertexVelocities[i];
            displacedVertices[i] += velocity * Time.deltaTime;
        }
        planeMesh.vertices = displacedVertices;
        meshCollider.sharedMesh = planeMesh;
    }

    private void OnDestroy() {
        planeMesh.vertices = originalVertices;
    }
}
