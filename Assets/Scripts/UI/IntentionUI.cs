using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using UnityEngine.UI;
using TMPro;

public class IntentionUI : UIBase
{
    EnemyEntity enemyEntity;
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        if (args is null || args.GetType() != typeof(EnemyEntity))
        {
            Debug.LogError($"传入参数{args}并非intention");
        }
        enemyEntity = (EnemyEntity)args;
        Get<Image>("IntentionUI").sprite = ResourceManager.Instance.Load<Sprite>($"Texture/Intention/{enemyEntity.intentionImgPath}");
        Get<TextMeshPro>("Value_tmp").text = (enemyEntity.value).ToString();
        Get<TextMeshPro>("ForeShake").text = (enemyEntity.foreShake).ToString();
    }

}
