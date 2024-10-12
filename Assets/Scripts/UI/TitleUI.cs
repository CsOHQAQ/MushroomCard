using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using UnityEngine.UI;
public class TitleUI : UIBase
{
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        Get<Button>("StartGame_btn").onClick.SetListener(StartGame);
    }
    protected override void OnClose()
    {
        base.OnClose();
    }
    void StartGame()
    {
        UIManager.Instance.Open("Close_ani");
        ProcedureManager.Instance.ChangeTo<LoadGameProcedure>();
    }
}
