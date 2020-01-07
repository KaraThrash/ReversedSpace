using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyShip : MonoBehaviour
{
  public Pathfind pathFind;
  public List<int> bestPath;
  public int hp,maxDistanceFromCenter,direction,currentTarget,timeshit;
  public float speed,curspeed,shootTimer,shootTime,metranome;
  public Text timeshittext;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


      // if( transform.localPosition.x < 0 || transform.localPosition.x > 10)
      // {
      //   transform.Translate(transform.right * -curspeed * 1.1f  * Time.deltaTime);
      // }
      transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(currentTarget,0), speed  * Time.deltaTime);
      if(transform.localPosition.x == currentTarget)
      {
        // if(bestPath.Count > 0){
        //   bestPath.RemoveAt(0);}
        // if(bestPath.Count > 0){
        //
        //   currentTarget = bestPath[0];
        // if(bestPath[0] < transform.localPosition.x)
        // { curspeed = speed; print(" left");}
        // else  if(bestPath[0] > transform.localPosition.x)
        //   { curspeed = -speed;print(" right");}
        //   else{curspeed = 0;print("stay");}
        //
        // }
        //
        //   else{bestPath = pathFind.GetPath();  currentTarget = bestPath[0];}
      }
      // transform.Translate(transform.right * curspeed  * Time.deltaTime);



      // if(Random.Range(0, 150) == 0){direction = -direction;}
      // CheckEdge();
      //  ShootClock();

    }
    public void IncrementClock()
    {
      if(bestPath.Count > 0){
        bestPath.RemoveAt(0);}
      if(bestPath.Count > 0){

        currentTarget = bestPath[0];
      if(bestPath[0] < transform.localPosition.x)
      { curspeed = speed; }
      else  if(bestPath[0] > transform.localPosition.x)
        { curspeed = -speed;}
        else{curspeed = 0;}

      }

        else{
          bestPath = pathFind.GetPath();
          if(bestPath.Count > 0){  currentTarget = bestPath[0];}

        }

      // metranome -= 1;
      // if(metranome <= 0)
      // {
      //     metranome = 1;
      //     if(bestPath.Count > 0){
      //     currentTarget = bestPath[0];
      //     if(bestPath[0] < transform.localPosition.x)
      //     { curspeed = speed; print(" left");}
      //     else  if(bestPath[0] > transform.localPosition.x)
      //       { curspeed = -speed;print(" right");}
      //       else{curspeed = 0;print("stay");}
      //       bestPath.RemoveAt(0);
      //     }
      //
      //       else{bestPath = pathFind.GetPath();}
      // }

    }
    public void ShootClock()
    {
      shootTimer -= Time.deltaTime;
      if(shootTimer <= 0)
      {
        shootTimer = shootTime;
        Instantiate(bullet,new Vector2(transform.position.x,transform.position.y + (GetComponent<Collider2D>().bounds.size.y) ),transform.rotation);
      }
    }
    public void CheckEdge()
    {
        //zero speed means it is a square in a row


            if(transform.localPosition.x < -maxDistanceFromCenter){direction = 1;}
            if(transform.localPosition.x > maxDistanceFromCenter){direction = -1;}


    }
    public void TakeDamage(int dmg)
    {
      hp -= dmg;

    }
    public void OnCollisionEnter2D(Collision2D col)
    {
      if(col.transform.GetComponent<Bullet>() != null  )
      {
        timeshit++;
        timeshittext.text = timeshit.ToString();
      //  print("bullet hit enemy ship");
      //  hp --;
      }

    }
    public void OnTriggerEnter2D(Collider2D col)
    {
      if(col.transform.GetComponent<Bullet>() != null  )
      {
        timeshit++;
        timeshittext.text = timeshit.ToString();
      //  print("bullet hit enemy ship");
      //  hp --;
      }

    }

}
