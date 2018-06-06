﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAbilityRankUp
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class ReqAbilityRankUp : WebAPI
  {
    public ReqAbilityRankUp(Dictionary<long, int> abilities, Network.ResponseCallback response, string trophyprog = null, string bingoprog = null)
    {
      this.name = "unit/job/abil/lvup";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"aps\":[");
      string str = string.Empty;
      using (Dictionary<long, int>.Enumerator enumerator = abilities.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<long, int> current = enumerator.Current;
          str += "{";
          str = str + "\"iid\":" + (object) current.Key + ",";
          str = str + "\"ap\":" + (object) current.Value;
          str += "},";
        }
      }
      if (str.Length > 0)
        stringBuilder.Append(str.Substring(0, str.Length - 1));
      stringBuilder.Append("]");
      if (!string.IsNullOrEmpty(trophyprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(trophyprog);
      }
      if (!string.IsNullOrEmpty(bingoprog))
      {
        stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
