using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockHandler : MonoBehaviour
{

    public static LockHandler Instance = null;

    [SerializeField] int lockHealth;
    [SerializeField] TextMeshPro healthText;
    [SerializeField] GameObject walls;
    [SerializeField] float eatCount;
    [SerializeField] float origHealth;
    [SerializeField] float lerpSpeed;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        lockHealth = GameManager.Instance.requiredBalls;
            healthText.text = lockHealth + "";
        origHealth = lockHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            lockHealth--;
            eatCount++;
            GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, Color.red, eatCount/origHealth);
                other.GetComponent<Ball>().destroyed = true;
                other.GetComponent<Ball>().smoke.SetActive(false);
                GameManager.Instance.AddBallToBasket(other.gameObject);
                BucketController.Instance.ballsSpawned.Remove(other.gameObject.GetComponent<Ball>());            
            Destroy(other.gameObject);
            healthText.text = lockHealth + "";
            if (lockHealth < 1)
            {
                //Play VFX
                //Destroy Chain
                Destroy(walls);
                Destroy(gameObject);
            }

        }
    }
}
