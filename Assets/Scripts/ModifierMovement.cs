using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierMovement : MonoBehaviour
{

    [SerializeField] float movementRange;
    [SerializeField] float moveSpeed;

    bool ForceStop;
    private Vector3 origPos;
    bool isMovingRight;
        // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ForceStop)
        {
            if (transform.position.x > origPos.x + movementRange && isMovingRight)
            {
                isMovingRight = false;
            }

            if(transform.position.x <origPos.x - movementRange && !isMovingRight)
            {
                isMovingRight = true;
            }

            if (isMovingRight)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            if (!isMovingRight)
            {
                transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
            }

        }
    }
    public void Stop()
    {
        ForceStop = true;
    }
}
