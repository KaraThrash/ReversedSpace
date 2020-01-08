﻿using System.Collections;
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
public int pathmaxlength = 5,mapxlength = 9,mapylength = 9;
  // public int[][] maze = new int[10][10];
  // jaggedArray[0] = new int[] { 1, 3, 5, 7, 9 };
    // Start is called before the first frame update
    void Start()
    {
      // foreach(int[] el in maze)
      // print(el[0] + "|" + el[1] + el[2] + "|" + el[3] + el[5] + "|" + el[6]);
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
                    if(nextspace == 1 ){nextspace = 3;}else{nextspace = 6;}
                }
      return bestPath;


                // List<List<int>> possiblepaths = new List<List<int>>();
                // //set where the ship currently is to invalid
                // maze[count][earthxpos] = 1;
                // while (count < pathmaxlength)
                // {
                //   if( earthxpos < mapxlength - 1 )
                //   {
                //         if( maze[count][earthxpos + 1] == 0 )
                //         {
                //           // possiblepaths.Add(CheckinRow(earthxpos + 1,count));
                //           bestPath.Add(earthxpos + 1);
                //
                //         }
                //         else if(earthxpos > 0 && maze[count][earthxpos - 1] == 0 ){bestPath.Add(earthxpos - 1 );}
                //
                //         else if( maze[count][earthxpos ] == 0 )
                //         {bestPath.Add(earthxpos );}
                //
                //           else{bestPath.Add(5);}
                //
                //   }else
                //   {
                //
                //     //the ship is at the max position, furthest right
                //      if( maze[count][earthxpos -1 ] == 0 )
                //     {bestPath.Add(earthxpos - 1 );}
                //     else{
                //       bestPath.Add(earthxpos  );
                //
                //     }
                //   }
                //   // maze[count][bestPath[bestPath.Count - 1] ] = (maze[count][bestPath[bestPath.Count - 1] ] * 10) + 2;
                //   earthxpos = bestPath[bestPath.Count - 1];
                //   count++;
                // }



          //
          //
          //
          //



          //NOTE: this just checks adjacent
          bestPath.Clear();
          // while (count < pathmaxlength)
          // {
          //   if( earthxpos < mapxlength - 1 )
          //   {
          //         if( maze[count][earthxpos + 1] == 0 && maze[count + 1][earthxpos + 1] == 0)
          //         {bestPath.Add(earthxpos + 1);}
          //         else if(earthxpos > 0 && maze[count][earthxpos - 1] == 0 && maze[count + 1][earthxpos - 1] == 0){bestPath.Add(earthxpos - 1 );}
          //
          //         else if( maze[count + 1][earthxpos ] == 0 )
          //         {bestPath.Add(earthxpos );}
          //
          //           else{bestPath.Add(5);}
          //
          //   }else
          //   {
          //
          //     //the ship is at the max position, furthest right
          //      if( maze[count][earthxpos -1 ] == 0 && maze[count + 1][earthxpos - 1] == 0)
          //     {bestPath.Add(earthxpos - 1 );}
          //     else{
          //       bestPath.Add(earthxpos  );
          //
          //     }
          //   }
          //   // maze[count][bestPath[bestPath.Count - 1] ] = (maze[count][bestPath[bestPath.Count - 1] ] * 10) + 2;
          //   earthxpos = bestPath[bestPath.Count - 1];
          //   count++;
          // }
          return bestPath;



          // while (rowcount < 10)
          // {
          //   colcount = 0;
          //
          //   while (colcount < 10)
          //   {
          //     tempstring = tempstring +  maze[rowcount][colcount];
          //     // tempstring.Append("" + maze[rowcount][colcount]);
          //     colcount++;
          //   }
          //   tempstring = tempstring + "\n";
          //
          //   rowcount++;
          // }
          // print(tempstring);
          // return bestPath;
    }


    public void GetBulletLocations()
    {//reset maze
      int rowcount = 0;
      int colcount = 0;
      while (rowcount < 9)
      {
        colcount = 0;

        while (colcount < 9)
        {
            mapsquares[rowcount][colcount].active = true;
          maze[rowcount][colcount] = 0;
          colcount++;
        }

        rowcount++;
      }
        foreach(Transform el in activeBullets)
        {
          //check that it is in the array parameters 10x10
                if(el.localPosition.x >= 0 && el.localPosition.x <= mapxlength && el.localPosition.y >= 0 && el.localPosition.y <= mapylength)
                {
                          int count = 0;
                          int futurespaces = ((int)Mathf.Floor(el.localPosition.y) );
                          int xpos = ((int)Mathf.Round(el.localPosition.x)) ;

                          //go through count and check where the bullet will be based on its Speed
                          //0 is current location, 1 is one second away etc
                          //makes a maze of the predicted locations since the earth ship only moves left/right
                          // mapsquares[futurespaces][xpos].active = false;
                          // maze[futurespaces][xpos] = 1;
                          while(count < mapylength)
                          {

                            if( futurespaces  == 0) {
                              if(maze[count][xpos] == 0){  maze[count][xpos] = 1;}
                                mapsquares[count][xpos].active = false;

                            }
                            count++;
                            futurespaces = futurespaces - (int)el.GetComponent<Bullet>().speed;
                          }
                }
        }
    }
}
