using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Station : MonoBehaviour
{
    //Settings
    public int colorIndex = 0;
    // Connections

    // State Variables
    
    // Start is called before the first frame update
    void Start()
    {
        //InitConnections();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            Train incomingTrain = other.GetComponent<Train>();
            if(incomingTrain.colorIndex == colorIndex)
            {
                EventManager.RightArrivalEvent(incomingTrain);
            }
            else
            {
                EventManager.WrongArrivalEvent(incomingTrain);
            }
        }
    }

    public void SetUpStation(Material[] materials)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[colorIndex];
    }
}
