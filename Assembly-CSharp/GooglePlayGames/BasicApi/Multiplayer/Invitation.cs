﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.Invitation
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Multiplayer
{
  public class Invitation
  {
    private Invitation.InvType mInvitationType;
    private string mInvitationId;
    private Participant mInviter;
    private int mVariant;

    internal Invitation(Invitation.InvType invType, string invId, Participant inviter, int variant)
    {
      this.mInvitationType = invType;
      this.mInvitationId = invId;
      this.mInviter = inviter;
      this.mVariant = variant;
    }

    public Invitation.InvType InvitationType
    {
      get
      {
        return this.mInvitationType;
      }
    }

    public string InvitationId
    {
      get
      {
        return this.mInvitationId;
      }
    }

    public Participant Inviter
    {
      get
      {
        return this.mInviter;
      }
    }

    public int Variant
    {
      get
      {
        return this.mVariant;
      }
    }

    public override string ToString()
    {
      return string.Format("[Invitation: InvitationType={0}, InvitationId={1}, Inviter={2}, Variant={3}]", new object[4]{ (object) this.InvitationType, (object) this.InvitationId, (object) this.Inviter, (object) this.Variant });
    }

    public enum InvType
    {
      RealTime,
      TurnBased,
      Unknown,
    }
  }
}
