using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay=2;
    private float rebeatDelay=2;

    [SerializeField]private GameObject[] obstaclesPrefab;
    private Vector3 spawnPosition=new Vector3(45,0,0);

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, rebeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle()
    {
        int index=Random.Range(0,obstaclesPrefab.Length);
        if(playerControllerScript.gameOver==false)
            Instantiate(obstaclesPrefab[index],spawnPosition+new Vector3(0, obstaclesPrefab[index].transform.position.y,0),obstaclesPrefab[index].transform.rotation);
    }
}
