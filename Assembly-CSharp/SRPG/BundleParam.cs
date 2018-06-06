﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BundleParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class BundleParam
  {
    private string mProductId;
    private string mPlatform;
    private string mName;
    private string mDescription;
    private int mAdditionalPaidCoin;
    private int mAdditionalFreeCoin;
    private long mEndDate;
    private BundleParam.BundleContents mContents;
    private int mMaxPurchaseLimit;
    private int mPurchaseLimit;
    private string mImage;
    private int mDisplayOrder;

    public string ProductId
    {
      get
      {
        return this.mProductId;
      }
    }

    public string Platform
    {
      get
      {
        return this.mPlatform;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string Description
    {
      get
      {
        return this.mDescription;
      }
    }

    public int AdditionalPaidCoin
    {
      get
      {
        return this.mAdditionalPaidCoin;
      }
    }

    public int AdditionalFreeCoin
    {
      get
      {
        return this.mAdditionalFreeCoin;
      }
    }

    public long EndDate
    {
      get
      {
        return this.mEndDate;
      }
    }

    public BundleParam.BundleContents Contents
    {
      get
      {
        return this.mContents;
      }
    }

    public int PurchaseLimit
    {
      get
      {
        return this.mPurchaseLimit;
      }
    }

    public string IconImage
    {
      get
      {
        return this.mImage;
      }
    }

    public int DisplayOrder
    {
      get
      {
        return this.mDisplayOrder;
      }
    }

    public List<BundleParam.BundleItemInfo> Deserialize(JSON_BundleItemInfo[] json)
    {
      if (json == null)
        return (List<BundleParam.BundleItemInfo>) null;
      List<BundleParam.BundleItemInfo> bundleItemInfoList = new List<BundleParam.BundleItemInfo>();
      for (int index = 0; index < json.Length; ++index)
      {
        BundleParam.BundleItemInfo bundleItemInfo = new BundleParam.BundleItemInfo();
        if (!bundleItemInfo.Deserialize(json[index]))
          return (List<BundleParam.BundleItemInfo>) null;
        bundleItemInfoList.Add(bundleItemInfo);
      }
      return bundleItemInfoList;
    }

    public bool Deserialize(JSON_BundleParam json)
    {
      if (json == null)
        return false;
      this.mProductId = json.product_id;
      this.mPlatform = json.platform;
      this.mName = json.name;
      this.mDescription = json.description;
      this.mAdditionalPaidCoin = json.additional_paid_coin;
      this.mAdditionalFreeCoin = json.additional_free_coin;
      this.mEndDate = json.end_date;
      this.mImage = json.image;
      this.mDisplayOrder = json.display_order;
      this.mMaxPurchaseLimit = json.max_purchase_limit;
      this.mPurchaseLimit = json.max_purchase_limit - json.purchase_count;
      this.mContents = new BundleParam.BundleContents();
      if (json.contents.items != null)
      {
        this.mContents.Items = new List<BundleParam.BundleItemInfo>();
        this.mContents.Items = this.Deserialize(json.contents.items);
      }
      if (json.contents.units != null)
      {
        this.mContents.Units = new List<BundleParam.BundleItemInfo>();
        this.mContents.Units = this.Deserialize(json.contents.units);
      }
      if (json.contents.artifacts != null)
      {
        this.mContents.Equipments = new List<BundleParam.BundleItemInfo>();
        this.mContents.Equipments = this.Deserialize(json.contents.artifacts);
      }
      return true;
    }

    public class BundleContents
    {
      public List<BundleParam.BundleItemInfo> Items;
      public List<BundleParam.BundleItemInfo> Units;
      public List<BundleParam.BundleItemInfo> Equipments;
    }

    public class BundleItemInfo
    {
      public string Name;
      public int Quantity;

      public bool Deserialize(JSON_BundleItemInfo json)
      {
        if (json == null)
          return false;
        this.Name = json.iname;
        this.Quantity = json.num;
        return true;
      }
    }
  }
}
