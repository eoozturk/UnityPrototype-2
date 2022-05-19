using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPosition = new Vector3(35, 0, 0);
    private float startDelay, repeatRate;
    private PlayerController controllerScript;

    // Start is called before the first frame update
    void Start()
    {
        startDelay = 2;
        repeatRate = 2;
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
        controllerScript = GameObject.Find("Player").GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnObstacle()
    {
        if(controllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }
        
    }
}
