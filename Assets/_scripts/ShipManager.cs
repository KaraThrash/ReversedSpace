using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipManager : MonoBehaviour
{
  public GameManager gameManager;
  public List<Ship> ships;
  public float shotTimer,shotTime;
  public Transform bulletHolder,mapCenter;
  public GameObject earthShip;
  public Text bulletCountText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncrementClock()
    {
      shotTimer -= 1;
      if(shotTimer <= 0)
      {
        shotTimer = shotTime;
        AllShipsFire();
        UpdateBulletText();
      }

    }
    public void AllShipsFire()
    {
      foreach(Ship firingship in ships)
      {
        //NOTE: keep rhythm, but leave commented out for simplicity while working
        //check if the ship fires on this sequence of the count
        // always increment the count for rhythm even if there is no bullet to fire
      //  if(firingship.CheckRhythm() == true)
    //    {
          // firingship.FireBullet();
          FireBullet(firingship.transform.position - firingship.transform.up,firingship);
      //  }
      }

    }
    //take the transform and the ship so that you can have ships fire from variable directions under special circumstances (e.g. enviromental guns)
    public void FireBullet(Vector3 fromwhere,Ship firingship)
    {

      if(bulletHolder.childCount > 0 )
      {
        GameObject tempbullet = bulletHolder.GetChild(0).gameObject;
        tempbullet.transform.position = fromwhere;
        tempbullet.active = true;

        // //launch the bullet and change it's parent, then set its type based on the ship that fired it
        tempbullet.GetComponent<Bullet>().Launch(fromwhere,mapCenter,firingship.bullet.GetComponent<Bullet>());
        //then set its direction
        tempbullet.GetComponent<Bullet>().Launch(firingship.transform);
        // tempbullet.transform.rotation =

      }

    }

    public void FireBullet(Vector3 fromwhere,Transform fromtransform,Bullet bulletfired)
    {
      if(bulletHolder.childCount > 0 )
      {
        GameObject tempbullet = bulletHolder.GetChild(0).gameObject;
        tempbullet.transform.position = fromwhere;
        tempbullet.active = true;

        // //launch the bullet and change it's parent, then set its type based on the ship that fired it
        tempbullet.GetComponent<Bullet>().Launch(fromwhere,mapCenter,bulletfired);
        //then set its direction
        tempbullet.GetComponent<Bullet>().Launch(fromtransform);

      }

    }
    public void UpdateBulletText()
    {
      int count = 0;
      string tempstring = "";
      while(count < bulletHolder.childCount)
      {
        tempstring += ".";
        count ++;
      }
      bulletCountText.text = tempstring;
    }
    public void EnableShip(GameObject newship)
    {

        AddShipToList(newship.GetComponent<Ship>());
        newship.GetComponent<Ship>().Activate(earthShip);

    }

    public void AddShipToList(Ship newship)
    {
      ships.Add(newship);
      newship.shipManager = GetComponent<ShipManager>();
    }

    public void ShipDie(Ship deadship)
    {

      if(ships.Contains(deadship))
      {
         ships.Remove(deadship);
          Destroy(deadship.gameObject);
      }


    }
}
