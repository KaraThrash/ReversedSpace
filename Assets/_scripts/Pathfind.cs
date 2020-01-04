using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour
{
  public Transform activeBullets;
  public GameObject earthShip;
  public List<int> bestPath;
  public int[][] maze = new int[10][] {new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10],new int[10] };
public int pathmaxlength = 5,mapxlength = 10;
  // public int[][] maze = new int[10][10];
  // jaggedArray[0] = new int[] { 1, 3, 5, 7, 9 };
    // Start is called before the first frame update
    void Start()
    {
      // foreach(int[] el in maze)
      // print(el[0] + "|" + el[1] + el[2] + "|" + el[3] + el[5] + "|" + el[6]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<int> GetPath()
    {
      GetBulletLocations();
      int rowcount = 0;
      int colcount = 0;
      string tempstring = "";

      bestPath.Clear();
      int earthxpos = (int)Mathf.Floor(earthShip.transform.localPosition.x);
      int count = 0;
      int count2 = 0;
      int nextspace = 5;
      if(earthxpos == 5 ){nextspace = 1;}
      int distancetonextspot = 99;






        //NOTE: this checks one row for distance from ship
                while (count < pathmaxlength)
                {
                  count2 = 0;
                  while(count2 < mapxlength )
                  {
                        if( maze[count][count2] == 0 && maze[count + 1][count2] == 0  && (Mathf.Abs(count2 - earthxpos) < distancetonextspot ||( Mathf.Abs(count2 - earthxpos) == 1 && distancetonextspot == 0)) )
                        {
                          nextspace = count2;
                          distancetonextspot = Mathf.Abs(nextspace - earthxpos);
                        }
                    count2++;
                  }
                  bestPath.Add(nextspace);
                  count++;
                  distancetonextspot = 99;
                    if(nextspace == 0 ){nextspace = 3;}else{nextspace = 6;}
                }




          //
          //
          //
          //
          //NOTE: this just checks adjacent
          // while (count < pathmaxlength)
          // {
          //   if( earthxpos < mapxlength - 1 )
          //   {
          //         if( maze[count][earthxpos + 1] == 0 )
          //         {bestPath.Add(earthxpos + 1);}
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
          return bestPath;
    }
    public void GetBulletLocations()
    {//reset maze
      int rowcount = 0;
      int colcount = 0;
      while (rowcount < 10)
      {
        colcount = 0;

        while (colcount < 10)
        {
          maze[rowcount][colcount] = 0;
          colcount++;
        }

        rowcount++;
      }
        foreach(Transform el in activeBullets)
        {
          //check that it is in the array parameters 10x10
                if(el.localPosition.x >= 0 && el.localPosition.x <= 9 && el.localPosition.y >= 0 && el.localPosition.y <=9)
                {
                          int count = 0;
                          int futurespaces = ((int)Mathf.Round(el.localPosition.y) ) ;
                          int xpos = ((int)Mathf.Round(el.localPosition.x)) ;

                          //go through count and check where the bullet will be based on its Speed
                          //0 is current location, 1 is one second away etc
                          //makes a maze of the predicted locations since the earth ship only moves left/right

                          maze[futurespaces][xpos] = 1;
                          // while(count < 10)
                          // {
                          //
                          //   if( futurespaces  == 0) {
                          //     if(maze[count][xpos] == 0){  maze[count][xpos] = 1;}
                          //
                          //
                          //   }
                          //   count++;
                          //   futurespaces = futurespaces - (int)el.GetComponent<Bullet>().speed;
                          // }
                }
        }
    }
}
