using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
