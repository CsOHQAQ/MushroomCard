using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase
{
    public RunManager runMgr;

    public int ID;
    public string Name;
    public int MaxHealth;
    public string FullPicPath;
    public string HeadPicPath;



    public int CurHealth;
    public int CurPosition;
    public int CurBlock;
    public BuffManager buffManager;

    public EntityBase() 
    {
    }

    public virtual void Init()
    {
        
    }
    /// <summary>
    /// 切换回合时切换状态
    /// </summary>
    public void Refresh()
    {

    }
    public void BeingDamaged(EntityBase Damager,int Damage)
    {
        int finalDmg = Damage;
        //if (buffManager.BuffList.Contains())  等待Buff系统上线
        if (CurBlock >= finalDmg)
        {
            CurBlock -= finalDmg;
        }
        else
        {
            CurHealth = CurHealth + CurBlock - finalDmg;
            CurBlock = 0;           
        }
    } 
    public void GainShield(EntityBase sender,int value)
    {
        int finalValue = value;
        CurBlock += finalValue;
    }
    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="value">负数代表向前移动一位，正数代表向后移动一位</param>
    public void Move(EntityBase sender,int value)
    {
        if (this.GetType() == typeof(PlayableEntity))
        {
            if (CurPosition + value >= 0 && CurPosition + value < runMgr.battleManager.playerTeam.Count)
            {
                int temp = runMgr.battleManager.playerTeam[CurPosition+value].CurPosition;
                runMgr.battleManager.playerTeam[CurPosition + value].CurPosition = CurPosition;
                CurPosition= temp;
            }
            else
            {
                CurPosition = CurPosition + value;
            }
        }
        else
        {
            if (CurPosition + value >= 0 && CurPosition + value < runMgr.battleManager.enemyTeam.Count)
            {
                int temp = runMgr.battleManager.enemyTeam[CurPosition + value].CurPosition;
                runMgr.battleManager.enemyTeam[CurPosition + value].CurPosition = CurPosition;
                CurPosition = temp;
            }
            else
            {
                CurPosition = CurPosition + value;
            }
        }
    }

    public void BeingInterupted(EntityBase sender,int value)
    {
        BeingDamaged(sender,value);
        if (CurBlock == 0)
        {
            buffManager.AddBuff(new Buff_Shock(),1);
        }
    }


    public static int PositionComparison(EntityBase e1,EntityBase e2)
    {
        return e1.CurPosition.CompareTo(e2.CurPosition);
    }

}
