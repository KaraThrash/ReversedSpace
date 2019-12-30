using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rowspot : MonoBehaviour
{
  public PlayerManager playerManager;
  public Toggle shootToggle;
  public Rowspot rowParent;
  public Ship myShip;
  public float speed,direction,maxDistanceFromCenter;
  public float maxSpeed;
  public bool open;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      transform.Translate(transform.right * speed * direction * Time.deltaTime);
    //for rows with no ships, check that it stays in bounds
     CheckEdge();

    }
    public void CheckEdge()
    {
        //zero speed means it is a square in a row

        if(speed != 0)
        {
            if(transform.localPosition.x < -maxDistanceFromCenter){direction = 1;}
            if(transform.localPosition.x > maxDistanceFromCenter){direction = -1;}
        }

    }
    //speed change is positive or negative
    //adjusts speed for the row, to a min of zero and a max set by the maxspeed variable
    public void ChangeSpeed(float speedChange)
    {
      speed += speedChange;
      if(speed <= 0){speed = 0;}
      if(speed > maxSpeed){speed = maxSpeed;}

    }
    public void ToggleShoot(bool onOrOff)
    {
      print(onOrOff);
      foreach (Transform go in transform)
      {
        if(go.childCount > 1)
        { if(go.GetComponent<Rowspot>() != null && go.GetComponent<Rowspot>().myShip != null)
            { go.GetComponent<Rowspot>().myShip.GetComponent<Ship>().ToggleShoot(shootToggle.isOn); }
        }
      }
    }
    public void ShipHitScreenEdge(float leftOrRight)
    {


      direction = -Mathf.Sign(leftOrRight);

    }
    public void OnMouseUp()
    {
      if(open == true){

      playerManager.PlaceShip(this.transform);
      open = false;
      }
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
      if(col.gameObject.tag == "border" && speed == 0)
      {

              if(open == false && rowParent != null)
              {
                //check that the x position is inside the play area.
                //if(Mathf.Abs(transform.position.x) > Mathf.Abs(maxDistanceFromCenter) ){  rowParent.ShipHitScreenEdge(transform.position.x);}
                rowParent.ShipHitScreenEdge(col.transform.localPosition.x);

              }
        }
    }
}
