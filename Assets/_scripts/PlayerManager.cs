using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
  public ShipList shiplist;
  public int money,moneyIncrement;// current money, and how much the money increases at a time interval
  public float moneyTimer,moneyTime;
  public int shipNumberSelected;
  public Text moneyText;
    // Start is called before the first frame update
    void Start()
    {
      moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
      MoneyClock();
    }

    public void MoneyClock()
    {
      moneyTimer -= Time.deltaTime;
      if(moneyTimer <= 0)
      {
        money += moneyIncrement;
        moneyTimer = moneyTime;
        moneyText.text = money.ToString();
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
          moneyText.text = money.ToString();
        GameObject clone = Instantiate(shiplist.GetShipObject(shipNumberSelected),transform.position,placeInRow.transform.rotation);
        // clone.transform.parent = placeInRow;
        placeInRow.GetComponent<Rowspot>().myShip = clone.GetComponent<Ship>();
        clone.GetComponent<Ship>().myspot = placeInRow.GetComponent<Rowspot>();
      }

      //spawn ship shipNumberSelected from ShipList
      //set new ship as child of placeInRow

    }
    public void SelectShip(int shipNumber)
    {
      //button press to select

      shipNumberSelected = shipNumber;
    }

}
