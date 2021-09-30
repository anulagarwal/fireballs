
using UnityEngine;
using TMPro;
using UniRx;

public class BucketController : MonoBehaviour
{
    public static BucketController Instance = null;

    Vector2 position = Vector2.zero;
    Vector3 origPos;

    float bounds = 3.5f;
    private int ballsRemaining = 0;
    [SerializeField]
    GameObject ballPrefab;

    [SerializeField]
    Transform dropPos;

    [SerializeField]
    TextMeshPro leftText;

    [SerializeField] private float pipeDownConstraint = 0f;
    [SerializeField] private float pipeUpConstraint = 0f;
    [SerializeField] private float moveSpeed = 0f;

    private float oldX;

    //public float dragMultiplier = 0.1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        position = transform.position;
        ballsRemaining = GameManager.Instance.numberOfBalls;
        GameManager.Instance.SetRemainingBalls(ballsRemaining);
        origPos = transform.position;
      /*  MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x => ResetLevel());*/
    }

    void ResetLevel() {
      //  ballsRemaining = numberofBalls;
      //  leftText.text = ballsRemaining.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ballsRemaining > 0 && GameManager.Instance.isGameOn)
        {
            SpawnBall();
        }
        if (Input.GetMouseButton(0))
        {
            if (GameManager.Instance.isGameOn && ballsRemaining > 0 && (transform.position.x <= bounds && Input.mousePosition.x > position.x) || (transform.position.x > bounds && Input.mousePosition.x < position.x) || transform.position.x >= -bounds && transform.position.x <= bounds)
            {

                float x = (Input.mousePosition.x - oldX) / 4;
                oldX = Input.mousePosition.x;
                transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime);
                // transform.position -= new Vector3((position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
                position = Input.mousePosition;
            }

            PipeDown();
        }
        else
        {
            PipeUp();
        }

        if (Input.GetMouseButtonUp(0) && ballsRemaining > 0 && GameManager.Instance.isGameOn)
        {
            GetComponentInChildren<Rigidbody>().useGravity = true;
            GetComponentInChildren<Rigidbody>().transform.parent = null;
            ballsRemaining--;
            leftText.text = ballsRemaining + "";
        }

    }

    public void SpawnBall()
    {
        GameObject ballObj = Instantiate(ballPrefab, dropPos.position, Quaternion.identity) as GameObject;
        oldX = Input.mousePosition.x;
        position = Input.mousePosition;
        ballObj.GetComponent<Rigidbody>().useGravity = false;
        ballObj.GetComponent<Ball>().isPipeSpawned = true;
        ballObj.transform.SetParent(transform);
        ballObj.gameObject.SetActive(true);
    }

    public void PipeDown()
    {
        if (transform.position.y > origPos.y+pipeDownConstraint)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * moveSpeed);
        }
    }

    public void PipeUp()
    {
        if (transform.position.y < origPos.y + pipeUpConstraint)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
    }

}
