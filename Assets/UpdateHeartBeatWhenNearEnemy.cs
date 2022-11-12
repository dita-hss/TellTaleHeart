using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HeartBeat))]
public class UpdateHeartBeatWhenNearEnemy : MonoBehaviour
{

    public float minDist;
    public float maxDist;
    public bool needsLOS = true;
    public float checkTime = 0.5f;

    private float curCheckTime = 0.5f;

    private float maxHeartRateIncreaseMulti = 3.0f; 

    private HeartBeat setHeartBeat;
    private BasicEnemyAI[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        setHeartBeat = GetComponent<HeartBeat>();

        enemies = FindObjectsOfType<BasicEnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (curCheckTime > 0.0f)
        {
            curCheckTime -= Time.deltaTime; 

            if (curCheckTime <= 0.0f)
            {

                float minEnemyDist = maxDist + 1.0f; 

                foreach (BasicEnemyAI enemy in enemies)
                {
                    

                    if (needsLOS)
                    {
                        Vector3 dir = (enemy.transform.position - transform.position).normalized;
                        Physics.Raycast(transform.position + dir * 1.5f, dir, out RaycastHit hit, maxDist);

                        if (hit.distance < minEnemyDist)
                        {
                            minEnemyDist = hit.distance;
                        }
                    }
                    else
                    {
                        float dist = (enemy.transform.position - transform.position).magnitude;
                        if (dist < minEnemyDist)
                        {
                            minEnemyDist = dist;
                        }
                    }
                        
                }

                if (minEnemyDist < minDist)
                {
                    setHeartBeat.HeartRate = setHeartBeat.defaultHeartRate * maxHeartRateIncreaseMulti;
                }
                else if (minDist < maxDist)
                {
                    setHeartBeat.HeartRate = setHeartBeat.defaultHeartRate * ((1 + (maxHeartRateIncreaseMulti - 1)) * ((minEnemyDist - minDist) / (maxDist - minDist))); 
                }
                else
                {
                    setHeartBeat.HeartRate = setHeartBeat.defaultHeartRate;
                }

                curCheckTime = checkTime;
            }
        }
        
    }
}
