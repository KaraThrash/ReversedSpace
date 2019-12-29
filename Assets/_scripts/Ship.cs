using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rowspot myspot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //move towards rowspot
      if(Input.GetKeyDown(KeyCode.Space)){Die();}
    }
    public void OnMouseUp()
    {
      //do action
    }
    public void Die()
    {

      //set row spot as open
        myspot.open = true;
        Destroy(this.gameObject);
    }
}
