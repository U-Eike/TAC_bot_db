﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SupportIcon
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SupportIcon : UnitIcon
  {
    private const string TooltipPath = "UI/SupportTooltip";
    public bool UseSelection;

    private SupportData GetSupportData()
    {
      if (this.UseSelection)
        return (SupportData) GlobalVars.SelectedSupport;
      return DataSource.FindDataOfClass<SupportData>(((Component) this).get_gameObject(), (SupportData) null);
    }

    protected override UnitData GetInstanceData()
    {
      SupportData supportData = this.GetSupportData();
      if (supportData == null || supportData.Unit == null)
        return (UnitData) null;
      return supportData.Unit;
    }

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      SupportData supportData = this.GetSupportData();
      if (supportData == null || supportData.Unit == null)
        return;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>("UI/SupportTooltip"));
      DataSource.Bind<UnitData>(gameObject, supportData.Unit);
      DataSource.Bind<SupportData>(gameObject, supportData);
    }
  }
}
