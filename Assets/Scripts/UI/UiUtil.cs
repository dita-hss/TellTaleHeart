using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiUtil : MonoBehaviour
{
    // GameObject RemainingTime;
    public double time  = 0.0;
    public int heartbeat = 0;
    private int DoingHeartBeat = -1;
    // Start is called before the first frame update
    void Start()
    {
        // RemainingTime = GameObject.find("RemainingTime")
    }

    // Update is called once per frame
    void Update()
    {
        if(DoingHeartBeat > -1){
            if(DoingHeartBeat <40){ DoingHeartBeat ++;}
            else{
                    GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize/1.5f;
                    DoingHeartBeat = -1;
            }
        }
        // updateRemainingTime(10.0);
    }

    public void updateRemainingTime(double elapsedTime){
        time = elapsedTime;
        // printf("update remaining time called");
        GetComponent<TextMeshProUGUI>().text = $"Remaining Time: {elapsedTime}";
    }

    public void heartBeatAnime(){
        GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize*1.5f;
        DoingHeartBeat = 0;
        heartbeat ++;
        //TODO: implement animation of a beating heart
    }
}
