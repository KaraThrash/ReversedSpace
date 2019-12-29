using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipList : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> shipSprites;
    public List<GameObject> shipObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetShipObject(int shiptype)
    {
      if(shiptype < shipObject.Count){return shipObject[shiptype];}
      else
      {return shipObject[0];}

    }
}
