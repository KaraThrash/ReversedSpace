using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rowspot : MonoBehaviour
{
  public PlayerManager playerManager;
  public bool open;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseUp()
    {
      if(open == true){

      playerManager.PlaceShip(this.transform);
      open = false;
      }
    }

}
