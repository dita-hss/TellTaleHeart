using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.Events;

public class HeartBeat : MonoBehaviour{

    //heart beating rate in 0-1 scale
    public double HeartRate = 0.1;//heartRate = beat/seconds = 1/hearbeatInterval
    public double HeartIncreaseRate = 1.2;
    public double LifeSpan = 60;
    public UnityEvent<double> UpdateLife;
    public UnityEvent Beating;
    public UnityEvent GameEnds;
    public SoundEmitter _emmiter;
    private Stopwatch stopwatch;
    private double ElapsedTime = 0.0;
    private double LastBeatTime = 0.0;
    private double StartTime;
    private bool Relaxing = false;
    private bool Awaken = false;

    //emmit sound per timer
    //Reference to UI
    //Unity Event

    void Start(){
        Awake();
    }

    void Update()
    {
        if (Awaken){
            ElapsedTime = 0.001*(stopwatch.ElapsedMilliseconds - StartTime);

            //Heart makes a beat
            if (ElapsedTime - LastBeatTime > 1.0/HeartRate){
                BeatFaster(HeartIncreaseRate);
                Beating.Invoke();
                LastBeatTime = ElapsedTime;
                _emmiter.EmitSound();
            }

            if (Relaxing){
                InRelaxation();
            }

            //Ran out of time
            if (ElapsedTime > LifeSpan){
                LifeEnds();
                return;
            }
            UpdateLife.Invoke(LifeSpan - ElapsedTime);
        }

    }

    void Awake()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        StartTime = stopwatch.ElapsedMilliseconds;
        Awaken = true;
    }

    void BeatFaster(double ratio)
    {
        HeartRate = HeartRate*ratio;
        if(HeartRate > 1.0){
            LifeEnds();
        }

    }

    void relax(double ratio)
    {
        if(HeartRate*ratio >= 0.0001){
            HeartRate = HeartRate*ratio;
        }
    }

    void BeingDectected()
    {
        BeatFaster(HeartIncreaseRate);
    }

    void InRelaxation(){
        relax(1.0/HeartIncreaseRate);
    }

    void ResetTime(){
        stopwatch = new Stopwatch();
        StartTime = stopwatch.ElapsedMilliseconds;
    }

    void LifeEnds(){
        GameEnds.Invoke();
        stopwatch.Stop();
        Awaken = false;
        //Behaviour TO BE IMPLEMENTED, everything else need to be stoped
    }

    void EnterRelaxroom(){
        Relaxing = true;
    }

    void LeaveRelaxRoom(){
        Relaxing = false;
    }

    
}