using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
public class LoadGameProcedure : ProcedureBase
{
    Close_ani close_Ani;
    RunManager runManager;
    protected override void OnEnter(object args)
    {
        base.OnEnter(args);
        close_Ani =(Close_ani) UIManager.Instance.Open("Close_ani",5);
        InitAllManager();
        ProcedureManager.Instance.ChangeTo<GamingProcedure>();
    }

    protected override void OnLeave()
    {
        base.OnLeave();
        UIManager.Instance.Close(close_Ani);
    }

    private void InitAllManager()
    {
        runManager = GameObject.Find("RunManager").GetComponent<RunManager>();
        //这里之后会接一个选人页面罢
        List<string> initPlayerList = new List<string>() {"101","103","102" };

        runManager.Init(initPlayerList);
    }
}
