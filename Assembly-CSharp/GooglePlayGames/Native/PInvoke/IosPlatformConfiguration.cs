﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.IosPlatformConfiguration
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal sealed class IosPlatformConfiguration : PlatformConfiguration
  {
    private IosPlatformConfiguration(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal void SetClientId(string clientId)
    {
      Misc.CheckNotNull<string>(clientId);
      GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_SetClientID(this.SelfPtr(), clientId);
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Dispose(selfPointer);
    }

    internal static IosPlatformConfiguration Create()
    {
      return new IosPlatformConfiguration(GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Construct());
    }
  }
}
