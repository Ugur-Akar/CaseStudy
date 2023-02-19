using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    //Settings
    public int amountToPool = 20;
    // Connections
    public GameObject targetObject;
    public List<GameObject> objectList;
    // State Variables

    // Start is called before the first frame update

    private void Awake()
    {
        PoolObjects();
    }
    void Start()
    {
        //InitState();
    }
    void InitConnections(){
    }
    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PoolObjects()
    {
        objectList = new List<GameObject>();

        for(int i = 0; i < amountToPool; i++)
        {
            GameObject train = Instantiate(targetObject, transform);
            objectList.Add(train);

            train.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeInHierarchy)
            {
                objectList[i].SetActive(true);
                return objectList[i];
            }
        }

        return null;
    }
}
