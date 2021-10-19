
using UnityEngine;
using UniRx;
using TMPro;

public class BucketCollector : MonoBehaviour
{

    [SerializeField]
    TextMeshPro ballsCollectedLabel;

    private int ballsCollected = 0;

    [SerializeField] private ParticleSystem vfx;

    [SerializeField] bool isEmpty;
    [SerializeField] float checkDelay = 6;
    bool isCheckOn;
    private float checkDelayStartTime;
    void Start()
    {
        checkDelay = 6;
        if (!isEmpty)
        {
            MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x =>
        {
            ballsCollected = 0;
            ballsCollectedLabel.text = ballsCollected.ToString();
        });

            ballsCollectedLabel.text = ballsCollected + "/" + GameManager.Instance.requiredBalls;
        }
    }

    private void Update()
    {
        if(isCheckOn && checkDelayStartTime + checkDelay < Time.time)
        {
            if(GameManager.Instance.GetCurrentState() == GameState.Game)
            {
              //  GameManager.Instance.Lose();
              //  this.enabled = false;
            }
            //Check if game win or lose already called or not
            //Call lose 

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!isEmpty)
            {
                ballsCollected++;
                ballsCollectedLabel.text = ballsCollected + "/" + GameManager.Instance.requiredBalls;
                if (ballsCollected >= GameManager.Instance.requiredBalls)
                {
                    ballsCollectedLabel.color = Color.green;
                }
                vfx.Play();
                checkDelayStartTime = Time.time;

            }
            UIManager.Instance.SpawnText(other.transform.position);
            other.GetComponent<Ball>().destroyed = true;
           // Vibration.Vibrate(5);
            GameManager.Instance.AddBallToBasket(other.gameObject);
            BucketController.Instance.ballsSpawned.Remove(other.gameObject.GetComponent<Ball>());
            other.GetComponent<Ball>().smoke.SetActive(false);
            Destroy(other.gameObject);
            if (!isCheckOn) { isCheckOn = true; }
           checkDelayStartTime = Time.time;

        }
    }

}
