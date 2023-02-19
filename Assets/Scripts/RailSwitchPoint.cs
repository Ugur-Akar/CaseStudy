using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System;

public class RailSwitchPoint : MonoBehaviour
{
    //Settings
    public int index = 0;
    // Connections
    public List<GameObject> junctionGos;
    public SplineComputer entrySpline;
    public List<SplineComputer> exitSplines;

    List<SplineComputer> junctionSplines;
    public Node entryNode;

    public Clickable cliackable;
    // State Variables
    int junctionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        InitState();
    }
    void InitConnections()
    {
        
    }
    void InitState()
    {
        junctionSplines = new List<SplineComputer>();

        for(int i = 0; i < junctionGos.Count; i++)
        {
            if(i != 0)
            {
                junctionGos[i].SetActive(false);
            }
            else
            {
                junctionGos[i].SetActive(true);
            }

            junctionSplines.Add(junctionGos[i].GetComponent<SplineComputer>());

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    void SwitchJunction()
    {
        //Switch To Next
        junctionIndex++;
        junctionIndex = junctionIndex % junctionGos.Count;

        for(int i = 0; i <junctionGos.Count; i++)
        {
            junctionGos[i].SetActive(false);
        }

        junctionGos[junctionIndex].SetActive(true);

        EventManager.JunctionChangedEvent(index);
    }

    

    public void InitJunction()
    {
        if(entryNode.TryGetComponent<NodeIndexer>(out NodeIndexer nIndexer))
        {
            nIndexer.index = index;
        }
    }

    public SplineComputer GetActiveJunction()
    {
        return junctionSplines[junctionIndex];
    }

    public void ActivateJunction()
    {
        cliackable.OnClick += SwitchJunction;
    }
}
