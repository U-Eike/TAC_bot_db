﻿// Decompiled with JetBrains decompiler
// Type: ResourceLoadRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ResourceLoadRequest : LoadRequest
{
  private Object mAsset;
  private ResourceRequest mLoadRequest;

  public ResourceLoadRequest()
  {
  }

  public ResourceLoadRequest(ResourceRequest request)
  {
    this.mLoadRequest = request;
  }

  public ResourceLoadRequest(Object _asset)
  {
    this.mAsset = _asset;
  }

  public override Object asset
  {
    get
    {
      return this.mAsset;
    }
  }

  public override float progress
  {
    get
    {
      if (this.mLoadRequest != null)
        return ((AsyncOperation) this.mLoadRequest).get_progress();
      return Object.op_Inequality(this.mAsset, (Object) null) ? 1f : 0.0f;
    }
  }

  public override bool isDone
  {
    get
    {
      this.UpdateLoading();
      return this.mLoadRequest == null;
    }
  }

  public override bool MoveNext()
  {
    this.UpdateLoading();
    return this.mLoadRequest != null;
  }

  private void UpdateLoading()
  {
    if (this.mLoadRequest == null || !((AsyncOperation) this.mLoadRequest).get_isDone())
      return;
    this.mAsset = this.mLoadRequest.get_asset();
    this.mLoadRequest = (ResourceRequest) null;
    this.UntrackTextComponents(this.mAsset);
  }
}
