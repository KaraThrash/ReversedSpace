using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragAndDrop : MonoBehaviour
{
  public PlayerManager playerManager;
  public int shiptype = -1;
  public bool midclick = false;
  public GameObject highlightedImage,currentlySelectedImage;
  public List<GameObject> shipSprites;
  public Vector3 worldPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(midclick == true)
      {
        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentlySelectedImage.transform.position = new Vector3(worldPos.x,worldPos.y,0);
        if(Input.GetMouseButtonDown(0))
           {
             //use child colliders to set legal placement dynamicly
        useraycast();
          }
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
                 playerManager.EnableShip(currentlySelectedImage,new Vector3(worldPos.x,worldPos.y,0));
                 currentlySelectedImage = null;
                 // currentlySelectedImage = Instantiate(highlightedImage,Input.mousePosition,transform.rotation);
               }
             }




       }
    public void SelectShipFromButton(int newshiptype)
    {
      if(currentlySelectedImage != null){Destroy(currentlySelectedImage);}
      if(newshiptype == -1){
        midclick = false;
        shiptype = newshiptype;

      }
      else{

          midclick = true;
          shiptype = newshiptype;
          worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          if(shipSprites.Count >= shiptype )
          currentlySelectedImage = Instantiate(shipSprites[shiptype],worldPos,transform.rotation);
      }
    }
    public void OnMouseDown()
    {



    }

}
