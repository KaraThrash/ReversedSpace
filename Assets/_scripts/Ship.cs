﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rowspot myspot;
    public GameObject bullet;
    public ShipManager shipManager;
    public int hp;
    public float speed,rotSpeed,shootTimer,shootTime;
    public float collisionTime,collisionTimer; // so that ships can be displaced but not vibrate when close to their spot
    public bool shoot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //move towards rowspot
      if(Input.GetKeyDown(KeyCode.Space)){Die();}
      if(myspot != null){MoveToMySpot();}

      //have all ships use the same clock

      // if(shoot == true && shootTime != -1)
      // {
      //   ShootClock();
      // }
    }
    public GameObject GetBulletType()
    {
      return bullet;
    }
    public Vector2 GetForward()
    {
      return new Vector2(transform.position.x,transform.position.y - GetComponent<Collider2D>().bounds.size.y);

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
      if(Vector2.Distance(transform.position, myspot.transform.position) > 1)
      {
        //structures dont rotate but still move to position
              if(rotSpeed != 0){
              //look in the direction the ship is moving
              Vector3 diff = ( transform.position - myspot.transform.position );
               diff.Normalize();

               float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
               transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), rotSpeed * 2 * Time.deltaTime);
               GetComponent<Rigidbody2D>().AddForce(transform.up * -speed * Time.deltaTime,ForceMode2D.Impulse);
              // GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed * Time.deltaTime,ForceMode2D.Impulse);
              }else
              {
                GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed * Time.deltaTime,ForceMode2D.Impulse);
              }
      }
      else{

            if(collisionTimer > 0){

              collisionTimer -= Time.deltaTime;
              transform.rotation = Quaternion.RotateTowards(transform.rotation, myspot.transform.rotation, rotSpeed * Time.deltaTime);
            GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed *  Time.deltaTime,ForceMode2D.Impulse);
            }
            else
            {
              transform.rotation = Quaternion.RotateTowards(transform.rotation, myspot.transform.rotation, rotSpeed * Time.deltaTime);
            // GetComponent<Rigidbody2D>().AddForce((myspot.transform.position - transform.position).normalized * speed * 4 * Time.deltaTime,ForceMode2D.Impulse);
            transform.position = Vector3.MoveTowards(transform.position, myspot.transform.position, speed * Time.deltaTime);
            }


      }

      // transform.position = Vector3.MoveTowards(transform.position, myspot.transform.position, speed * Time.deltaTime);

    }
    public void OnMouseUp()
    {
      //do action
      Instantiate(bullet,new Vector2(transform.position.x,transform.position.y - (GetComponent<Collider2D>().bounds.size.y) ),transform.rotation);
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
