using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
public class GamingProcedure : ProcedureBase
{
    RunManager runMgr;
    protected override void OnEnter(object args)
    {
        base.OnEnter(args);
        runMgr=GameObject.Find("RunManager").GetComponent<RunManager>();
        runMgr.OpenMap();
    }


    protected override void OnLeave()
    {
        base.OnLeave();
    }
}
