using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    public float dragMultiplier = 0.1f;
    void Start()
    {
        position = transform.position;
    }

    private void OnMouseDown() {
        position = Input.mousePosition;
    }
    private void OnMouseDrag() {
        Debug.LogError("Mouse drag" + new Vector3(position.x - Input.mousePosition.x, position.y - Input.mousePosition.y, 0));
        transform.position -= new Vector3( (position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
        position = Input.mousePosition;
    }
}
