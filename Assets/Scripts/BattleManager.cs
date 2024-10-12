using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleManager 
{
    private RunManager runMgr;
    private string enemyCombPath="Text/Table/EnemyComb";

    public List<PlayableEntity> playerTeam;
    public List<EnemyEntity> enemyTeam;
    public int CurTurn;
    public int CurFloor;
    public int TotalTime;
    public BattleSceneUI sceneUI;

    public List<CardBase> oriDrawDeck;
    public List<CardBase> curDrawCards;
    public List<CardBase> curDiscardCards;
    public List<CardBase> curCards;

    public void Init()
    {
        playerTeam = runMgr.playerManager.PlayerTeam;
    }

    public void SetRunManager(RunManager run)
    {
        runMgr = run;
    }
    public void EnterBattle(bool isElite)
    {
        CurTurn = 1;
        GenerateEnemy(isElite);
        sceneUI= UIManager.Instance.Open("BattleSceneUI").GetComponent<BattleSceneUI>();
        
    }

    public void DrawCards()
    {
        if (curDrawCards.Count >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                curCards.Add(curDrawCards[0]);
                curDrawCards.RemoveAt(0);
            }
        }
        else
        {
            int remain=curDrawCards.Count;
            for (int i = 0; i < remain; i++)
            {
                curCards.Add(curDrawCards[0]);
                curDrawCards.RemoveAt(0);
            }
            foreach(CardBase card in curDiscardCards)
            {
                curDrawCards.Add(card);
            }
            curDiscardCards.Clear();
            curDrawCards.Shuffle();
            for (int i = 0; i < 5-remain+1; i++)
            {
                curCards.Add(curDrawCards[0]);
                curDrawCards.RemoveAt(0);
            }
        }
    }
    public void GenerateEnemy(bool isElite)
    {
        enemyTeam = new List<EnemyEntity>();
        Randomer rnd = new Randomer();
        TableAgent tab=new TableAgent();
        tab.Add(ResourceManager.Instance.Load<TextAsset>(enemyCombPath).text);
        tab.Add(ResourceManager.Instance.Load<TextAsset>("Text/Table/Enemy").text);
        int range = 1;
        string EnemyList;
        if (isElite)
        {
            while (tab.GetString("EnemyComb", range.ToString(), $"{runMgr.curFloorStr}Elite") != "#End")
            {
                range++;
            }
            int groupID = rnd.nextInt(1, range+1);
            EnemyList=tab.GetString("EnemyComb", groupID.ToString(), $"{runMgr.curFloorStr}Elite");
        }
        else
        {
            while (tab.GetString("EnemyComb", range.ToString(),$"{runMgr.curFloorStr}Normal") != "#End")
            {
                range++;
            }
            int groupID = rnd.nextInt(1, range + 1);
            EnemyList = tab.GetString("EnemyComb", groupID.ToString(), $"{runMgr.curFloorStr}Normal");
        }


        foreach (var enemyID in EnemyList.Split('|'))
        {
            int id;
            if (int.TryParse(enemyID, out id))
            {
                Debug.LogError($"{enemyID}并不能转换成数字");
                continue;
            }

            var obj = typeof(EnemyEntity).Assembly.CreateInstance($"Enemy_{id}");
            EnemyEntity enemy = obj as EnemyEntity;
            enemy.runMgr = runMgr;
            enemy.Init();
            enemy.ID = id;
            enemy.Name = tab.GetString("Enemy", id.ToString(), "Name");
            enemy.MaxHealth = tab.GetInt("Enemy", id.ToString(), "MaxHealth");
            enemy.FullPicPath = tab.GetString("Enemy", id.ToString(), "FullPicPath");
            enemy.HeadPicPath = tab.GetString("Enemy", id.ToString(), "HeadPicPath");
            enemyTeam.Add(enemy);
        }


    }

    public void GenerateBoss(string BossID)
    {
        TableAgent tab=new TableAgent();
        tab.Add(ResourceManager.Instance.Load<TextAsset>("Text/Table/Enemy").text);
        int id;
        if (int.TryParse(BossID, out id))
        {
            Debug.LogError($"{BossID}并不能转换成数字");
            return;
        }

        var obj = typeof(EnemyEntity).Assembly.CreateInstance($"Enemy_{id}");
        EnemyEntity enemy = obj as EnemyEntity;
        enemy.runMgr = runMgr;
        enemy.Init();
        enemy.ID = id;
        enemy.Name = tab.GetString("Enemy", id.ToString(), "Name");
        enemy.MaxHealth = tab.GetInt("Enemy", id.ToString(), "MaxHealth");
        enemy.FullPicPath = tab.GetString("Enemy", id.ToString(), "FullPicPath");
        enemy.HeadPicPath = tab.GetString("Enemy", id.ToString(), "HeadPicPath");
        enemyTeam.Add(enemy);
    }

    public void ChangeTurn()
    {
        CurTurn++;
    }

    public int GetTotalTimeline()
    {
        int enemyT = 0, playerT = 0;
        foreach(var c in curCards)
        {
            playerT += c.ForeShake;
        }
        foreach(var e in enemyTeam) 
        {
            if (e.CurHealth > 0)
            {
                enemyT += e.foreShake;
            }
        }
        return Mathf.Max(enemyT, playerT);
    }

    public void SortTeamList()
    {
        playerTeam.Sort(EntityBase.PositionComparison);
        for (int i = 0; i < playerTeam.Count; i++)
        {
            playerTeam[i].CurPosition = i;
        }

        enemyTeam.Sort(EntityBase.PositionComparison);
        for (int i = 0; i < enemyTeam.Count; i++)
        {
            enemyTeam[i].CurPosition = i;
        }
    }

    public EntityBase GetEntityFromID(int id)
    {
        foreach (var ent in playerTeam)
        {
            if(ent.ID == id) return ent;
        }
        return null;
    }

}
