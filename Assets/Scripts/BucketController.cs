
using UnityEngine;
using TMPro;
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

    [SerializeField]
    TextMeshPro leftText;
        
    
    void Start()
    {
        position = transform.position;      
        ballsRemaining = numberofBalls;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            position = Input.mousePosition;
            GameObject g = Instantiate(ballPrefab, dropPos.position, Quaternion.identity);
            g.GetComponent<Rigidbody>().useGravity = false;
            g.transform.SetParent(transform);

        }
        if (Input.GetMouseButton(0))
        {
            if (ballsRemaining > 0 && (transform.position.x <= bounds && Input.mousePosition.x > position.x) || (transform.position.x > bounds && Input.mousePosition.x < position.x) || transform.position.x >= -bounds && transform.position.x <= 3)
            {
                transform.position -= new Vector3((position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
                position = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            GetComponentInChildren<Rigidbody>().useGravity = true;
            GetComponentInChildren<Rigidbody>().transform.parent = null;
            if (ballsRemaining > 0)
            {
                leftText.text = ballsRemaining + "";
                ballsRemaining--;
            }

        }
    }
       
}
