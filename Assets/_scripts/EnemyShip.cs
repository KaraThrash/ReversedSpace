﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
  public int hp,maxDistanceFromCenter,direction;
  public float speed,shootTimer,shootTime;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      transform.Translate(transform.right * speed * direction * Time.deltaTime);
      if(Random.Range(0, 150) == 0){direction = -direction;}
     CheckEdge();

       ShootClock();

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
    public void OnTriggerEnter2D(Collider2D col)
    {
      if(col.transform.GetComponent<Bullet>() != null  )
      {
      //  print("bullet hit enemy ship");
      //  hp --;
      }

    }

}
