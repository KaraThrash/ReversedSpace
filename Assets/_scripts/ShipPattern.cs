using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPattern : MonoBehaviour
{
  public int phase,shiptype;
  public float dist0 = 5,speed = 1.0f,rotSpeed = 5.0f;
  public GameObject earthShip;
    public Vector3 startpos,targetpos;
    public List<Vector3> pattern;
private Rigidbody2D rb;
private Ship myShip;
  //fly diagonal from spawn - for x distance - then turn up and loop back around to the start

    // Start is called before the first frame update
    void Start()
    {
      myShip = GetComponent<Ship>();
      rb = GetComponent<Rigidbody2D>();
      // SetStartPositionInformation();
    }
    public void SetStartPositionInformation()
    {
      earthShip = myShip.earthShip;
      switch(shiptype){
        case 0://DiagonalShip
        startpos = transform.position;
        //vector 3 list for a easy to change flight path
        if(pattern.Count > 0){  targetpos = startpos - pattern[0];}
        else{targetpos = new Vector3(startpos.x - dist0,startpos.y - dist0,startpos.z);}

        //
        break;
        case 1: //square ship
              startpos = transform.position;
              //vector 3 list for a easy to change flight path
              if(pattern.Count > 0){  targetpos = startpos - pattern[0];}
              else{targetpos = new Vector3(startpos.x - dist0,startpos.y - dist0,startpos.z);}
        break;
        default://
        startpos = transform.position;
        targetpos = new Vector3(startpos.x - dist0,startpos.y - dist0,startpos.z);
        break;

      }

    }
    // NOTE: instead of using the vector 3 to define the flight path using the switch statements allows for variation of similar types
    public void ExecutePattern()
    {
      switch(shiptype){
        case 0://Diagonal flight Ship, right traingle pattern bassed on starting location
          DiagonalShip();
        break;
        case 1://straight forward Straight out the side, square pattern
          SquareShip();
        break;
        case 3://straight forward
          BaseMoney();
        break;
        default://

        break;

      }
    }
    public void HandleRotation()
    {
      //face direction of travel
      if(targetpos.z == 0){
        Vector3 diff = ( transform.position - targetpos );
         diff.Normalize();

         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotSpeed * 2 * Time.deltaTime);

      }
      //face straight down
      else if(targetpos.z == 1){
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotSpeed * 2 * Time.deltaTime);

      }
      //face earth ship
      else if(targetpos.z == 2){
        Vector3 diff = ( transform.position - earthShip.transform.position );
         diff.Normalize();

         float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotSpeed * 2 * Time.deltaTime);

      }
      else{}
    }
    public void DiagonalShip()
    {
      HandleRotation();
      switch(phase){
        case 0://move from start position for dist0
          rb.velocity = ((targetpos - transform.position).normalized * speed );

          if(Vector2.Distance(transform.position, targetpos) < 1)
          {phase = 1;
            if(pattern.Count > 1){  targetpos = new Vector3(targetpos.x - pattern[1].x,targetpos.y - pattern[1].y,pattern[1].z);}
            else{targetpos = new Vector3(targetpos.x ,targetpos.y + dist0,startpos.z);}

          }
        break;
        case 1:
        rb.velocity = ((targetpos - transform.position).normalized * speed );
        if(Vector2.Distance(transform.position, targetpos) < 1)
        {
          phase = 3;
          if(pattern.Count > 2){   targetpos = new Vector3(startpos.x ,startpos.y ,pattern[2].z);}
          else{targetpos = new Vector3(startpos.x ,startpos.y ,startpos.z);}

        }
        break;
        default://if lost in the loop return to start position
        rb.velocity = ((targetpos - transform.position).normalized * speed );
        if(Vector2.Distance(transform.position, targetpos) < 1)
        {phase = 0;

          if(pattern.Count > 0){   targetpos = new Vector3(startpos.x - pattern[0].x,startpos.y - pattern[0].y,pattern[0].z);}
          else{targetpos = new Vector3(startpos.x - dist0,startpos.y - dist0,startpos.z);}
        }
        break;

      }

    }
    public void SquareShip()
    {
      HandleRotation();
      switch(phase){
        case 0://strauight down

              targetpos = new Vector3(transform.position.x,transform.position.y - 1 ,pattern[0].z );
              rb.velocity = ((targetpos - transform.position).normalized * speed );

              if(transform.position.y < earthShip.transform.position.y)
              {
                phase = 1;
                if(pattern.Count > 1){  targetpos = startpos - pattern[1];}
                else{targetpos = new Vector3(targetpos.x ,targetpos.y + dist0,startpos.z);}

              }
        break;
        case 1://straight sideways
          targetpos = new Vector3(transform.position.x - 1,transform.position.y , pattern[1].z);

              rb.velocity = ((targetpos - transform.position).normalized * speed );
              if(Vector2.Distance(transform.position, earthShip.transform.position) > 5)
              {
                phase = 2;
                if(pattern.Count > 2){  targetpos = startpos - pattern[2];}
                else{targetpos = new Vector3(startpos.x ,startpos.y ,startpos.z);}

                }
        break;
        case 2://strauight up
              targetpos = new Vector3(transform.position.x,transform.position.y + 1 , pattern[2].z);
              rb.velocity = ((targetpos - transform.position).normalized * speed );

              if(transform.position.y >= startpos.y)
              {
                phase = 3;
                if(pattern.Count > 1){  targetpos = startpos;}
                else{targetpos = new Vector3(targetpos.x ,targetpos.y + dist0,startpos.z);}

              }
        break;
        default://if lost in the loop return to start position
                rb.velocity = ((targetpos - transform.position).normalized * speed );
                if(Vector2.Distance(transform.position, targetpos) < 1)
                {phase = 0;
                  targetpos = startpos ;
        }
        break;

      }

    }
    public void BaseMoney()
    {
      targetpos = new Vector3(transform.position.x,transform.position.y - 1 , 1);
      rb.velocity = ((targetpos - transform.position).normalized * speed );

      if(transform.position.y < earthShip.transform.position.y)
      {
        //give bonus

      }


    }
    public void OtherShip()
    {
      switch(phase){
        case 0://

        break;
        case 1:

        break;
        default://
        break;

      }

    }
}
