using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
  public Rigidbody rb;
//  public GameObject player;

  public float forwardForce = 2000f;
  public float xSpeed = 10.0f;
  public float grav = -9.8f;
  private Vector3 targetDirection;
  private Vector3 newDirection;
  private Vector3 startPosition = new Vector3(0,0,0);


  // Start is called before the first frame update
  void Start()
  {
    transform.Translate(0,0,0);

      //rb.useGravity = false;
  }

  // Update is called once per frame
  void Update()
  {
    if(rb.position.y<0){ //reset in case falls through.
        transform.position = new Vector3(rb.position.x,rb.position.y*0,rb.position.z);
    }
    if(rb.position.y>0){
      rb.AddForce(0,grav*Time.deltaTime,0); //custom gravity
    }

  //  rb.AddForce( forwardForce * Time.deltaTime,0,0);
    if (Input.GetKey("w")){
        rb.AddForce(0,forwardForce*Time.deltaTime,0);
          }
    if (Input.GetKey("a")){
    //  float step =  speed * Time.deltaTime; // calculate distance to move

      //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-0.8f,0.0f,0.0f), step);
      transform.Translate(-xSpeed*Time.deltaTime,0,0);
    }
    if (Input.GetKeyDown("d")){
      transform.Translate(xSpeed*Time.deltaTime,0,0);
      transform.Rotate(0,0,90*Time.deltaTime, Space.Self); //try Space.World
    }

        //Debug.Log(mouseClicked.ToString());




  }
}
