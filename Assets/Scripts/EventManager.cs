using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Dreamteck.Splines;



public static class EventManager
{
    public static event Action<int> JunctionChanged;
    public static event Action<int, Train> TrainArrivedAtNode;
    public static event Action<Train> RightArrival;
    public static event Action<Train> WrongArrival;
    public static event Action AllTrainsArrived;

    public static void JunctionChangedEvent(int nodeIndex)
    {
        JunctionChanged?.Invoke(nodeIndex);
    }

    public static void TrainArrivedAtNodeEvent(int indexOfNode, Train train)
    {
        TrainArrivedAtNode?.Invoke(indexOfNode, train);
    }

    public static void RightArrivalEvent(Train train)
    {
        RightArrival?.Invoke(train);
    }

    public static void WrongArrivalEvent(Train train)
    {
        WrongArrival?.Invoke(train);
    }

    public static void AllTrainsArrivedEvent()
    {
        AllTrainsArrived?.Invoke();
    }

    public static void CleanEvents()
    {
        JunctionChanged = null;
        TrainArrivedAtNode = null;
        RightArrival = null;
        WrongArrival = null;
        AllTrainsArrived = null;
    }
}
