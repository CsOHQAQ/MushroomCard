using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using UnityEngine.UI;
using TMPro;

public class BuffUI : UIBase
{
    Buff buff;
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        if (args is null || args.GetType() != typeof(Buff))
        {
            Debug.LogError($"�������{args}����Buff");
        }
        buff= (Buff)args;
        Get<Image>("Icon_img").sprite = ResourceManager.Instance.Load<Sprite>($"Texture/Buff/{buff.Name}");
        Get<TextMeshPro>("Time_tmp").text =((int)buff.LastingTime).ToString();
    }



}
