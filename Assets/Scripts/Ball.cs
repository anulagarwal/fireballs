using DG.Tweening;
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
        transform.DOScale(transform.localScale.x - (transform.localScale.x * 0.1f), 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => {
            if (transform.localScale.x <= 0) {
                Destroy(gameObject);
            }
        });
        // transform.localScale -= transform.localScale * 0.1f;

        // Vector3.Lerp(transform.localScale, transform.localScale - transform.localScale * 0.5f, Time.deltaTime * 10);
    }

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
