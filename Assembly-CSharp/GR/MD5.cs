﻿// Decompiled with JetBrains decompiler
// Type: GR.MD5
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace GR
{
  public sealed class MD5
  {
    private System.Security.Cryptography.MD5 mMD5;

    ~MD5()
    {
      this.Clear();
    }

    public void Create()
    {
      if (this.mMD5 != null)
        return;
      this.mMD5 = System.Security.Cryptography.MD5.Create();
    }

    public void Clear()
    {
      if (this.mMD5 == null)
        return;
      this.mMD5.Clear();
      this.mMD5 = (System.Security.Cryptography.MD5) null;
    }

    public string Calc(string str, Encoding encode)
    {
      return this.Calc(encode.GetBytes(str));
    }

    public string Calc(byte[] src)
    {
      if (this.mMD5 == null)
        return (string) null;
      byte[] hash = this.mMD5.ComputeHash(src);
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("x2"));
      return stringBuilder.ToString();
    }
  }
}
