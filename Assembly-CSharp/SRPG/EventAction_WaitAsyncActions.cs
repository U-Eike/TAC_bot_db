﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitAsyncActions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("同期", "非同期処理が完了するのを待ちます", 5592405, 4473992)]
  public class EventAction_WaitAsyncActions : EventAction
  {
    public override void OnActivate()
    {
    }

    public override void Update()
    {
      for (int index = 0; index < this.Sequence.Actions.Length && !Object.op_Equality((Object) this.Sequence.Actions[index], (Object) this); ++index)
      {
        if (this.Sequence.Actions[index].enabled)
          return;
      }
      this.ActivateNext();
    }
  }
}
