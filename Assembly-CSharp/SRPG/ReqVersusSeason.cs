﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusSeason
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusSeason : WebAPI
  {
    public ReqVersusSeason(Network.ResponseCallback response)
    {
      this.name = "vs/towermatch/season";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int unreadmail;
    }
  }
}
