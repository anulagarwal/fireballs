using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField]
    public bool activated = false;
    public bool destroyed = false;
    [SerializeField]
    Material activatedMaterial;
    private float radius = 0.5f;
    private Vector2 clipPosition;
    public bool isPipeSpawned;
    public GameObject smoke;
    private bool scaling = false;
    public void Shrink(float shrinkPercentage = 0.1f) {
        scaling = false;
        if (destroyed) {
            return;
        }
        transform.DOScale(transform.localScale.x - (transform.localScale.x * (shrinkPercentage / 100)), 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => {
            // GetComponentInChildren<ParticleSystem>().startSize = transform.localScale.x;
            if (transform.localScale.x < 0.15f)
            {
                destroyed = true;
                GameManager.Instance.ReduceRemainingBalls(1);
                Destroy(gameObject);
            }
            if (gameObject == null || destroyed || !scaling) {
                return;
            }
            scaling = true;
        });
    }
}
