
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

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

    public List<Ball> ballsSpawned;

    [SerializeField] private float pipeDownConstraint = 0f;
    [SerializeField] private float pipeUpConstraint = 0f;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float launchSpeed= 0.4f;

    [SerializeField] Image borderFill;
    [SerializeField] float fallDelay;

    private float startTime;
    private bool isFalling;

    private float oldX;
    bool isGameOn;
    bool isPipeUp;
    bool isPipeDown;

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
        fallDelay = 2;
        ballsSpawned = new List<Ball>();
        position = transform.position;
        ballsRemaining = GameManager.Instance.numberOfBalls;
        GameManager.Instance.SetRemainingBalls(ballsRemaining);
        origPos = transform.position;
        leftText.text = ballsRemaining + "";

        /*  MessageBroker.Default.Receive<GamePlayMessage>()
          .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
          .Subscribe(x => ResetLevel());*/
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ballsRemaining > 0 && GameManager.Instance.isGameOn)
        {

            // SpawnBall();
            oldX = Input.mousePosition.x;
            startTime = Time.time;
            if(!isFalling)
            isFalling = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (!isGameOn)
            {
                oldX = Input.mousePosition.x;
                isGameOn = true;             
                //Invoke("LaunchBalls", launchSpeed);
                SpawnBall();
                isFalling = true;
                startTime = Time.time;
               // Invoke("PipeDown", 2f);                
            }

            if (isFalling)
            {
                if(startTime + fallDelay <= Time.time)
                {
                    PipeDown();
                    isFalling = false;
                    borderFill.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    borderFill.fillAmount = (Time.time - startTime) / fallDelay;
                }
            }
            if (GameManager.Instance.isGameOn && ballsRemaining > 0 && (transform.position.x <= bounds && Input.mousePosition.x > oldX) || (transform.position.x > bounds && Input.mousePosition.x < oldX) || transform.position.x >= -bounds && transform.position.x <= bounds)
            {

                float x = (Input.mousePosition.x - oldX) / 2;
                transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime);
                // transform.position -= new Vector3((position.x - Input.mousePosition.x) * dragMultiplier, 0, 0);
                position = Input.mousePosition;
                oldX = Input.mousePosition.x;
            }

            //PipeDown();
        }
        else
        {
           // PipeUp();
        }

        if (Input.GetMouseButtonUp(0) && ballsRemaining > 0 && GameManager.Instance.isGameOn)
        {
            if (isFalling)
            {
                borderFill.fillAmount = 0;
            }
            //ReleaseBall();
            //if (startTime + fallDelay >= Time.time && isFalling) ;
            // isFalling = false;

        }

       
        if (ballsRemaining > 0)
        {
            if (isGameOn && !isFalling)
            {
               /* if (startTime + fallDelay <= Time.time)
                {
                    startTime = Time.time;
                    PipeDown();
                    isFalling = false;
                }
               */
                
            }
            if (isPipeDown && !isPipeUp)
            {
                PipeDown();
            }

            if (isPipeUp && !isPipeDown)
            {
                PipeUp();
            }
        }

    }
    public void LaunchBalls()
    {
        
      
        if (ballsRemaining > 0)
        {
            SpawnBall();
            ReleaseBall();
            Invoke("LaunchBalls", launchSpeed);
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
        ballsSpawned.Add(ballObj.GetComponent<Ball>());
        
    }

    public void ReleaseBall()
    {
        GetComponentInChildren<Rigidbody>().useGravity = true;
        GetComponentInChildren<Rigidbody>().transform.parent = null;
        ballsRemaining--;
        leftText.text = ballsRemaining + "";
    }

    public void PipeDown()
    {
        if (transform.position.y > origPos.y+pipeDownConstraint)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * moveSpeed);
            isPipeDown = true;
            isPipeUp = false;
        }
        else
        {
            //Drop ball
           
            isPipeDown = false;
            isPipeUp = true;
            ReleaseBall();
            PipeUp();
        }
    }

    public void PipeUp()
    {
        if (transform.position.y < origPos.y + pipeUpConstraint)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            isPipeUp = true;
            isPipeDown = false;

        }
        else
        {
            isPipeUp = false;
            isPipeDown = true;
            SpawnBall();
        }
    }
}
