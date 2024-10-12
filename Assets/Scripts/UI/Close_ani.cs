using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using QxFramework.Core;

public class Close_ani : UIBase
{
    Tweener twe;
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            Debug.Log(1);
            twe.PlayBackwards();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(2);
            twe.PlayForward();
        }
    }
    */
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        twe = Get<Transform>("Image_1").DOScale(new Vector3(1, 1, 1), 1);
        twe.Pause();
        twe.SetAutoKill(false);
        twe.PlayForward();
    }

    protected override void OnClose()
    {
        base.OnClose();
        twe.PlayBackwards();
    }

}
