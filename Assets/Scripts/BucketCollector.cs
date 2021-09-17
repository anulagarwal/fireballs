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
    List<BoxCollider> collectorCollider;

    [SerializeField]
    TextMesh ballsCollectedLabel;

    private int ballsCollected = 0;
    void Start()
    {
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
