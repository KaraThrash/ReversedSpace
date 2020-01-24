using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragAndDrop : MonoBehaviour
{
  public GameManager gameManager;
  public PlayerManager playerManager;
  public int shiptype = -1;
  public bool midclick = false;
  public GameObject highlightedImage,currentlySelectedShip;
  public List<GameObject> shipSprites;
  public List<Button> purchaseButtons;
  public Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {
UpdateShipButtons(0);
    }

    // Update is called once per frame
    void Update()
    {
      if(midclick == true)
      {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentlySelectedShip.transform.position = new Vector3(worldPos.x,worldPos.y,0);
        if(Input.GetMouseButtonDown(0))
           {
             //use child colliders to set legal placement dynamicly
        useraycast();
          }
      }
    }
    public void UpdateShipButtons(int currentMoney)
    {
      int count = 0;
      while(count < purchaseButtons.Count)
      {
        if(gameManager.CheckMoneyForShip(count) == true )
        {
           purchaseButtons[count].interactable = true;
        }
          else
          {
             purchaseButtons[count].interactable = false;
          }
        count++;
      }
    }
    public void useraycast()
    {

             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit))
             {
               if( hit.transform.parent == this.transform)
               {
                 midclick = false;
                 playerManager.EnableShip(currentlySelectedShip,new Vector3(worldPos.x,worldPos.y,0));
                 currentlySelectedShip = null;
                 // currentlySelectedShip = Instantiate(highlightedImage,Input.mousePosition,transform.rotation);
               }
             }




       }
    public void SelectShipFromButton(int newshiptype)
    {
      if(currentlySelectedShip != null){Destroy(currentlySelectedShip);}

      if(newshiptype == -1){
        midclick = false;
        shiptype = newshiptype;

      }
      else{


          if(shipSprites.Count >= newshiptype  && gameManager.CheckMoneyForShip(newshiptype) == true)
          {
            midclick = true;
            shiptype = newshiptype;
            worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
              currentlySelectedShip = Instantiate(gameManager.shipList.GetShipObject(shiptype),worldPos,transform.rotation);
          }

      }
    }
    public void OnMouseDown()
    {



    }

}
