using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public float lifetime,speed;
  public int dmg,type; //type for fight pattern
  public bool alienBullet; //for who to damage
    // Start is called before the first frame update
    void Start()
    {
      if(lifetime == 0){lifetime = 10.0f;}
    }

    // Update is called once per frame
    void Update()
    {


      if(type == 0){  transform.Translate(Vector3.down * speed * Time.deltaTime);} //regular bullet, travels straight
      else if(type == 1){}

      lifetime -= Time.deltaTime;
      if(lifetime <= 0){Die();}
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
      // print("red");
      // Destroy(this.gameObject);
    }
    public void Die()
    {Destroy(this.gameObject);}
}
