﻿// Decompiled with JetBrains decompiler
// Type: OnStartDelete
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class OnStartDelete : MonoBehaviour
{
  public OnStartDelete()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }
}
