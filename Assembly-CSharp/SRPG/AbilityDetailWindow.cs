﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityDetailWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class AbilityDetailWindow : MonoBehaviour
  {
    public Transform SkillLayoutParent;
    public GameObject SkillTemplate;
    public GameObject SkillLockedTemplate;
    [StringIsGameObjectID]
    public string UnlockCondTextId;
    public GameObject SkillUnlockCondWindow;
    public static bool IsEnableSkillChange;

    public AbilityDetailWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.SkillTemplate, (Object) null))
        this.SkillTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.SkillLockedTemplate, (Object) null))
        this.SkillLockedTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.SkillUnlockCondWindow, (Object) null))
        return;
      this.SkillUnlockCondWindow.SetActive(false);
    }

    private void OnEnable()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      UnitData data1 = (UnitData) null;
      if (Object.op_Implicit((Object) UnitEnhanceV3.Instance))
        data1 = UnitEnhanceV3.Instance.CurrentUnit;
      if (data1 == null)
        data1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      AbilityData abilityData = data1.GetAbilityData((long) GlobalVars.SelectedAbilityUniqueID);
      AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam((string) GlobalVars.SelectedAbilityID);
      QuestClearUnlockUnitDataParam[] unlockedSkills = data1.UnlockedSkills;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>();
      QuestClearUnlockUnitDataParam[] allUnlockUnitDatas = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnlockUnitDatas();
      if (allUnlockUnitDatas != null)
      {
        for (int index = 0; index < allUnlockUnitDatas.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          AbilityDetailWindow.\u003CRefresh\u003Ec__AnonStorey22A refreshCAnonStorey22A = new AbilityDetailWindow.\u003CRefresh\u003Ec__AnonStorey22A();
          // ISSUE: reference to a compiler-generated field
          refreshCAnonStorey22A.param = allUnlockUnitDatas[index];
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (refreshCAnonStorey22A.param.type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill && refreshCAnonStorey22A.param.uid == data1.UnitID && refreshCAnonStorey22A.param.parent_id == abilityParam.iname && (unlockedSkills == null || Array.FindIndex<QuestClearUnlockUnitDataParam>(unlockedSkills, new Predicate<QuestClearUnlockUnitDataParam>(refreshCAnonStorey22A.\u003C\u003Em__230)) == -1))
          {
            // ISSUE: reference to a compiler-generated field
            unlockUnitDataParamList.Add(refreshCAnonStorey22A.param);
          }
        }
      }
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.GetRarityParam((int) data1.UnitParam.raremax);
      int num = Math.Min((int) rarityParam.UnitLvCap + (int) rarityParam.UnitAwakeLvCap, abilityParam.GetRankCap());
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), data1);
      DataSource.Bind<AbilityData>(((Component) this).get_gameObject(), abilityData);
      AbilityParam data2 = abilityParam;
      if (AbilityDetailWindow.IsEnableSkillChange)
      {
        string key = data1.SearchAbilityReplacementSkill(abilityParam.iname);
        if (!string.IsNullOrEmpty(key))
          data2 = MonoSingleton<GameManager>.Instance.GetAbilityParam(key);
      }
      DataSource.Bind<AbilityParam>(((Component) this).get_gameObject(), data2);
      if (Object.op_Inequality((Object) this.SkillTemplate, (Object) null))
      {
        List<SkillParam> skillParamList = new List<SkillParam>();
        if (abilityData != null && abilityData.LearningSkills != null)
        {
          for (int index1 = 0; index1 < abilityData.LearningSkills.Length; ++index1)
          {
            if (abilityData.LearningSkills[index1].locklv <= num)
            {
              string str1 = abilityData.LearningSkills[index1].iname;
              if (unlockedSkills != null)
              {
                for (int index2 = 0; index2 < unlockedSkills.Length; ++index2)
                {
                  if (unlockedSkills[index2].old_id == str1)
                  {
                    str1 = unlockedSkills[index2].new_id;
                    break;
                  }
                }
              }
              if (AbilityDetailWindow.IsEnableSkillChange)
              {
                string str2 = data1.SearchReplacementSkill(str1);
                if (!string.IsNullOrEmpty(str2))
                  str1 = str2;
              }
              skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(str1));
            }
          }
          if (unlockedSkills != null)
          {
            for (int index = 0; index < unlockedSkills.Length; ++index)
            {
              if (unlockedSkills[index].add && unlockedSkills[index].parent_id == abilityData.AbilityID)
                skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(unlockedSkills[index].new_id));
            }
          }
        }
        else
        {
          for (int index = 0; index < abilityParam.skills.Length; ++index)
          {
            if (abilityParam.skills[index].locklv <= num)
              skillParamList.Add(MonoSingleton<GameManager>.Instance.GetSkillParam(abilityParam.skills[index].iname));
          }
        }
        if (abilityData == data1.MasterAbility && data1.CollaboAbility != null)
        {
          using (List<SkillData>.Enumerator enumerator = data1.CollaboAbility.Skills.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SkillData current = enumerator.Current;
              skillParamList.Add(current.SkillParam);
            }
          }
        }
        for (int index = 0; index < skillParamList.Count; ++index)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.SkillTemplate);
          DataSource.Bind<SkillParam>(gameObject, skillParamList[index]);
          gameObject.get_transform().SetParent(this.SkillLayoutParent, false);
          gameObject.SetActive(true);
        }
        for (int index = 0; index < unlockUnitDataParamList.Count; ++index)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.SkillLockedTemplate);
          DataSource.Bind<SkillParam>(gameObject, MonoSingleton<GameManager>.Instance.GetSkillParam(unlockUnitDataParamList[index].new_id));
          DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject, unlockUnitDataParamList[index]);
          gameObject.get_transform().SetParent(this.SkillLayoutParent, false);
          gameObject.SetActive(true);
        }
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void OnOpenSkillUnlockCondWindow(GameObject button)
    {
      if (!Object.op_Inequality((Object) this.SkillUnlockCondWindow, (Object) null))
        return;
      GameObject gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.SkillUnlockCondWindow);
      QuestClearUnlockUnitDataParam dataOfClass = DataSource.FindDataOfClass<QuestClearUnlockUnitDataParam>(button, (QuestClearUnlockUnitDataParam) null);
      DataSource.Bind<QuestClearUnlockUnitDataParam>(gameObject1, dataOfClass);
      gameObject1.SetActive(true);
      gameObject1.get_transform().SetParent(((Component) this).get_transform(), false);
      Text gameObject2 = GameObjectID.FindGameObject<Text>(this.UnlockCondTextId);
      if (!Object.op_Inequality((Object) gameObject2, (Object) null))
        return;
      gameObject2.set_text(dataOfClass.GetUnitLevelCond());
      string abilityLevelCond = dataOfClass.GetAbilityLevelCond();
      if (!string.IsNullOrEmpty(abilityLevelCond))
      {
        Text text = gameObject2;
        text.set_text(text.get_text() + (string.IsNullOrEmpty(gameObject2.get_text()) ? string.Empty : "\n") + abilityLevelCond);
      }
      string clearQuestCond = dataOfClass.GetClearQuestCond();
      if (string.IsNullOrEmpty(clearQuestCond))
        return;
      Text text1 = gameObject2;
      text1.set_text(text1.get_text() + (string.IsNullOrEmpty(gameObject2.get_text()) ? string.Empty : "\n") + clearQuestCond);
    }
  }
}
