﻿// Decompiled with JetBrains decompiler
// Type: AnimEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AnimEvent : ScriptableObject
{
  public float Start;
  public float End;

  public AnimEvent()
  {
    base.\u002Ector();
  }

  public virtual void OnStart(GameObject go)
  {
  }

  public virtual void OnTick(GameObject go, float ratio)
  {
  }

  public virtual void OnEnd(GameObject go)
  {
  }

  public virtual void UpdatePreview(GameObject go, float time)
  {
  }
}
