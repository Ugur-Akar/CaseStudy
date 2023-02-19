using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clickable : MonoBehaviour
{
    //Settings

    // Connections
    public event Action OnClick;
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

    private void OnMouseDown()
    {
        OnClick?.Invoke();
    }
}
