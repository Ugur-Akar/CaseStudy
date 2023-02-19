using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    //Settings
    public int levelDuration = 100;
    // Connections
    public Material[] materials;
    public List<Station> stations;
    public TrainManager trainManager;
    public JunctionManager junctionManager;
    // State Variables
    
    // Start is called before the first frame update
    void Awake()
    {
        InitConnections();
        InitState();
    }
    void InitConnections()
    {
        EventManager.JunctionChanged += JunctionChanged;
    }
    void InitState()
    {
        for(int i = 0; i < stations.Count; i++)
        {
            stations[i].SetUpStation(materials);
        }

        trainManager.SetUpTrainManager(materials, stations.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonPressed()
    {
        trainManager.StartSpawning();
        junctionManager.ActivateJunctions();
    }

    void JunctionChanged(int nodeIndex)
    {
        trainManager.CheckTrainsOnChangedJunction(nodeIndex, junctionManager.GetNewSpline(nodeIndex));
    }

    public void StopSpawning()
    {
        trainManager.StopSpawning();
    }
}
