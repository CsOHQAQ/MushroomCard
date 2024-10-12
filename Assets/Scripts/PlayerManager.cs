using QxFramework.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager 
{
    private const string PlayerInfoPath = "Text/Table/Player";
    private RunManager run;

    public List<PlayableEntity> PlayerTeam;//默认排序从前到后
    
    public void Init(List<string> nameList)
    {
        InitPlayerTeam(nameList);
    }

    public void InitPlayerTeam(List<string>idList)
    {
        PlayerTeam = new List<PlayableEntity>();
        TableAgent tab=new TableAgent();
        tab.Add(ResourceManager.Instance.Load<TextAsset>(PlayerInfoPath).text);
        foreach (string id in idList)
        {
            PlayableEntity entity = new PlayableEntity();
            entity.runMgr= run;
            entity.Init();
            entity.ID =int.Parse(id);
            entity.Name = tab.GetString("Player", id, "Name");
            entity.MaxHealth= tab.GetInt("Player", id, "MaxHealth");
            entity.FullPicPath= tab.GetString("Player", id, "FullPicPath");
            entity.HeadPicPath = tab.GetString("Player", id, "HeadPicPath");
            entity.CardDeck = GetCardsFromStr(tab.GetString("Player", id, "StartCard"));
            PlayerTeam.Add(entity);
        }
    }
    public void SetRunManager(RunManager manager)
    {
        run= manager;
    }

    public List<CardBase> GetCardsFromStr(string input)
    {
        List<CardBase> cards = new List<CardBase>();
        string[]cardID= input.Split('|');
        foreach (string card in cardID)
        {
            int id=-1;
            if(int.TryParse(card,out id))
            {
                CardBase c = DeepCopy.Clone<CardBase>(run.cardManager.cards[id]);
                cards.Add(c);
            }
            else
            {
                Debug.LogError($"{card}并不为合法整数");
            }
        }
        return cards;
    }

    public void SortTeamList()
    {
        PlayerTeam.Sort(EntityBase.PositionComparison);
        for (int i = 0; i < PlayerTeam.Count; i++)
        {
            PlayerTeam[i].CurPosition = i;
        }
    }

}
