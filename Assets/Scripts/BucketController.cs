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

    public int numberofBalls = 5;
    private int ballsRemaining = 0;

    public float delay;
    [SerializeField]
    GameObject ballPrefab;

    [SerializeField]
    Transform dropPos;
        
    
    void Start()
    {
        position = transform.position;      
        ballsRemaining = numberofBalls;
    }

    async private void OnMouseDown() {
        position = Input.mousePosition;
        if (ballsRemaining <= 0) {
            return;
        }

        for (int i = 0; i < numberofBalls; i++)
        {
            Instantiate(ballPrefab, dropPos.position, Quaternion.identity);           
            await Task.Delay(TimeSpan.FromSeconds(delay));
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
