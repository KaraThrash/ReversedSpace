using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public Transform bulletHolder;
  public float lifetime,myLifeTime,speed,hortSpeed;
  public float temploc,curveAmplitude; //temploc is the variable for the middle of the curve
  public int dmg,type; //type for fight pattern
  public bool alienBullet,quedfordestruction; //for who to damage //if the building that made it is destroyed, remove the bullet from circulation
  public GameObject explosion;
  public Vector3 direction;
  private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      if(bulletHolder == null)
      {bulletHolder = GameObject.Find("BulletHolder").transform;}
      if(myLifeTime == 0){myLifeTime = 10.0f;}
      if(type == 1 || type == 2){temploc = transform.position.x;}
    }
    public void ResetToNeutral()
    {
      if(bulletHolder == null)
      {bulletHolder = GameObject.Find("ActiveBullets").transform;}
      if(bulletHolder != null)
      {transform.parent = bulletHolder.transform;}
      lifetime = -1;
      if(  GetComponent<Collider2D>() != null){GetComponent<Collider2D>().enabled = false;}

      this.gameObject.active = false;
    }
    // Update is called once per frame
    void Update()
    {
      if(lifetime != -1)
      {
            BulletTypes();


            lifetime -= Time.deltaTime;
            if(lifetime <= 0){Die();}
      }
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
    public void OnTriggerEnter2D(Collider2D col)
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
    public void Launch(Vector2 newpos,Transform newparent,Bullet newtype)
    {
      type = newtype.type;
      speed = newtype.speed;
      lifetime = newtype.myLifeTime;
      transform.parent = newparent;
      transform.position = newpos;

    }
    public void Launch(Transform from)
    {
      if(  GetComponent<Animator>() != null){GetComponent<Animator>().SetFloat("bullettype",type);}
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
          ResetToNeutral();

        }
    }
}
