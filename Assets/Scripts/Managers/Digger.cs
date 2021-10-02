using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField]
    float power = 1;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    private NativeArray<Vector3> m_Vertices;
    private Vector3[] modifiedVertices;

    private Vector3[] originalVertices;
    Mesh planeMesh;
    public float digRadius;

    MeltJob meltJob;

    JobHandle m_JobHandle;

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
    private NativeArray<Vector3> mod;
    private bool scheduled = false;
    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
        originalVertices = vertices;
        shrinkPercentage = GetShrinkPercentage();
        m_Vertices = new NativeArray<Vector3>(planeMesh.vertices, Allocator.Persistent);
        modifiedVertices = new Vector3[m_Vertices.Length];
        planeMesh.MarkDynamic();
        mod = new NativeArray<Vector3>(planeMesh.vertices, Allocator.Persistent);
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
            if (contact.otherCollider.CompareTag("Ball") ) {
                other.gameObject.GetComponent<Ball>().Shrink(shrinkPercentage);
                DeformMesh(new Vector3(contact.point.x, contact.point.y, 0), Mathf.Max(other.transform.localScale.x, 0.65f));
                break;
            }
        }
    }

    public void DeformMesh(Vector3 positionHit, float _radius = 0) {
        positionHit = transform.InverseTransformPoint(positionHit);
        meltJob = new MeltJob() {
            vertices = mod,
            radius = _radius,
            power = power,
            digVector = digVector,
            positionHit = positionHit
        };
        m_JobHandle = meltJob.Schedule(m_Vertices.Length, 64, m_JobHandle);
        scheduled = true;
    }

    private void LateUpdate() {
        if (!scheduled) {
            return;
        }
        m_JobHandle.Complete();
        scheduled = false;
        meltJob.vertices.CopyTo(mod);
        planeMesh.vertices = mod.ToArray();        
        meshCollider.sharedMesh = planeMesh;
    }
 
    private void OnDestroy() {
        planeMesh.vertices = originalVertices;
        m_Vertices.Dispose();
        mod.Dispose();
    }
}
