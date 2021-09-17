using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public static ObjectPool Instance;
    public List<Ball> pooledObjects;
    
    [SerializeField]
    Ball objectPrefab;

    [SerializeField]
    int pooledAmount = 20;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        pooledObjects = new List<Ball>();

        for (int i = 0; i < pooledAmount; i++)
        {
            Ball obj = Instantiate<Ball>(objectPrefab);
            obj.gameObject.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public Ball GetPooledObject() {
        for (int i = 0; i < pooledAmount; i++)
        {
            if (pooledObjects[i] != null && !pooledObjects[i].gameObject.activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        return null;
    }
}