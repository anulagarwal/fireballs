using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float rotSpeed = 0f;
    [SerializeField] private Vector3 rotationAxis = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        transform.Rotate(rotationAxis * Time.deltaTime * rotSpeed);
    }
    #endregion
}

