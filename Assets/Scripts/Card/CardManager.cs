using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using QxFramework.Core;

public class CardManager 
{
    private const string CardInfoPath = "Text/Table/Cards";
    private RunManager run;
    public Dictionary<int,CardBase>cards=new Dictionary<int,CardBase>();

    public void Init()
    {
        InitAllCard();
    }

    public void InitAllCard()
    {
        cards.Clear();
        TableAgent tab = new TableAgent();
        tab.Add(ResourceManager.Instance.Load<TextAsset>(CardInfoPath).text);
        List<string>allCardTypeName = GetAllCardBase();
        foreach (var cardTypeName in allCardTypeName)
        {
            CardBase c=new CardBase();
            System.Object obj=c.GetType().Assembly.CreateInstance(cardTypeName);
            
            c=(CardBase)obj;
            c.ID = int.Parse(cardTypeName.Split('_')[1]);
            c.Name = tab.GetString("Cards", c.ID.ToString(), "Name");
            c.IlluPath = tab.GetString("Cards", c.ID.ToString(), "IlluPath");
            c.ForeShake = tab.GetInt("Cards", c.ID.ToString(), "ForeShake");
            c.Belong = tab.GetInt("Cards", c.ID.ToString(), "Belong");
            c.Description = tab.GetString("Cards", c.ID.ToString(), "Description");
            c.SpecialDescription = tab.GetString("Cards", c.ID.ToString(), "SpecialDescription");
            c.Type= tab.GetString("Cards", c.ID.ToString(), "Type");
            c.MaxTrainLv = tab.GetInt("Cards", c.ID.ToString(), "MaxTrainLv");
            cards.Add(c.ID, c);
            Debug.Log($"{cardTypeName}成功添加至总牌库");
        }
        

    }
    private List<string> GetAllCardBase()
    {
        List<string> list= new List<string>();
        CardBase assembc = new CardBase();
        var assembly = assembc.GetType().Assembly;
        var allType = assembly.GetTypes();
        foreach (var type in allType)
        {
            var baseT=type.BaseType;
            if (baseT != null)
            {
                if (baseT.Name == assembc.GetType().Name)
                {
                    list.Add(type.Name);
                }
            }
        }

        return list;
    }
    public void SetRunManager(RunManager manager)
    {
        run = manager;
    }
}
