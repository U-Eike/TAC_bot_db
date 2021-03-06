﻿// Decompiled with JetBrains decompiler
// Type: OnClickInstantiate
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class OnClickInstantiate : MonoBehaviour
{
  public GameObject Prefab;
  public int InstantiateType;
  private string[] InstantiateTypeNames;
  public bool showGui;

  public OnClickInstantiate()
  {
    base.\u002Ector();
  }

  private void OnClick()
  {
    if (!PhotonNetwork.inRoom)
      return;
    switch (this.InstantiateType)
    {
      case 0:
        PhotonNetwork.Instantiate(((Object) this.Prefab).get_name(), Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.get_identity(), 0);
        break;
      case 1:
        PhotonNetwork.InstantiateSceneObject(((Object) this.Prefab).get_name(), Vector3.op_Addition(InputToEvent.inputHitPos, new Vector3(0.0f, 5f, 0.0f)), Quaternion.get_identity(), 0, (object[]) null);
        break;
    }
  }

  private void OnGUI()
  {
    if (!this.showGui)
      return;
    GUILayout.BeginArea(new Rect((float) (Screen.get_width() - 180), 0.0f, 180f, 50f));
    this.InstantiateType = GUILayout.Toolbar(this.InstantiateType, this.InstantiateTypeNames, new GUILayoutOption[0]);
    GUILayout.EndArea();
  }
}
