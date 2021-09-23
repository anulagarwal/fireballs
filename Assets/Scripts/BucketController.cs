using System;
using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    public float dragMultiplier = 0.1f;

    float bounds = 3f;

    [SerializeField]
    GameObject bucket, ballContainer, bucketSource;
    public int numberofBalls = 5;
    private int ballsRemaining = 0;
    [SerializeField]
    GameObject ballPrefab;
    Animator bucketAnimator;
    void Start()
    {
        position = transform.position;
        bucketAnimator = bucket.GetComponent<Animator>();
        bucket.AddComponent<ObservableTrigger2DTrigger>().OnTriggerExit2DAsObservable()
        .Subscribe(x => {
            if (x.CompareTag("Ball")) {
                if (ballContainer.transform.childCount == 0) {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                x.transform.SetParent(ballContainer.transform);
            }
        });
        ballsRemaining = numberofBalls;

        bucket.AddComponent<ObservableTriggerTrigger>().OnTriggerExitAsObservable()
        .Subscribe(x => {
            if (x.CompareTag("Ball")) {
                x.transform.SetParent(ballContainer.transform);
            }
        });
    }

    async private void OnMouseDown() {
        position = Input.mousePosition;
        if (ballsRemaining <= 0) {
            return;
        }
        // bucketAnimator.enabled = true;
        // bucketAnimator.Play("RotateBucket");

        for (int i = 0; i < numberofBalls; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.transform.SetParent(bucketSource.transform);
            ball.transform.localPosition = new Vector3(-0.16f, 0.2f, 0);
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
            ballsRemaining --;
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnMouseDrag() {
        if ( ballsRemaining > 0 && (transform.position.x <= bounds && Input.mousePosition.x > position.x) || (transform.position.x > bounds && Input.mousePosition.x < position.x) || transform.position.x >= -bounds && transform.position.x <= 3) {
            transform.position -= new Vector3( (position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
            position = Input.mousePosition;
        }
    }
}
