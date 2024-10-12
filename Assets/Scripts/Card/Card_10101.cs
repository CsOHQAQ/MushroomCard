using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_10101 : CardBase
{
    
    public Card_10101()
    {
        CardPos = new List<int>();
        UserPos=new List<int>() {0};
    }


    public override void OnDraw()
    {
        base.OnDraw();
    }
    public override void Func(EntityBase sender,List<EntityBase> target, int value)
    {
        CardEffect.Damage(sender,target,value);
    }
    public override string GetDescription()
    {
        return base.GetDescription();
    }


}
