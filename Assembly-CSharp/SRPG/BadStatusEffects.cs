﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BadStatusEffects
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public static class BadStatusEffects
  {
    public static List<BadStatusEffects.Desc> Effects;
    public static GameObject CurseEffect;
    public static string CurseEffectAttachTarget;

    [DebuggerHidden]
    public static IEnumerator LoadEffects(QuestAssets assets)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new BadStatusEffects.\u003CLoadEffects\u003Ec__Iterator48() { assets = assets, \u003C\u0024\u003Eassets = assets };
    }

    public static void UnloadEffects()
    {
      BadStatusEffects.Effects = (List<BadStatusEffects.Desc>) null;
      BadStatusEffects.CurseEffect = (GameObject) null;
    }

    public class Desc
    {
      public EUnitCondition Key;
      public GameObject Effect;
      public string AttachTarget;
      public Color BlendColor;
      public string AnimationName;
    }
  }
}
