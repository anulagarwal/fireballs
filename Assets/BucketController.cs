using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    public float dragMultiplier = 0.1f;

    [SerializeField]
    GameObject bucket, ballContainer;
    void Start()
    {
        position = transform.position;
        bucket.AddComponent<ObservableTriggerTrigger>().OnTriggerExitAsObservable()
        .Subscribe(x => {
            if (x.CompareTag("Ball")) {
                x.transform.SetParent(ballContainer.transform);
            }           
        });
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
