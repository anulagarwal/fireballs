using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public bool activated = false;

    [SerializeField]
    Material activatedMaterial;

    private MeshRenderer meshRenderer; 

    private Rigidbody rigidbody;
    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Shrink() {
        transform.localScale -= transform.localScale * 0.1f;
        if (transform.localScale.x <= 0) {
            Destroy(gameObject);
        }

        // Vector3.Lerp(transform.localScale, transform.localScale - transform.localScale * 0.5f, Time.deltaTime * 10);
    }

    private void OnCollisionEnter(Collision other) {
        if (!activated && (other.gameObject.CompareTag("Ball")) ) {
            if (other.gameObject.GetComponent<Ball>().activated) {
                activated = true;
                meshRenderer.material = activatedMaterial;
            }
        } else if (other.gameObject.CompareTag("Head") && activated){
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            other.gameObject.GetComponent<MeshRenderer>().material = activatedMaterial;
        }
    }
}
