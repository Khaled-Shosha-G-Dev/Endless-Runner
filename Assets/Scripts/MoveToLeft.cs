using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLeft : MonoBehaviour
{
    [SerializeField] private float speed=20;
    private PlayerController playerControllerScript;
    private float leftBound=10;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ObjectMovement();
        X2Speed();
        DestroyBoudedObject();
    }
    void ObjectMovement()
    {
        if (playerControllerScript.gameOver==false&&playerControllerScript.startRunning==true) 
            transform.Translate(Vector3.left*Time.deltaTime*speed);
    }
    void X2Speed()
    {
        if(Input.GetKey(KeyCode.LeftShift)&&playerControllerScript.isOnGround&&playerControllerScript.gameOver == false && playerControllerScript.startRunning == true) 
            transform.Translate(Vector3.left * Time.deltaTime * speed*1.5f);
    }
    void DestroyBoudedObject()
    {
        if(transform.position.x<-leftBound&&gameObject.CompareTag("Obstacle"))
            Destroy(gameObject);
    }
}
