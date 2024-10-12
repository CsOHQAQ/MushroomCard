using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using DG.Tweening;

public class CardUI : UIBase, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    CardBase card;
    bool CanDrag = true;
    bool Draging = false;
    Transform parentBefore,tempParent;
    CanvasGroup cg;
    
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        tempParent = GameObject.Find("Canvas").transform.Find("Layer3");
        cg=GetComponent<CanvasGroup>();
        if (args is null || args.GetType() != typeof(CardBase))
        {
            Debug.LogError($"传入参数{args}并非CardBase");
        }

        card = (CardBase)args;
        Get<TextMeshPro>("Name_tmp").text = card.Name;
        Get<TextMeshPro>("Foreshake_tmp").text=card.ForeShake.ToString();
        Get<Image>("CardPic_img").sprite = ResourceManager.Instance.Load<Sprite>($"{card.IlluPath}");
        Get<TextMeshPro>("CardType_tmp").text = card.Type;
        Get<TextMeshPro>("Description_tmp").text = card.GetDescription();
        Get<TextMeshPro>("SpecialDescription_tmp").text = card.GetSpecialDescription();

        for (int i = 7; i >card.MaxTrainLv; i--)
        {
            Destroy(Get<Transform>("TrainLvList").GetChild(i));
        }
        for(int i=0;i< card.MaxTrainLv; i++)
        {
            if (i < card.CurTrainLv)
                Get<Image>($"TrainLv_img{i}").sprite = ResourceManager.Instance.Load<Sprite>("Texture/Cards/trained");
            else
                Get<Image>($"TrainLv_img{i}").sprite = ResourceManager.Instance.Load<Sprite>("Texture/Cards/untrained");
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }
        if (!CanDrag)
        {
            return;
        }
        

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }
        if (!CanDrag)
        {
            return;
        }
        if(cg is null||tempParent is null)
        {
            tempParent = GameObject.Find("Canvas").transform.Find("Layer 3");
            cg = GetComponent<CanvasGroup>();
        }
            
        Draging = true;
        parentBefore = transform.parent;
        cg.blocksRaycasts = false;
        transform.SetParent(tempParent);
    }

    /// <summary>
    /// Raises the drag event.
    /// </summary>
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }
        if (!CanDrag)
        {
            return;
        }
        Vector3 newPosition = new Vector3();
        RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), Input.mousePosition, eventData.enterEventCamera, out newPosition);
        transform.position = newPosition;
    }

    /// <summary>
    /// Raises the end drag event.
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != 0)
        {
            return;
        }
        if (!CanDrag)
        {
            return;
        }
        if (!Draging)
        {
            return;
        }
        Draging = false;

        this.transform.localPosition = Vector3.zero;

        if (eventData.pointerEnter == null)
        {
            return;
        }

        //获取鼠标下面的物体.
        GameObject target = eventData.pointerEnter;
        if (target.transform.parent.name.Contains("CardSlot"))
        {
            Debug.Log($"成功与{target.name}交换位置");
            Transform newParent = target.transform.parent.parent;
            target.transform.SetParent(parentBefore);
            target.transform.DOLocalMove(Vector3.zero,0.5f);
            //target.transform.localPosition = Vector3.zero;
            transform.SetParent(newParent);
            transform.DOLocalMove(Vector3.zero, 0.5f);
            //transform.localPosition = Vector3.zero;
        }
        else
        {
            if (target.transform.name.Contains("MoveCardSlot"))
            {
                Debug.Log($"成功进入转换槽");
                Transform newParent = target.transform;
                transform.SetParent(newParent);
                transform.DOLocalMove(Vector3.zero, 0.5f);
                return;
            }
            else if (target.transform.name.Contains("CardSlot"))
            {
                Debug.Log($"成功进入空白槽");
                Transform newParent = target.transform;
                transform.SetParent(newParent);
                transform.DOLocalMove(Vector3.zero, 0.5f);
                return;
            }

            Debug.Log($"未能成功交换位置,检测到GO为{target}");
            transform.SetParent(parentBefore);

            transform.DOLocalMove(transform.TransformPoint(Vector3.zero), 0.5f);
            //transform.localPosition = Vector3.zero;
        }

        cg.blocksRaycasts = true;

    }

}
