﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlashEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FlashEffect : MonoBehaviour
  {
    private RenderPipeline mTarget;
    public float Strength;
    public float Duration;
    private float mTime;

    public FlashEffect()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mTarget = (RenderPipeline) ((Component) this).GetComponent<RenderPipeline>();
      if (!Object.op_Equality((Object) this.mTarget, (Object) null))
        return;
      Object.Destroy((Object) this);
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mTarget, (Object) null))
        return;
      this.mTarget.SwapEffect = RenderPipeline.SwapEffects.Copy;
    }

    private void Update()
    {
      this.mTime += Time.get_deltaTime();
      float num = Mathf.Clamp01(this.mTime / this.Duration);
      this.mTarget.SwapEffect = RenderPipeline.SwapEffects.Dodge;
      this.mTarget.SwapEffectOpacity = (1f - num) * this.Strength;
      if ((double) num < 1.0)
        return;
      Object.Destroy((Object) this);
    }
  }
}
