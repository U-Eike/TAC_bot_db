﻿// Decompiled with JetBrains decompiler
// Type: SRPG.VersusPlayerInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class VersusPlayerInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject parent;

    public VersusPlayerInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.template, (Object) null))
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      if (multiPlayerParam == null)
        return;
      for (int index = 0; index < multiPlayerParam.units.Length; ++index)
      {
        if (multiPlayerParam.units[index] != null)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.template);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
            DataSource.Bind<UnitData>(gameObject, multiPlayerParam.units[index].unit);
          gameObject.SetActive(true);
          gameObject.get_transform().SetParent(this.parent.get_transform(), false);
        }
      }
    }
  }
}
