using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using UnityEngine.UI;

public class BattleSceneUI : UIBase
{
    private List<Action> oriFunc;
    bool haveChangeCard = false;

    public BattleManager battleMgr;
    public List<CardBase> cards;
    public bool inActionRound=false;
    public float ActionTime = 5f;


    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        battleMgr=(BattleManager)args;
        if(battleMgr is null)
        {
            Debug.LogError("传参错误！");
            return;
        }
        Get<Button>("LeftArrow_btn").onClick.SetListener(ChangeCardLeft);
        Get<Button>("RightArrow_btn").onClick.SetListener(ChangeCardRight);
        Get<Button>("Undo_btn").onClick.SetListener(UndoCardChange);
        Get<Button>("EndTurn_btn").onClick.SetListener(battleMgr.ChangeTurn);

        Refresh();
    }

    

    public void Refresh()
    {
        for (int i = 0; i < Get<Transform>("PlayerTeamSlot").childCount; i++)
        {
            Destroy(Get<Transform>("PlayerTeamSlot").GetChild(i).gameObject);
        }
        for (int i = 0; i < Get<Transform>("EnemyTeamSlot").childCount; i++)
        {
            Destroy(Get<Transform>("EnemyTeamSlot").GetChild(i).gameObject);
        }
        for (int i = 0; i < Get<Transform>("Timeline_img").childCount; i++)
        {
            Destroy(Get<Transform>("Timeline_img").GetChild(i).gameObject);
        }

        foreach (var ent in battleMgr.playerTeam)
        {
            if (ent.CurHealth > 0)//死亡者不添加
            {
                UIManager.Instance.Open("SingleEntityUI", Get<Transform>("PlayerTeamSlot"), "", ent);
            }
        }

        foreach (var ent in battleMgr.enemyTeam)
        {
            if (ent.CurHealth > 0)//死亡者不添加
            {
                UIManager.Instance.Open("SingleEntityUI", Get<Transform>("EnemyTeamSlot"), "", ent);
            }
        }

        int sumT = 0;
        float timelineWidth = Get<RectTransform>("Timeline_img").rect.width;
        foreach (var card in battleMgr.curCards)
        {
            if (battleMgr.GetEntityFromID(card.Belong).CurHealth > 0)//死亡者不添加
            {
                sumT += card.ForeShake;
                var headicon= UIManager.Instance.Open("Headicon_img", Get<Transform>("Timeline_img"));
                headicon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>(battleMgr.GetEntityFromID(card.Belong).HeadPicPath);
                headicon.transform.localPosition = new Vector3(sumT/battleMgr.GetTotalTimeline()*timelineWidth,0);
            }
        }
        sumT = 0;
        foreach (var ent in battleMgr.enemyTeam)
        {
            if (ent.CurHealth > 0)//死亡者不添加
            {
                sumT += ent.foreShake;
                var headicon = UIManager.Instance.Open("Headicon_img", Get<Transform>("Timeline_img"));
                headicon.GetComponent<Image>().sprite = ResourceManager.Instance.Load<Sprite>(ent.HeadPicPath);
                headicon.transform.localPosition = new Vector3(sumT / battleMgr.GetTotalTimeline() * timelineWidth, 0);
            }
        }
        for(int  i = 0; i < 5; i++)
        {
            if(Get<Transform>($"CardSlot ({i})").childCount > 0)
            {
                for (int j = 0; j < Get<Transform>($"CardSlot ({i})").childCount; j++)
                {
                    Destroy(Get<Transform>($"CardSlot ({i})").GetChild(j).gameObject);
                }
            }
            UIManager.Instance.Open("CardUI", Get<Transform>($"CardSlot ({i})"), "", battleMgr.curCards[i]);
        }
    }
    private void Update()
    {
        if (Get<Transform>("MoveCardSlot").childCount > 4)
        {
            CardBase c = Get<Transform>("MoveCardSlot").GetChild(4).GetComponent<CardBase>();
            if (c is not null)
            {
                if(!haveChangeCard)
                {
                    oriFunc = c.TotalFunc;
                    haveChangeCard = true;
                }                
            }
        }
        else
        {
            haveChangeCard = false;
            oriFunc = null;
        }
        if (inActionRound)
            Get<Button>("EndTurn_btn").interactable = false;
        else
            Get<Button>("EndTurn_btn").interactable = true;
    }

    public void ChangeCardLeft()
    {
        if (Get<Transform>("MoveCardSlot").childCount > 4)
        {
            CardBase c=Get<Transform>("MoveCardSlot").GetChild(4).GetComponent<CardBase>();
            if(c is not null)
            {
                oriFunc = c.TotalFunc;
                EntityBase ent=battleMgr.GetEntityFromID(c.ID);
                c.TotalFunc = new List<Action>() {
                    delegate(){
                        CardEffect.Move(ent,new List<EntityBase>(){ent},1);
                    }
                };
            }
        }
    }

    public void ChangeCardRight()
    {
        if (Get<Transform>("MoveCardSlot").childCount > 4)
        {
            CardBase c = Get<Transform>("MoveCardSlot").GetChild(4).GetComponent<CardBase>();
            if (c is not null)
            {
                oriFunc = c.TotalFunc;
                EntityBase ent = battleMgr.GetEntityFromID(c.ID);
                c.TotalFunc = new List<Action>() {
                    delegate(){
                        CardEffect.Move(ent,new List<EntityBase>(){ent},-1);
                    }
                };
            }
        }
    }

    public void UndoCardChange()
    {

        if (Get<Transform>("MoveCardSlot").childCount > 4)
        {
            CardBase c = Get<Transform>("MoveCardSlot").GetChild(4).GetComponent<CardBase>();
            if (c is not null)
            {
                c.TotalFunc = oriFunc;
            }
        }
    }
}
