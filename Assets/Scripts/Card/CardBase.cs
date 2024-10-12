using QxFramework.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
/// <summary>
/// ���ƻ���
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
    /// �鵽ʱ����Ч��
    /// </summary>
    public virtual void OnDraw()
    {

    }

    /// <summary>
    /// ʹ��ʱ����Ч��
    /// </summary>
    public virtual void OnUse()
    {

    }

    /// <summary>
    /// ���ƶ���ʱ����Ч��
    /// </summary>
    public virtual void OnCast()
    {

    }

    /// <summary>
    /// ����ս��������
    /// </summary>
    public virtual void TempExhaust()
    {

    }

    /// <summary>
    /// ��������
    /// </summary>
    public virtual void PermExhaust()
    {

    }

    /// <summary>
    /// ����ʵ��Ч��
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
        string desc = "����ǹ��ڿ���Ч��������";

        switch (effect)
        {
            case EffectType.Damage:
                desc = $"���{value}���˺���";
                break;
            case EffectType.Shield:
                desc = $"���{value}��񵲡�";
                break;
            case EffectType.Move:
                desc = $"��{(value>0?"��":"ǰ")}�ƶ�{value}λ��";
                break;
            case EffectType.Stop:
                desc = $"��{value}�غ����޷������ƶ���";
                break;
            case EffectType.Fragile:
                desc = $"��{value}�غ��л�õĸ񵲼���50%��";
                break;
            case EffectType.Weak:
                desc = $"��{value}�غ�����ɵ��˺�����25%��";
                break;
            case EffectType.Vulnerable:
                desc = $"��{value}�غ����ܵ��˺�����50%��";
                break;
            case EffectType.Toxic:
                desc = $"���{value}�����Ӹ񵲵��ж���";
                break;
            case EffectType.Shock:
                desc = $"��{value}�غ����޷��ж���";
                break;
            case EffectType.Cheer:
                desc = $"��һ���˺��������{value}�㡣";
                break;
            case EffectType.Interupt:
                desc = $"��{value}�غ����޷��ж���";
                break;
            case EffectType.Tenacity:
                desc = $"��{value}�غ����޷�����ϡ�";
                break;
            case EffectType.Power:
                desc = $"����ս���У��˺����{value}�㡣";
                break;
            default:
                desc = $"����Ĭ��Ч��{value}~��";
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
            Debug.Log($"{sender.Name}һ���ƶ��˶����ɫ");
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
            //�ȴ�buffϵͳ����
            ent.BeingInterupted(sender, value);
        }
    }

    public static void Tenacity(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            //�ȴ�buffϵͳ����
            ent.buffManager.AddBuff(new Buff_Tenacity(), value);
        }
    }
    public static void Power(EntityBase sender, List<EntityBase> target, int value)
    {
        foreach (var ent in target)
        {
            //�ȴ�buffϵͳ����
            ent.buffManager.AddBuff(new Buff_Power(), value);
        }
    }
}
