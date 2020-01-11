using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
  public GameObject pathIndicator,mapsquareblack,mapsquarewhite,mapsquare;
  public Transform activeBullets;
  public GameObject earthShip;
  public List<int> bestPath,algobestPath;
  public int[][] maze = new int[10][] {new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10] };
  public GameObject[][] mapsquares = new GameObject[][] { };
public int pathmaxlength = 5,mapxlength = 10,mapylength = 10;
  // public int[][] maze = new int[10][10];
  // jaggedArray[0] = new int[] { 1, 3, 5, 7, 9 };
    // Start is called before the first frame update
    void Start()
    {
      //NOTE: debug to visually see the path finding logic. Leave in for future interations.
        // MakeVisualMap();
    }

    //debug to visually see the path finding logic. Leave in for future interations.
    public void MakeVisualMap()
    {
      mapsquares = new GameObject[10][] ;
     int rowcount = 0;
     int colcount = 0;
     while (rowcount < 10)
     {
       mapsquares[rowcount] = new GameObject[10];
       colcount = 0;
       if(mapsquare == mapsquareblack){mapsquare = mapsquarewhite;}else{mapsquare = mapsquareblack;}
       while (colcount < 10)
       {
         GameObject clone = Instantiate(mapsquare,new Vector3(transform.position.x + colcount,transform.position.y + rowcount,transform.position.x - 2),transform.rotation) as GameObject;
         mapsquares[rowcount][colcount] = clone;
         colcount++;
         if(mapsquare == mapsquareblack){mapsquare = mapsquarewhite;}else{mapsquare = mapsquareblack;}
       }

       rowcount++;
     }

    }
    // Update is called once per frame
    void Update()
    {

    }



    public List<int> GetPath()
    {
      //update the maze with the locations of the bullets
      GetBulletLocations();
      //set iterators to zero
      int rowcount = 0;
      int colcount = 0;

      //clear the previous best path if it wasnt already cleared.
      bestPath.Clear();
      //set the start of the path search at the ships current location
      int earthxpos = (int)Mathf.Floor(earthShip.transform.localPosition.x);
      int count = 0;
      int count2 = 0;
      int nextspace = 5;
      if(earthxpos == 5 ){nextspace = 1;}
      int distancetonextspot = 99;


        //NOTE: checks when bullets occupy the row the Earth ship is on // when they would hit it if its in that square
        //y,x array for map layout
                while (count < pathmaxlength)
                {
                  //count tracks length of the path to find
                  //count2 checks the earth ship row to determine distance from its current location
                  count2 = 1;
                  while(count2 < mapxlength - 2)
                  {
                    if( maze[count][count2] == 0 && maze[count + 1][count2] == 0  && (Mathf.Abs(count2 - earthxpos) < distancetonextspot ||( Mathf.Abs(count2 - earthxpos) == 1 && distancetonextspot == 0)) )
                        // if( maze[count][count2] == 0 && maze[count + 1][count2] == 0  && (Mathf.Abs(count2 - earthxpos) < distancetonextspot ||( Mathf.Abs(count2 - earthxpos) == 1 && distancetonextspot == 0)) )
                        {
                          nextspace = count2;
                          distancetonextspot = Mathf.Abs(nextspace - earthxpos);
                        }
                    count2++;
                  }
                  //put a mark on the screen to see the path
                  Instantiate(pathIndicator, new Vector2(transform.position.x + nextspace,transform.position.y + count),transform.rotation);
                  bestPath.Add(nextspace);
                  count++;
                  distancetonextspot = 99;

                  //so the earth ship is more likely to move set the next space to somewhere close to the middle
                  if(nextspace == 1 ){nextspace = 3;}else{nextspace = 6;}
                }
      return bestPath;



    }


    public void GetBulletLocations()
    {

      //reset maze
      int rowcount = 0;
      int colcount = 0;
      while (rowcount < mapylength)
      {
        colcount = 0;

        while (colcount < mapxlength)
        {
            // mapsquares[rowcount][colcount].active = true;
          maze[rowcount][colcount] = 0;
          colcount++;
        }

        rowcount++;
      }
        foreach(Transform el in activeBullets)
        {
          //check that it is in the array parameters 10x10
                if(el.localPosition.x >= 0 && el.localPosition.x < mapxlength && el.localPosition.y >= 0 && el.localPosition.y < mapylength)
                {
                          int count = 0;
                          int futurespaces = ((int)Mathf.Floor(el.localPosition.y) );
                          int xpos = ((int)Mathf.Round(el.localPosition.x)) ;

                          //go through count and check where the bullet will be based on its Speed
                          //0 is current location, 1 is one second away etc
                          //makes a maze of the predicted locations since the earth ship only moves left/right
                          while(count < mapylength)
                          {

                            if( futurespaces  >= 0 && futurespaces < 1) {

                                maze[count][xpos] = 1;

                                // mapsquares[count][xpos].active = false;

                            }
                            count++;
                            futurespaces = futurespaces - (int)el.GetComponent<Bullet>().speed;
                          }
                }
        }
    }
}
