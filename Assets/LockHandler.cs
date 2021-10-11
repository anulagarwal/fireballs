using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockHandler : MonoBehaviour
{
    [SerializeField] int lockHealth;
    [SerializeField] TextMeshPro healthText;
    [SerializeField] GameObject walls;

    private void Start()
    {
            healthText.text = lockHealth + "";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            lockHealth--;           
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
