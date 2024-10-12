using QxFramework.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
/// <summary>
/// 卡牌基类
/// </summary>
public class CardBase
{
    public int ID;
    public string Name;
    public string IlluPath;
    public int ForeShake;
    public int Belong;
    public string Description;
    public string SpecialDescription;
    public string Type;
    public int MaxTrainLv;
    public int CurTrainLv;
    public List<int> CardPos;
    public List<int> UserPos;
    public List<int> PossibleUpgrade;
    public List<Action> TotalFunc;
     

    public CardBase()
    {

    }
    /// <summary>
    /// 抽到时触发效果
    /// </summary>
    public virtual void OnDraw()
    {

    }

    /// <summary>
    /// 使用时触发效果
    /// </summary>
    public virtual void OnUse()
    {

    }

    /// <summary>
    /// 卡牌丢弃时触发效果
    /// </summary>
    public virtual void OnCast()
    {

    }

    /// <summary>
    /// 单场战斗内消耗
    /// </summary>
    public virtual void TempExhaust()
    {

    }

    /// <summary>
    /// 永久消耗
    /// </summary>
    public virtual void PermExhaust()
    {

    }

    /// <summary>
    /// 具体实现效果
    /// </summary>
    public virtual void Func(EntityBase sender,List<EntityBase>target, int value)
    {

    }

    public virtual void SpeFunc()
    {

    }

    public virtual bool Condition()
    {
        return true;
    }

    public virtual bool SpecialCondition()
    {
        return false;
    }

    public virtual string GetDescription()
    {
        return Description;
    }

    public virtual string GetSpecialDescription()
    {
        return SpecialDescription;
    }
    
}

public class CardEffect
{
    public  enum EffectType
    {
        Damage,
        Shield,
        Move,
        Stop,
        Fragile,
        Weak,
        Vulnerable,
        Toxic,
        Shock,
        Cheer,
        Interupt,
        Tenacity,
        Power,
    }

    public static string GetEffectDescribe(EffectType effect,int value)
    {
        string desc = "这个是关于卡牌效果的描述";

        switch (effect)
        {
            case EffectType.Damage:
                desc = $"造成{value}点伤害。";
                break;
            case EffectType.Shield:
                desc = $"获得{value}点格挡。";
                break;
            case EffectType.Move:
                desc = $"向{(value>0?"后":"前")}移动{value}位。";
                break;
            case EffectType.Stop:
                desc = $"在{value}回合中无法主动移动。";
                break;
            case EffectType.Fragile:
                desc = $"在{value}回合中获得的格挡减少50%。";
                break;
            case EffectType.Weak:
                desc = $"在{value}回合中造成的伤害减少25%。";
                break;
            case EffectType.Vulnerable:
                desc = $"在{value}回合中受到伤害增加50%。";
                break;
            case EffectType.Toxic:
                desc = $"造成{value}点无视格挡的中毒。";
                break;
            case EffectType.Shock:
                desc = $"在{value}回合中无法行动。";
                break;
            case EffectType.Cheer:
                desc = $"下一次伤害额外造成{value}点。";
                break;
            case EffectType.Interupt:
                desc = $"在{value}回合中无法行动。";
                break;
            case EffectType.Tenacity:
                desc = $"在{value}回合中无法被打断。";
                break;
            case EffectType.Power:
                desc = $"本场战斗中，伤害提高{value}点。";
                break;
            default:
                desc = $"我是默认效果{value}~。";
                break;
        }

        return desc;
    }

    public static void Damage(EntityBase sender,List<EntityBase>target,int value)
    {
        foreach (var ent in target)
        {
            ent.BeingDamaged(sender, value);
        }
    }

    public static void Shield(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.GainShield(sender, value);
        }
    }

    public static void Move(EntityBase sender, List<EntityBase> target, int value)
    {
        if (target.Count > 1)
            Debug.Log($"{sender.Name}一次移动了多个角色");
        foreach (var ent in target)
        {
            ent.Move(sender, value);
        }
    }
    public static void Stop(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.buffManager.AddBuff(new Buff_Stop(),value);
        }
    }
    public static void Fragile(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.buffManager.AddBuff(new Buff_Fragile(),value);
        }
    }
    public static void Weak(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.buffManager.AddBuff(new Buff_Weak(),value);
        }
    }
    public static void Vulnerable(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            
            ent.buffManager.AddBuff(new Buff_Vulnerable(),value);
        }
    }
    public static void Shock(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            
            ent.buffManager.AddBuff(new Buff_Shock(),value);
        }
    }
    public static void Toxic(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.buffManager.AddBuff(new Buff_Toxic(),value);
        }
    }

    public static void Cheer(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            ent.buffManager.AddBuff(new Buff_Cheer(),value);
        }
    }
    public static void Interupt(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            //等待buff系统上线
            ent.BeingInterupted(sender, value);
        }
    }

    public static void Tenacity(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            //等待buff系统上线
            ent.buffManager.AddBuff(new Buff_Tenacity(), value);
        }
    }
    public static void Power(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            //等待buff系统上线
            ent.buffManager.AddBuff(new Buff_Power(), value);
        }
    }
}
