﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlColoEnd
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class FlowNode_ReqBtlColoEnd : FlowNode_Network
  {
    private FlowNode_ReqBtlColoEnd.OnSuccesDelegate mOnSuccessDelegate;

    public FlowNode_ReqBtlColoEnd.OnSuccesDelegate OnSuccessListeners
    {
      set
      {
        this.mOnSuccessDelegate = value;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ColoNoBattle)
          this.OnFailed();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.mOnSuccessDelegate();
        }
      }
    }

    public override void OnActivate(int pinID)
    {
    }

    public delegate void OnSuccesDelegate();
  }
}
