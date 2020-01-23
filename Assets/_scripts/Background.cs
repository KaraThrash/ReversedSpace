using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
  public List<GameObject> groundPieces;
  public GameObject grounda,groundb;
  public int scrollCount;
  public float scrollspeed,ystart,ybottom;
    // Start is called before the first frame update
    void Start()
    {
      ystart = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
      grounda.transform.Translate(Vector3.down * scrollspeed * Time.deltaTime);
      if(grounda.transform.position.y < ybottom)
      {
        scrollCount++;
        if( scrollCount >= groundPieces.Count){scrollCount = 0;}
        grounda = groundb;
        groundb = groundPieces[scrollCount];
        groundb.transform.position = new Vector3(groundb.transform.position.x,ystart,groundb.transform.position.z);
      }
      groundb.transform.Translate(Vector3.down * scrollspeed * Time.deltaTime);
    }
}
