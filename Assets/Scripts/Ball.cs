using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;


public class Ball : MonoBehaviour
{
   
    [SerializeField]
    public bool activated = false;

    private bool destroyed = false;
    [SerializeField]
    Material activatedMaterial;

    private float radius = 0.5f;
    private Vector2 clipPosition;
    public bool isPipeSpawned;

    private void Start() {
    
        Vector2 positionWorldSpace = transform.position;
        // clipPosition = positionWorldSpace - terrain.GetPositionOffset();
    }

    public void Shrink(float shrinkPercentage = 0.1f) {
        transform.DOScale(transform.localScale.x - (transform.localScale.x * (shrinkPercentage / 100)), 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => {
            if (gameObject == null || destroyed) {
                return;
            }
            GetComponentInChildren<ParticleSystem>().startSize = transform.localScale.x;
            if (transform.localScale.x < 0.1f) {
                destroyed = true;

                Destroy(gameObject);
                destroyed = true;
                GameManager.Instance.ReduceRemainingBalls(1);
            }
        });
    }

  

    public bool CheckBlockOverlapping(Vector2 p, float size)
    {       
        float dx = Mathf.Abs(clipPosition.x - p.x) - radius - size / 2;
        float dy = Mathf.Abs(clipPosition.y - p.y) - radius - size / 2;
        return dx < 0f && dy < 0f;      
    }
}
