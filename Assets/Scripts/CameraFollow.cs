using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    List<Ball> balls;


    [SerializeField]
    private Transform farthestBall;

    CinemachineVirtualCamera followCamera;

    [SerializeField]
    float minimumDistance = 2;
    void Start()
    {
        followCamera = GetComponent<CinemachineVirtualCamera>();
        followCamera.Follow = farthestBall;
    }

    private void Update() {
        foreach (var ball in BucketController.Instance.ballsSpawned)
        {
            if (ball.destroyed) {
                continue;
            }
            if (farthestBall != null || (farthestBall.transform.position.y - ball.transform.position.y > minimumDistance)) {
                farthestBall = ball.transform;
                followCamera.Follow = farthestBall;
            }
        }
    }
}
