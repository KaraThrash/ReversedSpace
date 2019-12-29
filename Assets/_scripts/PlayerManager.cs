using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public ShipList shiplist;
  public int money,moneyIncrement;// current money, and how much the money increases at a time interval
  public float moneyTimer,moneyTime;
  public int shipNumberSelected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoneyClock()
    {
      moneyTimer -= Time.deltaTime;
      if(moneyTimer <= 0)
      {
        money += moneyIncrement;
        moneyTimer = moneyTime;
      }

    }
    public void PlaceShip(Transform placeInRow)
    {

      //check if ship type selected avilable // default to raider

      //check for money
      if(money >= shipNumberSelected)
      {
        //deduct money
        money -= shipNumberSelected;
        GameObject clone = Instantiate(shiplist.GetShipObject(shipNumberSelected),placeInRow.transform.position,placeInRow.transform.rotation);
        clone.transform.parent = placeInRow;
        clone.GetComponent<Ship>().myspot = placeInRow.GetComponent<Rowspot>();
      }

      //spawn ship shipNumberSelected from ShipList
      //set new ship as child of placeInRow

    }
}
