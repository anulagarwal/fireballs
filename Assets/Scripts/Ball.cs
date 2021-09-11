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
        if (!activated) {
            // rigidbody.useGravity = false;
            // rigidbody.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!activated && (other.gameObject.CompareTag("Ball")) ) {
            if (other.gameObject.GetComponent<Ball>().activated) {
                activated = true;
                meshRenderer.material = activatedMaterial;
                // rigidbody.useGravity = true;
                // rigidbody.isKinematic = false;
            }
        } else if (other.gameObject.CompareTag("Head") && activated){
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            other.gameObject.GetComponent<MeshRenderer>().material = activatedMaterial;
        }
    }
}
