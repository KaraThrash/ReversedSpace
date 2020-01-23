using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public Transform bulletHolder;
  public float lifetime,speed,hortSpeed;
  public float temploc,curveAmplitude; //temploc is the variable for the middle of the curve
  public int dmg,type; //type for fight pattern
  public bool alienBullet,quedfordestruction; //for who to damage //if the building that made it is destroyed, remove the bullet from circulation
  public GameObject explosion;
  public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
      if(bulletHolder == null)
      {bulletHolder = GameObject.Find("ActiveBullets").transform;}
      if(lifetime == 0){lifetime = 10.0f;}
      if(type == 1 || type == 2){temploc = transform.position.x;}
    }

    // Update is called once per frame
    void Update()
    {

      BulletTypes();


      lifetime -= Time.deltaTime;
      if(lifetime <= 0){Die();}
    }
    public void BulletTypes()
    {
      if(type == 0){
        GetComponent<Rigidbody2D>().velocity = direction * speed;}
        // transform.Translate(direction * speed * Time.deltaTime);}
         //regular bullet, travels straight
      //moves in a sinewave
       else if(type == 1){
        if( transform.position.x - temploc < -curveAmplitude)
        {hortSpeed = speed;}
        if( transform.position.x - temploc > curveAmplitude)
        {hortSpeed = -speed;}


        GetComponent<Rigidbody2D>().AddForce(Vector2.right * hortSpeed *  Time.deltaTime,ForceMode2D.Impulse);
      }else if(type == 2){

        if( transform.position.x - temploc < -curveAmplitude)
        {hortSpeed = speed;temploc = transform.position.x + curveAmplitude;}
        if( transform.position.x - temploc > curveAmplitude)
        {hortSpeed = -speed;temploc = transform.position.x - curveAmplitude;}


        GetComponent<Rigidbody2D>().AddForce(Vector2.right * hortSpeed *  Time.deltaTime,ForceMode2D.Impulse);
      }

    }
    public void OnCollisionEnter2D(Collision2D col)
    {

      if(alienBullet == true  )
      {
        if( col.transform.GetComponent<EnemyShip>() != null)
        {col.transform.GetComponent<EnemyShip>().TakeDamage(dmg);
          Die();}
          else{
            if( col.transform.GetComponent<Ship>() != null)
            {
              Die();
              //delete when hitting friendly
            //  Destroy(this.gameObject);
            }
          }

      }
      //non friendly bullets destroy each other
      if(col.transform.GetComponent<Bullet>() != null && col.transform.GetComponent<Bullet>().alienBullet != alienBullet)
      {Die();}
      if(alienBullet == false && col.transform.GetComponent<Ship>() != null)
      {
        col.transform.GetComponent<Ship>().TakeDamage(dmg);
          Die();
      }
    }
    public void Launch(Vector2 newpos,Transform newparent)
    {
      transform.parent = newparent;
      lifetime = 10.0f;
      transform.position = newpos;

    }
    public void Launch(Transform from)
    {
      direction = (from.position - transform.position).normalized;

    }
    public void Die()
    {

      Instantiate(explosion,transform.position,transform.rotation);
      //if this bullets creator is destroyed
        if(quedfordestruction == true || alienBullet == false){
          Destroy(this.gameObject);
        }
        else
        {
            transform.parent = bulletHolder.transform;
            lifetime = 10.0f;
            this.gameObject.active = false;
        }
    }
}
