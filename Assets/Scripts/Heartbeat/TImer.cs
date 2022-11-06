using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class Timer : MonoBehaviour{
    private double StartTime;
    private double HardLimit = 600.0;
    private Stopwatch stopwatch;
    private double ElapsedTime = 0.0;

    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        StartTime = stopwatch.ElapsedMilliseconds;
    }

    void Update()
    {
        ElapsedTime = 0.001*(stopwatch.ElapsedMilliseconds - StartTime);
    }

    double GetElapsedTIme(){
        return ElapsedTime;
    }

    void ResetTime(){
        stopwatch.Stop();
        stopwatch = new Stopwatch();
        StartTime = stopwatch.ElapsedMilliseconds;
    }
    
}