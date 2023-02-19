using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;


public class TrainManager : MonoBehaviour
{
    //Settings
    public float delayBetweenTrains = 1f;
    // Connections
    ObjectPooler objectPooler;

    public List<Train> activeTrains;
    public SplineComputer startingSpline;
    // State Variables
    List<Material> matList;

    float timer = 0;
    bool isSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        InitConnections();
        InitState();
    }
    void InitConnections()
    {
        objectPooler = GetComponent<ObjectPooler>();

        EventManager.RightArrival += TrainArrived;
        EventManager.WrongArrival += TrainArrived;
    }
    void InitState()
    {
        activeTrains = new List<Train>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnTrain();
        }

        if (isSpawning)
        {
            timer += Time.deltaTime;
            if(timer >= delayBetweenTrains)
            {
                timer = 0;
                SpawnTrain();
            }
        }
    }

    void SpawnTrain()
    {
        GameObject trainGO = objectPooler.GetPooledObject();
        
        if(trainGO != null)
        {
            Train train = trainGO.GetComponent<Train>();
            activeTrains.Add(train);
            int cIndex = Random.Range(0, matList.Count);

            train.SetUpTrain(startingSpline, cIndex, matList[cIndex]);
        }
    }

    void TrainArrived(Train train)
    {
        activeTrains.Remove(train);
        train.ResetTrain();
        if (!isSpawning)
        {
            if(activeTrains.Count == 0)
            {
                EventManager.AllTrainsArrivedEvent();
            }
        }
    }

    public void SetUpTrainManager(Material[] materials, int numberOfStations)
    {
        matList = new List<Material>();
        for(int i = 0; i < numberOfStations; i++)
        {
            matList.Add(materials[i]);
        }
    }

    public void StartSpawning()
    {
        SpawnTrain();
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void CheckTrainsOnChangedJunction(int nodeIndex, SplineComputer nextSpline)
    {
        for(int i = 0; i < activeTrains.Count; i++)
        {
            if (activeTrains[i].isInJunction)
            {
                if(activeTrains[i].indexOfNode == nodeIndex)
                {
                    activeTrains[i].SwitchSplines(nextSpline, true);
                }
            }
        }
    }

}
