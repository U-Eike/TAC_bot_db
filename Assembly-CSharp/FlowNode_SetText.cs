﻿// Decompiled with JetBrains decompiler
// Type: FlowNode_SetText
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.Pin(1, "Set", FlowNode.PinTypes.Input, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("UI/SetText", 32741)]
[FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 0)]
public class FlowNode_SetText : FlowNode
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (UnityEngine.UI.Text), true)]
  public UnityEngine.UI.Text Target;
  public string Text;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    if (Object.op_Inequality((Object) this.Target, (Object) null))
      this.Target.set_text(this.Text);
    this.ActivateOutputLinks(100);
  }
}
