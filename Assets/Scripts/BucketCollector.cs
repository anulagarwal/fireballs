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
    List<BoxCollider2D> collectorCollider2D;

    [SerializeField]
    List<BoxCollider> collectorCollider;

    [SerializeField]
    TextMesh ballsCollectedLabel;

    private int ballsCollected = 0;
    void Start()
    {
        foreach (var collector in collectorCollider2D)
        {
            collector.gameObject.AddComponent<ObservableCollisionTrigger>().OnCollisionEnter2DAsObservable()
            .Subscribe(x => {
                if (x.gameObject.CompareTag("Ball")) {
                    ballsCollected++;
                    ballsCollectedLabel.text = ballsCollected.ToString(); 
                    Destroy(x.gameObject);
                }
            });
        }

        MessageBroker.Default.Receive<GamePlayMessage>()
        .Where(x => x.commandType == GamePlayMessage.COMMAND.RESTART || x.commandType == GamePlayMessage.COMMAND.PLAYING)
        .Subscribe(x => {
            ballsCollected = 0;
            ballsCollectedLabel.text = ballsCollected.ToString(); 
        });

        foreach (var collector in collectorCollider)
        {
            collector.gameObject.AddComponent<ObservableCollisionTrigger>().OnCollisionEnterAsObservable()
            .Subscribe(x => {
                if (x.gameObject.CompareTag("Ball")) {
                    ballsCollected++;
                    ballsCollectedLabel.text = ballsCollected.ToString(); 
                    Destroy(x.gameObject);
                }
            });
        }
    }

}
