using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;


public class JunctionManager : MonoBehaviour
{
    //Settings

    // Connections
    public List<RailSwitchPoint> junctions;

    // State Variables
    
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        InitState();
    }
    void InitConnections()
    {
        EventManager.TrainArrivedAtNode += TrainArrivedAtNode;
    }

    private void TrainArrivedAtNode(int nodeIndex, Train train)
    {
        SplineComputer nextSpline = junctions[nodeIndex].GetActiveJunction();
        train.SwitchSplines(nextSpline);
    }

    void InitState()
    {
        for(int i = 0; i < junctions.Count; i++)
        {
            junctions[i].index = i;
            junctions[i].InitJunction();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SplineComputer GetNewSpline(int jIndex)
    {
        return junctions[jIndex].GetActiveJunction();
    }

    public void ActivateJunctions()
    {
        foreach(RailSwitchPoint junc in junctions)
        {
            junc.ActivateJunction();
        }
    }
}
