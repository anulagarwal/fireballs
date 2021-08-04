﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    [SerializeField]
    float radius = 4, power = 1;
    private Vector3 offset;
    private MeshFilter mesh;
    private MeshCollider meshCollider;
    private Vector3[] vertices;

    Mesh planeMesh;

    private void Start() {
        mesh = GetComponent<MeshFilter>();
        planeMesh = mesh.mesh;
        vertices = planeMesh.vertices;
        meshCollider = GetComponent<MeshCollider>();
    }
    public void DeformMesh(Vector3 positionHit) {
        positionHit = transform.InverseTransformPoint(positionHit);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = (vertices[i] - positionHit).sqrMagnitude;

            if (distance < radius) {
                Debug.Log("Deform");
                vertices[i] -= (Vector3.up * power); 
            }
        }
        planeMesh.vertices = vertices;
        meshCollider.sharedMesh = planeMesh;
    }
}
