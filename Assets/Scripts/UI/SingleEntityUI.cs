using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;
using TMPro;

public class SingleEntityUI : UIBase,IPointerEnterHandler,IPointerExitHandler
{
    bool Dragable = false;
    EntityBase entity;
    Tweener healthTextFadeout,nameTextFadeout;
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        if(args is null||args.GetType() != typeof(EntityBase))
        {
            Debug.LogError($"传入参数{args}并非EntityBase");
            return;
        }

        healthTextFadeout = Get<TextMeshPro>("CurHealth_tmp").DOColor(new Color(1,1,1,1),0.5f);
        nameTextFadeout= Get<TextMeshPro>("EntityName_tmp").DOColor(new Color(1, 1, 1, 1), 0.5f);
        healthTextFadeout.Pause();
        healthTextFadeout.SetAutoKill(false);
        nameTextFadeout.Pause();
        nameTextFadeout.SetAutoKill(false);

        entity = (EntityBase)args;
        Get<Image>("Entity_img").sprite = ResourceManager.Instance.Load<Sprite>($"Character/{entity.ID}_Full");
        Get<Image>("Entity_img").SetNativeSize();
        Get<TextMeshPro>("EntityName_tmp").text = entity.Name;
    }

    private void Update()
    {
        if(entity is not null)
        {
            Get<Image>("CurBlood_img").fillAmount = entity.CurHealth / entity.MaxHealth;
            Get<TextMeshPro>("CurHealth_tmp").text = ($"{entity.CurHealth}/{entity.MaxHealth}");
            int childNum = Get<Transform>("BuffList").childCount;
            for(int i = 0; i < childNum; i++)
            {
                Destroy(Get<Transform>("BuffList").GetChild(0));
            }

            foreach(var buff in entity.buffManager.BuffList) 
            {
                UIManager.Instance.Open("BuffUI", Get<Transform>("BuffList"), "", buff);
            }
            childNum = Get<Transform>("IntentionList").childCount;
            for (int i = 0; i < childNum; i++)
            {
                Destroy(Get<Transform>("IntentionList").GetChild(0));
            }
            

            if (entity.GetType() == typeof(EnemyEntity))
            {
                var ent = (EnemyEntity)entity;
                UIManager.Instance.Open("IntentionUI", Get<Transform>("IntentionList"), "", ent);
                
            }

            
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("MouseEnter");
        healthTextFadeout.PlayForward();
        nameTextFadeout.PlayForward();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("MouseLeave");
        healthTextFadeout.PlayBackwards();
        nameTextFadeout.PlayBackwards();
    }

}
