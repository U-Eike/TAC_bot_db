﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoReset
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlColoReset : WebAPI
  {
    public ReqBtlColoReset(ColoResetTypes reset, Network.ResponseCallback response)
    {
      this.name = "btl/colo/reset/" + reset.ToString();
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }
  }
}
