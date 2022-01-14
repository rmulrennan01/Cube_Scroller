using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstGenerator : MonoBehaviour{
/* this builds a cluster of obstacles*/
public int obsArraySize = 6;
//public GameObject blockGroup;
public GameObject block;
public float zSpeed = 10f;
private int[,] obstMask;  //array for obstacles binary coordinates
private GameObject[] obstacleArray;
private System.Random rnd = new System.Random();
private int randIntx;
private int randInty;
public bool rebuild;
public int gapQty = 3;
public int spawnX = 0;
public int spawnY = 0;
public int spawnZ = 0;
public bool active;




    // Start is called before the first frame update
    void Start()
    {
      //Instantiate(blockGroup, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity); //build the parent container object
      rebuild = true;
      active = false;
      obstMask = new int[obsArraySize,obsArraySize];
      obstacleArray = new GameObject[obsArraySize*obsArraySize];
      buildObjects(); //builds the game objects. These will never be destroyed.
      updateOreintation(); //positions them in a grid
      //Random.seed = System.DateTime.Now.Millisecond;
    }

    // Update is called once per frame
    void Update()
    {
      if (rebuild == true){ //test if the obstacle cluster needs to be rebuilt.
        clearArray(); //resets the mask array to all 1's (no gaps)
        for (int i = 0; i<gapQty; i++){
          randomOpening(); //adds a random opening mask of 0 to the array.
        }
        rebuild = false;
        updateOreintation();
      }
      if (active == true){ //active dictates if the group is moving or not
        updatePositions();
      }
    }


    void clearArray(){ //populates the obstMask array with default of 1;
      for (int i = 0; i < obsArraySize; i++){
        for (int j = 0; j < obsArraySize; j++){
          obstMask[i,j] = 1; //1 represents obstacle
        }
      }
    }


    void randomOpening(){ //method to build random array masks
      //Random.Seed = (int)System.DateTime.Now.Ticks;
      randIntx = Random.Range(0,obsArraySize-1);
    //  randIntx = rnd.Next(0,obsArraySize-1); //x origin of first gap
      randInty = Random.Range(0,obsArraySize-1); //y origin of first gap
      while(obstMask[randIntx,randInty] ==0){ //case if there is already a gap origin at these coordinates
        randIntx = Random.Range(0,obsArraySize-1); //reset coordinates to try again.
        randInty = Random.Range(0,obsArraySize-1);
      }
      obstMask[randIntx,randInty] = 0;
      obstMask[randIntx,randInty+1] = 0;
      obstMask[randIntx+1,randInty] = 0;
      obstMask[randIntx+1,randInty+1] = 0;
      //builds a 2x2 block in the 2d array to represent a gap
    }

    void buildObjects(){ //takes prefabs, spawns them into the scene, and adds them to an array
        for (int i = 0; i<obsArraySize*obsArraySize; i++){
          obstacleArray[i] = Instantiate(block, new Vector3(spawnX, spawnY, spawnZ), Quaternion.identity);
        }
    }


    void updatePositions(){ //updates the obstacle positions.
        int a = 0;
        for (int i = 0; i<obsArraySize; i++){
          for(int j = 0; j<obsArraySize; j++){
            obstacleArray[a].transform.Translate(0,0,obstMask[i,j]*zSpeed*Time.deltaTime);
            a++;
          }

      }
    }


    void updateOreintation(){ //orients the gameobjects into grid
        int a = 0;
        for (int i = 0; i<obsArraySize; i++){
          for(int j = 0; j<obsArraySize; j++){
            obstacleArray[a].transform.position = new Vector3(i,j,0);
            a++;
          }
        }

    }



}
