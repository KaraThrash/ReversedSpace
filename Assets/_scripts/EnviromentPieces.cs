using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentPieces : MonoBehaviour
{
  public GameManager gameManager;
  public int type;
  public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
      ActivateTrap();
    }
    public void ActivateTrap()
    {
      switch (type)
      {
        case 0:
        gameManager.shipManager.FireBullet(transform.position - transform.up,this.transform,bullet);
        break;
        default:
        break;
      }
    }
}
