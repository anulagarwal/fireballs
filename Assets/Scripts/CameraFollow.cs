using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    List<Ball> balls;
    
    [SerializeField]
    Transform limitTransform;

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

    private void LateUpdate()
    {
        if (transform.localPosition.y <= limitTransform.localPosition.y)
        {
            followCamera.Follow = null;
            return;
        }
        float yVal = 0;
        float ballTotal = 0;
        float totalBalls = 0;
        if (BucketController.Instance.ballsSpawned.Count != 0)
        {
            foreach (var ball in BucketController.Instance.ballsSpawned)
            {
                if (ball.destroyed)
                {
                    continue;                    
                }
               

                    ballTotal += ball.transform.position.y;
                    totalBalls++;
                    if (followCamera.Follow == null)
                    {
                        //farthestBall = ball.transform;
                        followCamera.Follow = farthestBall;
                    }
                    else if (farthestBall != null || (farthestBall.transform.position.y - ball.transform.position.y > minimumDistance))
                    {
                        //farthestBall = ball.transform;
                        followCamera.Follow = farthestBall;
                    }
                
            }            
                yVal = ballTotal / totalBalls;
            if (yVal != 0)
            {
                farthestBall.transform.position = new Vector3(farthestBall.position.x, yVal-3, farthestBall.position.z);
            }
        }
    }
}
