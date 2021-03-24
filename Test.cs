using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public int count;

    public Transform player;


	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	//void Update () 
    //{
    //    if (Vector3.Distance(player.position, this.transform.position) < 10)
    //    {
    //        Vector3 direction = player.position - this.transform.position;
    //        direction.y = 0;
    //
      //      this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    //
      //      if (direction.magnitude > 5)
        //    {
  //              this.transform.Translate(0, 0, 0.05f);
  //          }
  //      }
  //      else if (Vector3.Distance(player.position, this.transform.position) > 10 && Vector3.Distance(player.position, this.transform.position) < 200)
  //      {
  //          if (count < 200)
  //          {
  //              transform.Translate(Vector3.forward * Time.deltaTime);
  //              count++;
  //          }
   //         else if (count == 200)
  //          {
  //              count = 0;
  //              transform.localRotation *= Quaternion.Euler(0, 180, 0);
//
 //           }
   //     }
   
	    //}
}
