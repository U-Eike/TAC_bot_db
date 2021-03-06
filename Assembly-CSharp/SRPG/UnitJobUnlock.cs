﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitJobUnlock
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/UnitJobUnlock")]
  public class UnitJobUnlock : MonoBehaviour
  {
    public GameObject JobIcon;
    public Text JobName;

    public UnitJobUnlock()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      JobData jobData = (JobData) null;
      for (int index = 0; index < unitDataByUniqueId.Jobs.Length; ++index)
      {
        if (unitDataByUniqueId.Jobs[index] != null && unitDataByUniqueId.Jobs[index].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
          jobData = unitDataByUniqueId.Jobs[index];
      }
      if (jobData == null)
        return;
      if (Object.op_Inequality((Object) this.JobIcon, (Object) null))
      {
        string str = AssetPath.JobIconSmall(jobData == null ? (JobParam) null : jobData.Param);
        if (!string.IsNullOrEmpty(str))
        {
          IconLoader iconLoader = GameUtility.RequireComponent<IconLoader>(this.JobIcon);
          if (Object.op_Inequality((Object) iconLoader, (Object) null))
            iconLoader.ResourcePath = str;
        }
      }
      if (!Object.op_Inequality((Object) this.JobName, (Object) null))
        return;
      this.JobName.set_text(jobData.Name);
    }
  }
}
