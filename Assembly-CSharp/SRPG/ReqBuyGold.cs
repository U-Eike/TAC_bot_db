﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBuyGold
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBuyGold : WebAPI
  {
    public ReqBuyGold(int coin, Network.ResponseCallback response)
    {
      this.name = "shop/gold/buy";
      this.body = WebAPI.GetRequestString("\"coin\":" + coin.ToString());
      this.callback = response;
    }
  }
}
