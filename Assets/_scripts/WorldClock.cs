using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{
  public float countdown,increment;
  public ShipManager shipManager;
  public EnemyShip enemyShip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(countdown != -1 )
      {
          countdown -= Time.deltaTime;
          if(countdown <= 0)
          {IncrementWorld();countdown = increment;}
      }
    }
    public void IncrementWorld()
    {
      if(enemyShip != null)
      {enemyShip.IncrementClock();}
        if(shipManager != null)
        {shipManager.IncrementClock();}

    }
}
