﻿// Decompiled with JetBrains decompiler
// Type: SRPG.LogMapEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class LogMapEvent : BattleLog
  {
    public BuffBit buff = new BuffBit();
    public BuffBit debuff = new BuffBit();
    public Unit self;
    public Unit target;
    public EEventType type;
    public EEventGimmick gimmick;
    public int heal;

    public bool IsBuffEffect()
    {
      for (int index = 0; index < this.buff.bits.Length; ++index)
      {
        if (this.buff.bits[index] != 0)
          return true;
      }
      for (int index = 0; index < this.debuff.bits.Length; ++index)
      {
        if (this.debuff.bits[index] != 0)
          return true;
      }
      return false;
    }
  }
}
