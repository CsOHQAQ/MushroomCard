using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using DG.Tweening;
using TMPro;

public class ChangeTurnUI : UIBase
{
    Tweener FlagMoveIn,FlagMoveOut,WordMoveIn,WordColor;
   
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        var test = Get<TextMeshPro>("tmp");
        Get<TMP_Text>("tmp").text = (string)args;
        FlagMoveIn = transform.DOLocalMove(new Vector3(-1920,0), 0.7f).From();
        FlagMoveIn.Pause();
        FlagMoveIn.SetAutoKill(false);
        FlagMoveIn.SetEase(Ease.OutQuint);
        FlagMoveIn.OnComplete(delegate () { 
            WordMoveIn.Play();
            WordColor.Play();
        });

        FlagMoveOut = transform.DOLocalMove(new Vector3(1920, 0), 1f);
        FlagMoveOut.Pause();
        FlagMoveOut.SetAutoKill(false);
        FlagMoveOut.SetEase(Ease.InQuint);

        WordMoveIn = Get<Transform>("tmp").DOLocalMove(new Vector3(0, 0), 0.5f);
        WordMoveIn.Pause();
        WordMoveIn.SetAutoKill(false);
        WordMoveIn.SetEase(Ease.OutQuint);
        WordMoveIn.OnComplete(delegate () {
            FlagMoveOut.Play();
        });

        WordColor = Get<TMP_Text>("tmp").DOColor(new Color(1,1,1,1),0.5f);
        WordColor.Pause();
        WordColor.SetAutoKill(false);

        FlagMoveIn.Play();
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(1);
            FlagMoveIn.PlayBackwards();
            WordMoveIn.PlayBackwards();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(2);
            FlagMoveIn.PlayForward();
        }
    }
    */
}
