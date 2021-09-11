using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallModifier : MonoBehaviour
{
    [SerializeField]
    TextMesh currentValLbl;

    [SerializeField]
    GameObject ballContainer;

    [SerializeField]
    Ball ballPrefab;
    public enum MODIFIER_TYPE
    {
        ADDER,
        MULTIPLIER,
        SUBTRACTOR,
        DIVIDER
    }

    [SerializeField]
    float value;

    [SerializeField]
    public MODIFIER_TYPE currentModifier = MODIFIER_TYPE.ADDER;
    
    private void Start() {
        currentValLbl.text = currentModifier == MODIFIER_TYPE.ADDER ? "+" + value : "*" + value;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Ball") && !other.GetComponent<Ball>().activated && value > 0) {
            if (currentModifier == MODIFIER_TYPE.ADDER) {
                value -= 1;
                currentValLbl.text = "+" + value;
                SpawnBall(other);
            } else {
                for (int i = 0; i < value; i++)
                {
                    SpawnBall(other);                    
                }
            }

        }
    }

    void SpawnBall(Collider colliderPos) {
        Ball ball = Instantiate<Ball>(ballPrefab, colliderPos.transform.position, Quaternion.identity);
        colliderPos.GetComponent<Ball>().activated = true;
        ball.activated = true;
        ball.transform.SetParent(ballContainer.transform);

    }
}
