﻿// Decompiled with JetBrains decompiler
// Type: EventCode
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

public class EventCode
{
  public const byte GameList = 230;
  public const byte GameListUpdate = 229;
  public const byte QueueState = 228;
  public const byte Match = 227;
  public const byte AppStats = 226;
  public const byte LobbyStats = 224;
  [Obsolete("TCP routing was removed after becoming obsolete.")]
  public const byte AzureNodeInfo = 210;
  public const byte Join = 255;
  public const byte Leave = 254;
  public const byte PropertiesChanged = 253;
  [Obsolete("Use PropertiesChanged now.")]
  public const byte SetProperties = 253;
  public const byte ErrorInfo = 251;
  public const byte CacheSliceChanged = 250;
  public const byte AuthEvent = 223;
}
