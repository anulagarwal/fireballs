using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    public float dragMultiplier = 0.1f;

    float bounds = 3f;

    [SerializeField]
    GameObject bucket, ballContainer;
    Animator bucketAnimator;
    void Start()
    {
        position = transform.position;
        bucketAnimator = bucket.GetComponent<Animator>();
        bucket.AddComponent<ObservableTriggerTrigger>().OnTriggerExitAsObservable()
        .Subscribe(x => {
            if (x.CompareTag("Ball")) {
                if (ballContainer.transform.childCount == 0) {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                x.transform.SetParent(ballContainer.transform);
            }
        });
    }

    private void OnMouseDown() {
        position = Input.mousePosition;
        bucketAnimator.enabled = true;
        bucketAnimator.Play("RotateBucket");
    }
    private void OnMouseDrag() {

        if ( (transform.position.x <= bounds && Input.mousePosition.x > position.x) || (transform.position.x > bounds && Input.mousePosition.x < position.x) || transform.position.x >= -bounds && transform.position.x <= 3) {
            transform.position -= new Vector3( (position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
            position = Input.mousePosition;
        }

    }
}
