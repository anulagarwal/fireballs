using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class BucketCollector : MonoBehaviour
{

    [SerializeField]
    TextMesh ballsCollectedLabel;

    private int ballsCollected = 0;
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
            ballsCollectedLabel.text = ballsCollected.ToString();
            Destroy(collision.gameObject);
            GameManager.Instance.AddBallToBasket(collision.gameObject);
        }
    }

}
