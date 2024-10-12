using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
public class GamingModule : Submodule
{
    string LevelName;
    public GamingModule(object args)
    {
        LevelName = (string)args;
    }
    protected override void OnInit()
    {
        base.OnInit();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
