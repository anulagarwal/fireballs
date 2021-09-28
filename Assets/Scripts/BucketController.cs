
using UnityEngine;
using TMPro;
using UniRx;

public class BucketController : MonoBehaviour
{
    Vector2 position = Vector2.zero;
    public float dragMultiplier = 0.1f;

    float bounds = 3f;

    public int numberofBalls = 5;
    private int ballsRemaining = 0;

    public float delay;
    [SerializeField]
    Ball ballPrefab;

    [SerializeField]
    Transform dropPos;

    [SerializeField]
    TextMeshPro leftText;

    Rigidbody currentBody;

    void Start()
    {
        numberofBalls = GameManager.Instance.numberOfBalls;
        position = transform.position;      
        ballsRemaining = numberofBalls;
        GameManager.Instance.SetRemainingBalls(ballsRemaining);

        MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x => ResetLevel());
    }

    void ResetLevel() {
        ballsRemaining = numberofBalls;
        leftText.text = ballsRemaining.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOn)
        {
            if (Input.GetMouseButtonDown(0) && ballsRemaining > 0)
            {
                Ball ballObj = ObjectPool.Instance.GetPooledObject();

                position = Input.mousePosition;
                if (ballObj == null)
                {
                    ballObj = Instantiate<Ball>(ballPrefab, dropPos.position, Quaternion.identity);
                }
                else
                {
                    ballObj.transform.position = dropPos.position;
                }
                ballObj.GetComponent<Rigidbody>().useGravity = false;
                ballObj.transform.SetParent(transform);
                ballObj.gameObject.SetActive(true);
                currentBody = ballObj.GetComponent<Rigidbody>();
            }
            if (Input.GetMouseButton(0))
            {
                if (ballsRemaining > 0 && (transform.position.x <= bounds && Input.mousePosition.x > position.x) || (transform.position.x > bounds && Input.mousePosition.x < position.x) || transform.position.x >= -bounds && transform.position.x <= 3)
                {
                    transform.position -= new Vector3((position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
                    position = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0) && ballsRemaining > 0)
            {
                if (currentBody != null) {
                    currentBody.useGravity = true;
                    currentBody.transform.parent = null;
                    currentBody = null;
                    ballsRemaining--;
                    leftText.text = ballsRemaining + "";
                }
            }
        }
    }
       
}
