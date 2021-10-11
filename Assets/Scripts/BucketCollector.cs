
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
    void Start()
    {
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
            }
            other.GetComponent<Ball>().destroyed = true;
            Destroy(other.gameObject, 1f);
           // Vibration.Vibrate(5);

            GameManager.Instance.AddBallToBasket(other.gameObject);
            other.GetComponent<Ball>().smoke.SetActive(false);
            // Destroy(collision.gameObject);
            
        }
    }

}
