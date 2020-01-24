using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rowspot myspot;
    public GameObject bullet,earthShip;
    public ShipManager shipManager;
    public int hp,points,cost;//points for base +$ or other bonuses
    public int rhythm,rhythmcount;
    public float speed,rotSpeed,shootTimer,shootTime;
    public float collisionTime,collisionTimer; // so that ships can be displaced but not vibrate when close to their spot
    public bool shoot,activated;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Activate(GameObject target)
    {
      activated = true;
      earthShip = target;
      GetComponent<Collider2D>().enabled = true;
      GetComponent<ShipPattern>().SetStartPositionInformation();
    }
    // Update is called once per frame
    void Update()
    {
      //move towards rowspot
      if(activated == true){

          if(Input.GetKeyDown(KeyCode.Space)){Die();}
          //TODO: set this to be specific based on the ship type
              if(earthShip != null){GetComponent<ShipPattern>().ExecutePattern();}


      }
      //have all ships use the same clock

      // if(shoot == true && shootTime != -1)
      // {
      //   ShootClock();
      // }
    }
    public bool CheckRhythm()
    {

      rhythmcount++;
      if(rhythmcount == rhythm)
      { rhythmcount = 0;
        return true;
      }
      return false;
    }
    public GameObject GetBulletType()
    {
      return bullet;
    }
    public Vector2 GetForward()
    {
      return new Vector2(transform.position.x,transform.position.y - 1);
      //TODO: decide if this should just be a universal 1 and fire to the square in front
      // return new Vector2(transform.position.x,transform.position.y - GetComponent<Collider2D>().bounds.size.y);

    }
    public void FireBullet()
    {
      // GameObject tempbullet = Instantiate(bullet,transform.position - transform.up,transform.rotation);
      // tempbullet.active = true;
      //launch the bullet and change it's parent
      // tempbullet.GetComponent<Bullet>().Launch(this.transform);
      // tempbullet.transform.rotation =
      // Instantiate(firingship.GetBulletType(),firingship.GetForward(),transform.rotation);

    }
    public void TakeDamage(int dmg)
    {
      hp -= dmg;
      if(hp <= 0){Die();}
    }

    public void ToggleShoot(bool onOrOff)
    {shoot = onOrOff;}

    public void ShootClock()
    {
      shootTimer -= Time.deltaTime;
      if(shootTimer <= 0)
      {
        shootTimer = shootTime;
        Instantiate(bullet,new Vector2(transform.position.x,transform.position.y - (GetComponent<Collider2D>().bounds.size.y) ),transform.rotation);
      }
    }
    public void MoveToMySpot()
    {
      Vector3 targetspot = new Vector3(earthShip.transform.position.x,earthShip.transform.position.y + 5,earthShip.transform.position.z);
      if(Vector2.Distance(transform.position, targetspot) > 5)
      {
        //structures dont rotate but still move to position
              if(rotSpeed != 0){
              //look in the direction the ship is moving
              Vector3 diff = ( transform.position - targetspot );
               diff.Normalize();

               float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
               transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotSpeed * 2 * Time.deltaTime);
               GetComponent<Rigidbody2D>().AddForce(transform.up * -speed * Time.deltaTime,ForceMode2D.Impulse);
              // GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed * Time.deltaTime,ForceMode2D.Impulse);
              }else
              {
                GetComponent<Rigidbody2D>().AddForce((targetspot - transform.position).normalized * speed * Time.deltaTime,ForceMode2D.Impulse);
              }
        }
        else{

            if(collisionTimer > 0){
                //after bumping into something, take time to readjust before snapping to position
              collisionTimer -= Time.deltaTime;
              transform.rotation = Quaternion.RotateTowards(transform.rotation, earthShip.transform.rotation, rotSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce((targetspot - transform.position).normalized * speed *  Time.deltaTime,ForceMode2D.Impulse);
            }
            else
            {
              Vector3 diff = ( transform.position - targetspot );
               diff.Normalize();

               float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
               transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotSpeed * 2 * Time.deltaTime);
            // GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed * 4 * Time.deltaTime,ForceMode2D.Impulse);
            // transform.position = Vector3.MoveTowards(transform.position, targetspot, speed * Time.deltaTime);
            }


      }

      // transform.position = Vector3.MoveTowards(transform.position, myspot.transform.position, speed * Time.deltaTime);

    }
    public void OnMouseUp()
    {
        if(activated == true){
          //do action
          // shipManager.FireBullet(new Vector2(transform.position.x,transform.position.y - (GetComponent<Collider2D>().bounds.size.y) ));
          // GameObject clone = Instantiate(bullet,new Vector2(transform.position.x,transform.position.y - (GetComponent<Collider2D>().bounds.size.y) ),transform.rotation) as GameObject;

        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        collisionTimer = collisionTime;
    }
    public void Die()
    {

      //set row spot as open
          if(myspot != null){  myspot.SetOpen();}
          //tell ship manager this ship is dead and needs to be removed from the list
        if(shipManager != null){shipManager.ShipDie(GetComponent<Ship>());}
        else{  Destroy(this.gameObject);}


    }
}
