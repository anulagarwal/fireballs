using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform ballsContainer;

    List<Transform> balls;


    [SerializeField]
    private Transform farthestBall;

    CinemachineVirtualCamera followCamera;

    [SerializeField]
    float minimumDistance = 2;
    void Start()
    {
        followCamera = GetComponent<CinemachineVirtualCamera>();
        balls = new List<Transform>();

        for (int i = 0; i< ballsContainer.childCount; i++)
        {
            balls.Add(ballsContainer.GetChild(i));
        }
        farthestBall = balls[0];
        followCamera.Follow = farthestBall;
    }

    private void Update() {
        foreach (var ball in balls)
        {
            if (farthestBall.transform.position.y - ball.transform.position.y > minimumDistance) {
                farthestBall = ball;
                followCamera.Follow = farthestBall;
            }
        }
    }
}
