﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TowerEnemyListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TowerEnemyListItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject DisableIcon;
    [SerializeField]
    private CanvasGroup mCanvasGroup;

    public TowerEnemyListItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
      this.Refresh();
      if (!Object.op_Inequality((Object) this.DisableIcon, (Object) null))
        return;
      this.DisableIcon.SetActive(false);
    }

    private void Update()
    {
    }

    private void Refresh()
    {
      Unit dataOfClass = DataSource.FindDataOfClass<Unit>(((Component) this).get_gameObject(), (Unit) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsDead)
        this.mCanvasGroup.set_alpha(0.5f);
      else
        this.mCanvasGroup.set_alpha(1f);
    }

    public void UpdateValue()
    {
      this.Refresh();
    }
  }
}
