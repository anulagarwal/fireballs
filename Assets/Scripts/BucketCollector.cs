
using UnityEngine;
using UniRx;
using TMPro;

public class BucketCollector : MonoBehaviour
{

    [SerializeField]
    TextMeshPro ballsCollectedLabel;

    private int ballsCollected = 0;

    [SerializeField] private ParticleSystem vfx; 
    void Start()
    {
        MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x => {
            ballsCollected = 0;
            ballsCollectedLabel.text = ballsCollected.ToString(); 
        });

        ballsCollectedLabel.text = ballsCollected.ToString() + "/" + GameManager.Instance.requiredBalls;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballsCollected++;
            ballsCollectedLabel.text = ballsCollected.ToString() + "/" + GameManager.Instance.requiredBalls;
            if (ballsCollected >= GameManager.Instance.requiredBalls)
            {
                ballsCollectedLabel.color = Color.green;
            }
            Destroy(other.gameObject, 1f);
            GameManager.Instance.AddBallToBasket(other.gameObject);
            other.GetComponent<Ball>().smoke.SetActive(false);
            // Destroy(collision.gameObject);
            vfx.Play();
        }
    }

}
