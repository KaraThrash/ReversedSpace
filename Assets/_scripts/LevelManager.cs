using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public GameManager gameManager;
  public int leveltype;
  public int enemyDmgGoal,moneyGainedGoal,totalMoneyGoal,playerHealth;
  public int currentenemyDmgGoal,currentmoneyGainedGoal,currenttotalMoneyGoal,currentplayerHealth;
  public float levelTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCurrentLevelProgress(int moneyGained,int currentMoney,int enemydmg,int playerdmg)
    {

        if(moneyGainedGoal != -1)
        {
          currentmoneyGainedGoal += moneyGained;
        }
        if(totalMoneyGoal != -1)
        {
          currenttotalMoneyGoal = currentMoney;
        }
        if(enemyDmgGoal != -1)
        {
          currentenemyDmgGoal += enemydmg;
        }
        if(playerHealth != -1)
        {
          currentplayerHealth += playerdmg;
        }
    }
    public void UpdateCurrentLevelProgressMoney(int moneyGained,int currentMoney)
    {

        if(moneyGainedGoal != -1)
        {
          currentmoneyGainedGoal += moneyGained;
        }
        if(totalMoneyGoal != -1)
        {
          currenttotalMoneyGoal = currentMoney;
        }

    }
    public void UpdateCurrentLevelProgressEnemyDmg(int enemydmg)
    {


        if(enemyDmgGoal != -1)
        {
          currentenemyDmgGoal += enemydmg;
        }

    }
    public void UpdateCurrentLevelProgressPlayerDmg(int playerdmg)
    {

        if(playerHealth != -1)
        {
          currentplayerHealth += playerdmg;
        }
    }
    public void SetLevelParameters()
    {

      switch(leveltype)
      {
        case 0:

        break;
        default:
        break;
      }

    }

}
