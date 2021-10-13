using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AwesomeText : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float fadeSpeed;
    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        GetComponent<TextMeshPro>().alpha -= fadeSpeed;
    }
}
