﻿// Decompiled with JetBrains decompiler
// Type: BatchChunk
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public abstract class BatchChunk : MonoBehaviour
{
  public Mesh Mesh;
  public Material Material;

  protected BatchChunk()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    if (!Application.get_isPlaying())
      return;
    ((Behaviour) this).set_enabled(false);
  }

  public abstract void FillTriangles(int baseVertex, Vector3[] vertices, Vector3[] normals, Vector2[] uv, Color32[] colors, Vector3[] centers, int baseIndex, int[] indices);

  public virtual int VertexCount
  {
    get
    {
      if (Object.op_Inequality((Object) this.Mesh, (Object) null))
        return this.Mesh.get_vertexCount();
      return 0;
    }
  }

  public virtual int IndexCount
  {
    get
    {
      if (Object.op_Inequality((Object) this.Mesh, (Object) null))
        return this.Mesh.get_triangles().Length;
      return 0;
    }
  }
}
