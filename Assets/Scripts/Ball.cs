using System.Collections.Generic;
using ClipperLib;
using DG.Tweening;
using UnityEngine;
using Vector2i = ClipperLib.IntPoint;

public class Ball : MonoBehaviour, IClip
{
    public DestructibleTerrain terrain;

    [SerializeField]
    public bool activated = false;

    [SerializeField]
    Material activatedMaterial;

    private MeshRenderer meshRenderer; 

    private float radius = 0.5f;
    
    private Vector2 clipPosition {
        get {
            return (Vector2) transform.position - terrain.GetPositionOffset();
        }
    }

    private int segmentCount = 20;
    private Rigidbody rigidbody;
    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        
        Vector2 positionWorldSpace = transform.position;
        // clipPosition = positionWorldSpace - terrain.GetPositionOffset();
    }

    public void Shrink() {
        transform.DOScale(transform.localScale.x - (transform.localScale.x * 0.1f), 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => {
            if (transform.localScale.x < 0.1) {
                Destroy(gameObject);
            }
        });
        // transform.localScale -= transform.localScale * 0.1f;

        Vector3.Lerp(transform.localScale, transform.localScale - transform.localScale * 0.5f, Time.deltaTime * 10);
    }

    public ClipBounds GetBounds()
    {
        return new ClipBounds{
            lowerPoint = new Vector2(clipPosition.x - radius, clipPosition.y - radius),
            upperPoint = new Vector2(clipPosition.x + radius, clipPosition.y + radius)
        };
    }

    public List<Vector2i> GetVertices()
    {
        List<Vector2i> vertices = new List<Vector2i>();
        for (int i = 0; i < segmentCount; i++)
        {
            float angle = Mathf.Deg2Rad * (-90f - 360f / segmentCount * i);

            Vector2 point = new Vector2(clipPosition.x + radius * Mathf.Cos(angle), clipPosition.y + radius * Mathf.Sin(angle));
            Vector2i point_i64 = point.ToVector2i();
            vertices.Add(point_i64);
        }
        return vertices;
    }

    public bool CheckBlockOverlapping(Vector2 p, float size)
    {       
        float dx = Mathf.Abs(clipPosition.x - p.x) - radius - size / 2;
        float dy = Mathf.Abs(clipPosition.y - p.y) - radius - size / 2;

        return dx < 0f && dy < 0f;      
    }

    // private void OnCollisionStay2D(Collision2D other) {
    //     Debug.LogError(other.gameObject.tag);
    //    if (other.gameObject.CompareTag("Respawn")) {
    //         Debug.LogError("Respawn");
    //         terrain.ExecuteClip(this);
    //     } 
    // }

    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.CompareTag("Respawn")) {
    //         Debug.LogError("Respawn");
    //         terrain.ExecuteClip(this);
    //     }
    // }

    // private void OnCollisionEnter(Collision other) {
    //     if (!activated && (other.gameObject.CompareTag("Ball")) ) {
    //         if (other.gameObject.GetComponent<Ball>().activated) {
    //             // activated = true;
    //             meshRenderer.material = activatedMaterial;
    //         }
    //     } else if (other.gameObject.CompareTag("Head") && activated){
    //         rigidbody.useGravity = false;
    //         rigidbody.isKinematic = true;
    //         other.gameObject.GetComponent<MeshRenderer>().material = activatedMaterial;
    //     }
    // }
}
