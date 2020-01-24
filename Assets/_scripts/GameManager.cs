using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public PlayerManager playerManager;
  public ShipManager shipManager;
  public DragAndDrop shipPlacementControls;
  public WorldClock worldClock;
  public ShipList shipList;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool CheckMoneyForShip(int shiptype)
    {

        if(playerManager.CheckMoney(shipList.GetShipObject(shiptype).GetComponent<Ship>().cost) == true){return true;}
        else{return false;}
    }
}
