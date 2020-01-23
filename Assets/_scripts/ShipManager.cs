using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
  public List<Ship> ships;
  public float shotTimer,shotTime;
  public Transform bulletHolder,mapCenter;
  public GameObject earthShip;
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

      }

    }
    public void AllShipsFire()
    {
      foreach(Ship firingship in ships)
      {
        //check if the ship fires on this sequence of the count
        // always increment the count for rhythm even if there is no bullet to fire
      //  if(firingship.CheckRhythm() == true)
    //    {
          // firingship.FireBullet();
          FireBullet(firingship.transform.position + firingship.transform.forward,firingship);
      //  }
      }

    }
    public void FireBullet(Vector2 fromwhere,Ship firingship)
    {
      GameObject tempbullet = Instantiate(firingship.GetBulletType(),firingship.transform.position - firingship.transform.up,transform.rotation);
      tempbullet.GetComponent<Bullet>().Launch(firingship.transform);
      tempbullet.transform.parent = mapCenter;
      // if(bulletHolder.childCount > 0 )
      // {
      //   // GameObject tempbullet = bulletHolder.GetChild(0).gameObject;
      //   // tempbullet.active = true;
      //   // //launch the bullet and change it's parent
      //   tempbullet = Instantiate(firingship.GetBulletType(),firingship.GetForward(),transform.rotation);
      //   tempbullet.GetComponent<Bullet>().Launch(fromwhere,mapCenter);
      //   // tempbullet.transform.rotation =
      //
      // }
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
