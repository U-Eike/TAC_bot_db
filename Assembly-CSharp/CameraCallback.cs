﻿// Decompiled with JetBrains decompiler
// Type: CameraCallback
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class CameraCallback : MonoBehaviour
{
  public CameraCallback.CameraEvent OnCameraPreCull;
  public CameraCallback.CameraEvent OnCameraPreRender;
  public CameraCallback.CameraEvent OnCameraPostRender;
  public CameraCallback.RenderImageEvent OnCameraRenderImage;

  public CameraCallback()
  {
    base.\u002Ector();
  }

  private void OnPreCull()
  {
    if (this.OnCameraPreCull == null)
      return;
    this.OnCameraPreCull((Camera) ((Component) this).GetComponent<Camera>());
  }

  private void OnPreRender()
  {
    if (this.OnCameraPreRender == null)
      return;
    this.OnCameraPreRender((Camera) ((Component) this).GetComponent<Camera>());
  }

  private void OnPostRender()
  {
    if (this.OnCameraPostRender == null)
      return;
    this.OnCameraPostRender((Camera) ((Component) this).GetComponent<Camera>());
  }

  public delegate void CameraEvent(Camera camera);

  public delegate void RenderImageEvent(Camera camera, RenderTexture src, RenderTexture dest);
}
