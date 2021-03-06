﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiRank
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiRank : WebAPI
  {
    public ReqMultiRank(string iname, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit";
      this.body = "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Json_MultiRankParam
    {
      public string unit_iname;
      public string job_iname;
    }

    public class Json_MultiRank
    {
      public ReqMultiRank.Json_MultiRankParam[] ranking;
      public int isReady;
    }
  }
}
