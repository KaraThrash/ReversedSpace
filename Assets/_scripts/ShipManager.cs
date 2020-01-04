using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
  public List<Ship> ships;
  public float shotTimer,shotTime;
  public Transform bulletHolder,mapCenter;
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
        if(bulletHolder.childCount > 0)
        {
          GameObject tempbullet = bulletHolder.GetChild(0).gameObject;
          tempbullet.active = true;
          //launch the bullet and c hange it's parent
          tempbullet.GetComponent<Bullet>().Launch(firingship.GetForward(),mapCenter);
          // Instantiate(firingship.GetBulletType(),firingship.GetForward(),transform.rotation);
        }

      }

    }

    public void AddShipToList(Ship newship)
    {
      ships.Add(newship);
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
