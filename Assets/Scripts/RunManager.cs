using QxFramework.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunManager : MonoBehaviour
{

    public PlayerManager playerManager;
    public MapManager mapManager;
    public CardManager cardManager;
    public BattleManager battleManager;
    public int curFloor=0;
    public string curFloorStr
    {
        get
        {
            if (curFloor == 0)
                return "Zero";
            else if (curFloor == 1)
                return "First";
            else if (curFloor == 2)
                return "Second";
            else if (curFloor == 3)
                return "Third";
            else if (curFloor == 4)
                return "Forth";
            else return "More Than Forth";
        }
    }
    public string bossID="";

    public void Init(List<string>playerList)
    {
        cardManager= new CardManager();
        cardManager.Init();
        cardManager.SetRunManager(this);

        playerManager=new PlayerManager();
        playerManager.SetRunManager(this);
        playerManager.Init(playerList);

        mapManager= new MapManager();
        mapManager.Init();
        mapManager.SetRunManager(this);

        battleManager=new BattleManager();
        battleManager.SetRunManager(this) ;
        battleManager.Init();
        

    }

    public void OnEnterFloor()
    {
        curFloor++;
        Randomer rnd = new Randomer();
        TableAgent tab = new TableAgent();
        tab.Add(ResourceManager.Instance.Load<TextAsset>("Text/Table/EnemyComb").text);
        int range=1;
        while (tab.GetString("EnemyComb", range.ToString(), $"{curFloorStr}Boss") != "#End")
        {
            range++;
        }

        int groupID = rnd.nextInt(1, range + 1);
        bossID = tab.GetString("EnemyComb", groupID.ToString(), $"{curFloorStr}Boss");



    }



    public void OpenMap()
    {
        if (UIManager.Instance.FindUI("MapUI"))
        {
            UIManager.Instance.Close("MapUI");
        }
        UIManager.Instance.Open("MapUI",2,"",mapManager.Map);
    }

    public void EndRun()
    {

    }



}
