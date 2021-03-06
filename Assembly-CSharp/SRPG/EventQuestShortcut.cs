﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventQuestShortcut
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class EventQuestShortcut : MonoBehaviour
  {
    public GameObject KeyQuestOpenEffect;

    public EventQuestShortcut()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.RefreshSwitchButton();
    }

    private void RefreshSwitchButton()
    {
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      bool flag = false;
      if (chapters != null)
      {
        long serverTime = Network.GetServerTime();
        for (int index = 0; index < chapters.Length; ++index)
        {
          if (chapters[index].IsKeyQuest() && chapters[index].IsKeyUnlock(serverTime))
            flag = true;
        }
      }
      if (!Object.op_Inequality((Object) this.KeyQuestOpenEffect, (Object) null))
        return;
      this.KeyQuestOpenEffect.SetActive(flag);
    }
  }
}
