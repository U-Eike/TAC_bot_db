﻿// Decompiled with JetBrains decompiler
// Type: SRPG.StatusList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class StatusList : MonoBehaviour
  {
    public StatusListItem ListItem;
    public bool ShowSign;
    private List<StatusListItem> mItems;

    public StatusList()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.ListItem, (Object) null) || !((Component) this.ListItem).get_gameObject().get_activeInHierarchy())
        return;
      ((Component) this.ListItem).get_gameObject().SetActive(false);
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul, BaseStatus modAdd, BaseStatus modMul)
    {
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int num1 = (int) paramAdd[(ParamTypes) values.GetValue(index2)];
          int bonus1 = (int) modAdd[(ParamTypes) values.GetValue(index2)] - num1;
          if (num1 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num1, bonus1, false);
            ++index1;
          }
          int num2 = (int) paramMul[(ParamTypes) values.GetValue(index2)];
          int bonus2 = (int) modMul[(ParamTypes) values.GetValue(index2)] - num2;
          if (num2 != 0 && index2 != 2)
          {
            this.AddValue(index1, names[index2], num2, bonus2, true);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }

    public void SetValues(BaseStatus paramAdd, BaseStatus paramMul)
    {
      this.SetValues(paramAdd, paramMul, paramAdd, paramMul);
    }

    private void AddValue(int index, string type, int value, int bonus, bool multiply)
    {
      if (this.mItems.Count <= index)
      {
        StatusListItem statusListItem = (StatusListItem) Object.Instantiate<StatusListItem>((M0) this.ListItem);
        ((Component) statusListItem).get_transform().SetParent(((Component) this).get_transform(), false);
        this.mItems.Add(statusListItem);
      }
      StatusListItem mItem = this.mItems[index];
      ((Component) mItem).get_gameObject().SetActive(true);
      if (Object.op_Inequality((Object) mItem.Label, (Object) null))
        mItem.Label.set_text(LocalizedText.Get("sys." + type));
      if (Object.op_Inequality((Object) mItem.Value, (Object) null))
      {
        string str = value.ToString();
        if (this.ShowSign && value > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Value.set_text(str);
      }
      if (!Object.op_Inequality((Object) mItem.Bonus, (Object) null))
        return;
      if (bonus != 0)
      {
        string str = bonus.ToString();
        if (this.ShowSign && bonus > 0)
          str = "+" + str;
        if (multiply)
          str += "%";
        mItem.Bonus.set_text(str);
        ((Component) mItem.Bonus).get_gameObject().SetActive(true);
      }
      else
        ((Component) mItem.Bonus).get_gameObject().SetActive(false);
    }

    public void SetDirectValues(BaseStatus old_status, BaseStatus new_status)
    {
      if (Object.op_Equality((Object) this.ListItem, (Object) null))
      {
        DebugUtility.LogWarning(((Component) this).get_gameObject().GetPath((GameObject) null) + ": ListItem not set");
      }
      else
      {
        int index1 = 0;
        string[] names = Enum.GetNames(typeof (ParamTypes));
        Array values = Enum.GetValues(typeof (ParamTypes));
        for (int index2 = 0; index2 < values.Length; ++index2)
        {
          int oldStatu = (int) old_status[(ParamTypes) values.GetValue(index2)];
          int newStatu = (int) new_status[(ParamTypes) values.GetValue(index2)];
          if (oldStatu != newStatu && index2 != 2)
          {
            this.AddValue(index1, names[index2], oldStatu, newStatu, false);
            ++index1;
          }
        }
        for (; index1 < this.mItems.Count; ++index1)
          ((Component) this.mItems[index1]).get_gameObject().SetActive(false);
      }
    }
  }
}
