﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeRealTimeRoom
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeRealTimeRoom : BaseReferenceHolder
  {
    internal NativeRealTimeRoom(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal string Id()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, size) => RealTimeRoom.RealTimeRoom_Id(this.SelfPtr(), out_string, size)));
    }

    internal IEnumerable<MultiplayerParticipant> Participants()
    {
      return PInvokeUtilities.ToEnumerable<MultiplayerParticipant>(RealTimeRoom.RealTimeRoom_Participants_Length(this.SelfPtr()), (Func<UIntPtr, MultiplayerParticipant>) (index => new MultiplayerParticipant(RealTimeRoom.RealTimeRoom_Participants_GetElement(this.SelfPtr(), index))));
    }

    internal uint ParticipantCount()
    {
      return RealTimeRoom.RealTimeRoom_Participants_Length(this.SelfPtr()).ToUInt32();
    }

    internal Types.RealTimeRoomStatus Status()
    {
      return RealTimeRoom.RealTimeRoom_Status(this.SelfPtr());
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      RealTimeRoom.RealTimeRoom_Dispose(selfPointer);
    }

    internal static NativeRealTimeRoom FromPointer(IntPtr selfPointer)
    {
      if (selfPointer.Equals((object) IntPtr.Zero))
        return (NativeRealTimeRoom) null;
      return new NativeRealTimeRoom(selfPointer);
    }
  }
}
