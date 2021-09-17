using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

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

    private Tween scaleUpTween;
    [SerializeField]
    float value;

    [SerializeField]
    public MODIFIER_TYPE currentModifier = MODIFIER_TYPE.ADDER;

    List<Ball> spawnedBalls;
    private void Start() {
        spawnedBalls = new List<Ball>();
        currentValLbl.text = currentModifier == MODIFIER_TYPE.ADDER ? "+" + value : "*" + value;
        DOTween.defaultAutoKill = false;
    }

    async private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Ball") && !spawnedBalls.Contains(other.GetComponent<Ball>()) && value > 0) {
            if (scaleUpTween == null || (scaleUpTween != null && !scaleUpTween.IsPlaying())) {
                scaleUpTween = currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x + 0.2f, 0.15f).SetEase(Ease.InOutCubic).OnComplete(() => {
                    currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x - 0.2f, 0.15f).SetEase(Ease.InOutCubic);
                });
            }
            if (currentModifier == MODIFIER_TYPE.ADDER) {
                value -= 1;

                if (value <= 0) {
                    Destroy(currentValLbl.gameObject);
                }
                currentValLbl.text = "+" + value;
                await Task.Delay(TimeSpan.FromSeconds(0.25f));
                SpawnBall(other);
            } else {
                for (int i = 0; i < value; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.25f));
                    SpawnBall(other);
                }
            }
        }
    }

    void SpawnBall(Collider colliderPos) {
        if (colliderPos != null) {
            Ball ballObj = ObjectPool.Instance.GetPooledObject(); 

            colliderPos.GetComponent<Ball>().activated = true;
            if (ballObj == null) {
                ballObj = Instantiate<Ball>(ballPrefab, colliderPos.transform.position, Quaternion.identity);
            } else {
                ballObj.transform.position = colliderPos.transform.position;
            }
            spawnedBalls.Add(ballObj);
            ballObj.transform.SetParent(ballContainer.transform);
            ballObj.gameObject.SetActive(true);
        }
    }
}
