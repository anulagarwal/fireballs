
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
    }


        // foreach (var collector in collectorCollider)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ballsCollected++;
            ballsCollectedLabel.text = ballsCollected.ToString() + "/" + GameManager.Instance.requiredBalls;
            if (ballsCollected >= GameManager.Instance.requiredBalls)
            {
                ballsCollectedLabel.color = Color.green;
            }
            GameManager.Instance.AddBallToBasket(collision.gameObject);
            Destroy(collision.gameObject);
            vfx.Play();
        }
    }

}
