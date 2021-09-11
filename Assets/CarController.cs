using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int movementSpeed = 5;
    public int turnSpeed = 5;
    Vector3 nextPosition = Vector3.forward;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            nextPosition.x = transform.position.x - Time.deltaTime * turnSpeed;
            // transform.position = new Vector2(transform.position.x - Time.deltaTime * movementSpeed, transform.position.y);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            nextPosition.x = transform.position.x + Time.deltaTime * turnSpeed;
            // transform.position = new Vector2(transform.position.x + Time.deltaTime * movementSpeed, transform.position.y);
        }
        nextPosition.z = transform.position.z + Time.deltaTime * movementSpeed;
        transform.position = nextPosition;
    }
}
