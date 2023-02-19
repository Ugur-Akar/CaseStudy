using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class Train : MonoBehaviour
{
    //Settings
    public int colorIndex = 0;
    float startPercent = 0.02f;
    // Connections
    SplineFollower follower;
    // State Variables

    public int indexOfNode = -1;
    public bool isInJunction = false;
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        //InitState();
    }
    void InitConnections()
    {
        follower = GetComponent<SplineFollower>();
        follower.onNode += Follower_onNode;
    }

    private void Follower_onNode(List<SplineTracer.NodeConnection> passed)
    {
        int connectionIndex = 0;
        if(passed[0].node.TryGetComponent<NodeIndexer>(out NodeIndexer nIndexer))//EnteringJunction
        {
            isInJunction = true;
            indexOfNode = nIndexer.index;
            EventManager.TrainArrivedAtNodeEvent(indexOfNode, this);
        }
        else//Exiting Junction
        {
            isInJunction = false;
            indexOfNode = -1;
            Node.Connection[] connections = passed[0].node.GetConnections();
            for(int i = 0; i < connections.Length; i++)
            {
                if(connections[i].spline != follower.spline)
                {
                    connectionIndex = i;
                }
            }

            SwitchSplines(connections[connectionIndex].spline);
        }


    }

    public void SwitchSplines(SplineComputer nextSpline, bool change = false)
    {
        double newPercent = startPercent;
        if (change)
        {
            newPercent = follower.result.percent;
        }

        follower.spline = nextSpline;
        follower.SetPercent(newPercent);
        follower.RebuildImmediate();
    }

    void InitState(){
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUpTrain(SplineComputer startingSpline, int cIndex, Material material)
    {
        follower.spline = startingSpline;
        follower.SetPercent(0);
        colorIndex = cIndex;
        GetComponent<MeshRenderer>().material = material;
    }
    
    public void ResetTrain()
    {
        follower.spline = null;
        colorIndex = 0;
        isInJunction = false;
        indexOfNode = -1;
        gameObject.SetActive(false);
    }
}
