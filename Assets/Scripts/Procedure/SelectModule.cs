using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
public class SelectModule : Submodule
{
    protected override void OnInit()
    {
        base.OnInit();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.Close("SelectUi");
    }
}
