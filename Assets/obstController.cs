using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstController : MonoBehaviour
{
  //public GameObject cube;
  public GameObject[] obstacles;
  public float speed;
  public Vector3 origin;
  public float destinationz;
  public float zSpacing; //space between each obstacle
  public GameObject cube;

  private float timeSpacing; //time between each obstacle
//  private float timer;
  private float duration = 20.0f;
  private float[] startTimes;
  private float[] movementDuration;
  private float[] waitTimes;
  public int obsQty = 5;


    // Start is called before the first frame update
    void Start()
    {
      //  timer = 0.0f;
        obstacles = new GameObject[obsQty];
        startTimes = new float[obsQty];
        movementDuration = new float[obsQty];
        waitTimes = new float[obsQty];
        speed = 2.0f;
        origin = new Vector3(0,0,0);
        zSpacing = 25.0f;
        destinationz = 200;
        timeSpacing = duration/obsQty;

        for (int i= 0; i<obsQty; i++){ //build a bunch of obstacles
          obstacles[i] = new GameObject();
          obstacles[i].AddComponent<obstGenerator>();
          obstacles[i].GetComponent<obstGenerator>().block = cube;
          obstacles[i].GetComponent<obstGenerator>().active = false;
          obstacles[i].GetComponent<obstGenerator>().obsArraySize = 15;
          obstacles[i].GetComponent<obstGenerator>().gapQty = 6;
        }

      /*  for (int i= 0; i<obsQty; i++){ //builds array of initial start times
          startTimes[i] = i*timeSpacing;
        } */ 
        for (int i= 0; i<obsQty; i++){ //builds array of initial runtime
          movementDuration[i] = 0;
        }
        for (int i= 0; i<obsQty; i++){ //builds array of initial wait times
          waitTimes[i] = i*timeSpacing;
        }

    }

    // Update is called once per frame
    void Update()
    {
      //updateTime();
      for(int i = 0; i<obsQty; i++){
        //if exceeds duration, reset and wait.
        if(movementDuration[i]>duration){
          obstacles[i].GetComponent<obstGenerator>().rebuild = true;
          obstacles[i].GetComponent<obstGenerator>().active = false;
          movementDuration[i] = 0;
          waitTimes[i] = timeSpacing;//check

        }
        //base movement case
        else if(waitTimes[i] <= 0){
          obstacles[i].GetComponent<obstGenerator>().active = true;
          movementDuration[i] += Time.deltaTime;

        }
        else{ //object is still waiting. update wait time
          waitTimes[i] -= Time.deltaTime;
        }

      }

      }


}
