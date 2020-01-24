using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
  public List<GameObject> groundPieces;
  public GameObject earthShip,grounda,groundb,backgrounda,backgroundb;
  public int scrollCount;
  public float scrollspeed,parralaxspeed,currentparralaxspeed,ystart,ybottom;
    // Start is called before the first frame update
    void Start()
    {
      ystart = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

      foreground();
      Parralax();
    }

    public void foreground()
    {

      if(grounda.transform.position.y < ybottom)
      {
          scrollCount++;
          if( scrollCount >= groundPieces.Count){scrollCount = 0;}
          grounda = groundb;
          groundb = groundPieces[scrollCount];
          groundb.transform.position = new Vector3(groundb.transform.position.x,ystart,groundb.transform.position.z);
      }
      grounda.transform.Translate(Vector3.down * scrollspeed * Time.deltaTime);
      groundb.transform.Translate(Vector3.down * scrollspeed * Time.deltaTime);

    }
    public void Parralax()
    {
      currentparralaxspeed = Mathf.Lerp(currentparralaxspeed,parralaxspeed * Mathf.Sign(earthShip.transform.position.x - backgrounda.transform.position.x),Time.deltaTime * 0.5f);
      if(backgrounda.transform.position.y < ybottom)
      {

        backgrounda.transform.position = new Vector3(backgrounda.transform.position.x,ystart,backgrounda.transform.position.z);
      }
      if(backgroundb.transform.position.y < ybottom)
      {

        backgroundb.transform.position = new Vector3(backgroundb.transform.position.x,ystart,backgroundb.transform.position.z);
      }

      backgrounda.transform.Translate(Vector3.down * parralaxspeed * Time.deltaTime);
      backgroundb.transform.Translate(Vector3.down * parralaxspeed * Time.deltaTime);
      // if(earthShip.transform.position.x > backgrounda.transform.position.x)
      // {
        backgrounda.transform.Translate(Vector3.right * currentparralaxspeed * Time.deltaTime);
        backgroundb.transform.Translate(Vector3.right * currentparralaxspeed * Time.deltaTime);
      // }
        // else
        // {
        //   backgrounda.transform.Translate(Vector3.right * -currentparralaxspeed * Time.deltaTime);
        //   backgroundb.transform.Translate(Vector3.right * -currentparralaxspeed * Time.deltaTime);
        // }

    }
}
