﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Navigation
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(2, "Discard", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("UI/Navigation", 32741)]
  [FlowNode.Pin(1, "Show", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_Navigation : FlowNode
  {
    public NavigationWindow Template;
    [StringIsTextID(false)]
    public string TextID;
    public NavigationWindow.Alignment Alignment;

    protected override void OnDestroy()
    {
      base.OnDestroy();
      NavigationWindow.DiscardCurrent();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          NavigationWindow.Show(this.Template, LocalizedText.Get(this.TextID), this.Alignment);
          this.ActivateOutputLinks(10);
          break;
        case 2:
          NavigationWindow.DiscardCurrent();
          this.ActivateOutputLinks(10);
          break;
      }
    }
  }
}
