﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Scene2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/シーン切り替え", "別シーンに切り替えます", 5592405, 4473992)]
  public class EventAction_Scene2 : EventAction
  {
    [HideInInspector]
    public bool FadeIn = true;
    [HideInInspector]
    public bool WaitFadeIn = true;
    [HideInInspector]
    public float FadeInTime = 3f;
    [HideInInspector]
    public List<GameObject> SceneObject = new List<GameObject>();
    [StringIsSceneID]
    public string SceneID;
    private SceneRequest mAsyncOp;
    private GameObject mSceneRoot;

    public override void OnActivate()
    {
      this.Sequence.Scene = (GameObject) null;
      LightmapSettings.set_lightmapsMode((LightmapsMode) 0);
      if (!string.IsNullOrEmpty(this.SceneID))
      {
        if (this.FadeIn)
          GameUtility.FadeOut(0.0f);
        SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
        CriticalSection.Enter(CriticalSections.SceneChange);
        this.mAsyncOp = AssetManager.LoadSceneAsync(this.SceneID, true);
      }
      else
      {
        if (!Object.op_Inequality((Object) this.Sequence.Scene, (Object) null))
          return;
        this.Sequence.Scene = (GameObject) null;
        this.ActivateNext();
      }
    }

    private void OnSceneLoad(GameObject sceneRoot)
    {
      this.mSceneRoot = sceneRoot;
      this.Sequence.Scene = sceneRoot;
      Camera[] componentsInChildren = (Camera[]) this.mSceneRoot.GetComponentsInChildren<Camera>(true);
      for (int index = componentsInChildren.Length - 1; index >= 0; --index)
        ((Component) componentsInChildren[index]).get_gameObject().SetActive(false);
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
    }

    public override void Update()
    {
      if (this.mAsyncOp != null)
      {
        if (this.mAsyncOp.canBeActivated)
          this.mAsyncOp.ActivateScene();
        if (!this.mAsyncOp.isDone)
          return;
        if (this.FadeIn)
          GameUtility.FadeIn(this.FadeInTime);
        CriticalSection.Leave(CriticalSections.SceneChange);
        this.mAsyncOp = (SceneRequest) null;
      }
      else
      {
        if (this.WaitFadeIn && GameUtility.IsScreenFading && this.FadeIn)
          return;
        this.ActivateNext();
      }
    }
  }
}
