﻿// Decompiled with JetBrains decompiler
// Type: SRPG.TacticsUnitController
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG.AnimEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class TacticsUnitController : UnitController
  {
    public static List<TacticsUnitController> Instances = new List<TacticsUnitController>(16);
    public static readonly string ANIM_IDLE_FIELD = "idlefield0";
    public static readonly string ANIM_IDLE_DEMO = "cmn_demo_talk_idle0";
    public static readonly string ANIM_RUN_FIELD = "runfield0";
    public static readonly string ANIM_STEP = "cmn_step0";
    public static readonly string ANIM_FALL_LOOP = "cmn_fallloop0";
    public static readonly string ANIM_FALL_END = "cmn_fallland0";
    public static readonly string ANIM_CLIMBUP = "cmn_jumploop0";
    public static readonly string ANIM_PICKUP = "cmn_pickup0";
    public static readonly string ANIM_GENKIDAMA = "cmn_freeze0";
    private static string CameraAnimationDir = "Camera/";
    public Color ColorMod = Color.get_white();
    [NonSerialized]
    public int PlayerMemberIndex = -1;
    public bool AutoUpdateRotation = true;
    private Dictionary<string, GameObject> mDefendSkillEffects = new Dictionary<string, GameObject>();
    private float mRunSpeed = 4f;
    private Vector3 mVelocity = Vector3.get_zero();
    private bool mCollideGround = true;
    private float mSpinCount = 2f;
    public TacticsUnitController.EElementEffectTypes ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
    public List<TacticsUnitController.ShieldState> Shields = new List<TacticsUnitController.ShieldState>();
    private float mCastJumpOffsetY = 10f;
    private const string ID_IDLE = "IDLE";
    private const string ID_RUN = "RUN";
    private const string ID_STEP = "STEP";
    private const string ID_PICKUP = "PICK";
    private const string ID_BADSTATUS = "BAD";
    private const string ID_CUSTOMRUN = "RUN_";
    private const string ID_FALL_LOOP = "FLLP";
    private const string ID_FALL_END = "FLEN";
    private const string ID_CLIMBUP = "CLMB";
    private const string ID_GENKIDAMA = "GENK";
    private const string ID_BATTLE_RUN = "B_RUN";
    private const string ID_BATTLE_SKILL = "B_SKL";
    private const string ID_BATTLE_BACKSTEP = "B_BS";
    private const string ID_BATTLE_DAMAGE_NORMAL = "B_DMG0";
    private const string ID_BATTLE_DAMAGE_AIR = "B_DMG1";
    private const string ID_BATTLE_DEFEND = "B_DEF";
    private const string ID_BATTLE_DODGE = "B_DGE";
    private const string ID_BATTLE_DOWN = "B_DOWN";
    private const string ID_BATTLE_DYING = "B_DIE";
    private const string ID_BATTLE_CHANT = "B_CHA";
    private const string ID_BATTLE_RUNLOOP = "B_RUNL";
    private const string ID_BATTLE_DEAD = "B_DEAD";
    private const string ID_BATTLE_PRESKILL = "B_PRS";
    private const string ID_BATTLE_TOSS_LIFT = "B_TOSS_LIFT";
    private const string ID_BATTLE_TOSS_THROW = "B_TOSS_THROW";
    public const string ANIM_BATTLE_TOSS_LIFT = "cmn_toss_lift0";
    public const string ANIM_BATTLE_TOSS_THROW = "cmn_toss_throw0";
    public const string COLLABO_SKILL_NAME_SUB = "_sub";
    private const float CameraInterpRate = 4f;
    private const float KNOCK_BACK_SEC = 0.4f;
    public List<string> mCustomRunAnimations;
    private GameObject mRenkeiAuraEffect;
    private GameObject mDrainEffect;
    private GameObject mChargeGrnTargetUnitEffect;
    private GameObject mChargeRedTargetUnitEffect;
    private GameObject mVersusCursor;
    private Transform mVersusCursorRoot;
    public TacticsUnitController.PostureTypes Posture;
    private Texture2D mIconSmall;
    private Texture2D mIconMedium;
    private Texture2D mJobIcon;
    private string mRunAnimation;
    private int mCachedHP;
    private UnitGauge mHPGauge;
    private UnitGaugeMark mAddIconGauge;
    private float mKeepHPGaugeVisible;
    private ChargeIcon mChargeIcon;
    private DeathSentenceIcon mDeathSentenceIcon;
    private GameObject mOwnerIndexUI;
    private TacticsUnitController.HideGimmickAnimation mHideGimmickAnim;
    public string UniqueName;
    public int TurnCount;
    private Unit mUnit;
    private Projector mShadow;
    public UnitCursor UnitCursorTemplate;
    private UnitCursor mUnitCursor;
    private StateMachine<TacticsUnitController> mStateMachine;
    private bool mCancelAction;
    private EUnitDirection mFieldActionDir;
    private Vector2 mFieldActionPoint;
    private Color mBlendColor;
    private TacticsUnitController.ColorBlendModes mBlendMode;
    private float mBlendColorTime;
    private float mBlendColorTimeMax;
    private bool mEnableColorBlending;
    private PetController mPet;
    private GameObject mPickupObject;
    private Vector3[] mRoute;
    private int mRoutePos;
    private GridMap<int> mWalkableField;
    public bool IgnoreMove;
    private float mPostMoveAngle;
    private float mIdleInterpTime;
    private bool mLoadedPartially;
    private Vector3 mLookAtTarget;
    private TacticsUnitController.FieldActionEndEvent mOnFieldActionEnd;
    private Vector3 mStepStart;
    private Vector3 mStepEnd;
    private float mMoveAnimInterpTime;
    private BadStatusEffects.Desc mBadStatus;
    private GameObject mBadStatusEffect;
    private string mLoadedBadStatusAnimation;
    private GameObject mCurseEffect;
    private string mLoadedCurseAnimation;
    private bool IsCursed;
    public int DrainGemsOnHit;
    public GemParticle[] GemDrainEffects;
    public GameObject GemDrainHitEffect;
    public bool ShowCriticalEffectOnHit;
    public bool ShowBackstabEffectOnHit;
    private TacticsUnitController.ShakeUnit mShaker;
    private float mMapTrajectoryHeight;
    private TacticsUnitController.SkillVars mSkillVars;
    public bool ShouldDodgeHits;
    public bool ShouldPerfectDodge;
    public bool ShouldDefendHits;
    private GameObject mLastHitEffect;
    private LogSkill.Target mHitInfo;
    private LogSkill.Target mHitInfoSelf;
    private bool mCastJumpStartComplete;
    private bool mCastJumpFallComplete;
    public Vector3 JumpFallPos;
    public IntVector2 JumpMapFallPos;
    private TacticsUnitController.eKnockBackMode mKnockBackMode;
    private Grid mKnockBackGrid;
    private Vector3 mKbPosStart;
    private Vector3 mKbPosEnd;
    private float mKbPassedSec;

    public void StartSkillWithAnimationOption(Vector3 targetPosition, Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill, bool withAnimation = true)
    {
      this.mSkillVars.HitGrids = hitGrids;
      if (withAnimation)
        this.InternalStartSkill(targets, targetPosition, activeCamera);
      else
        this.InternalStartSkillWithoutAnimation(targets, targetPosition, activeCamera);
    }

    public void StartSkillWithAnimationOption(TacticsUnitController target, Camera activeCamera, SkillParam skill, bool withAnimation = true)
    {
      if (withAnimation)
        this.InternalStartSkill(new TacticsUnitController[1]
        {
          target
        }, ((Component) target).get_transform().get_position(), activeCamera);
      else
        this.InternalStartSkillWithoutAnimation(new TacticsUnitController[1]
        {
          target
        }, ((Component) target).get_transform().get_position(), activeCamera);
    }

    private void InternalStartSkillWithoutAnimation(TacticsUnitController[] targets, Vector3 targetPosition, Camera activeCamera)
    {
      if (this.mSkillVars.mSkillSequence == null)
      {
        Debug.LogError((object) "SkillSequence not loaded yet");
      }
      else
      {
        this.mCollideGround = !this.mSkillVars.UseBattleScene;
        this.mSkillVars.mActiveCamera = activeCamera;
        this.mSkillVars.Targets = new List<TacticsUnitController>((IEnumerable<TacticsUnitController>) targets);
        if (this.mSkillVars.UseBattleScene)
          this.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
        this.mSkillVars.HitTimerThread = this.StartCoroutine(this.AsyncHitTimer());
        this.mSkillVars.mStartPosition = ((Component) this).get_transform().get_position();
        if (targets.Length == 1)
        {
          this.mSkillVars.mTargetController = targets[0];
          this.mSkillVars.mTargetControllerPosition = ((Component) this.mSkillVars.mTargetController).get_transform().get_position();
          this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        }
        this.mSkillVars.mTargetPosition = !this.mSkillVars.UseBattleScene ? targetPosition : this.mSkillVars.mTargetControllerPosition;
        this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
        this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
        this.mSkillVars.mAuraEnable = Object.op_Inequality((Object) this.mSkillVars.mSkillEffect.AuraEffect, (Object) null);
        if (this.UseSubEquipment)
          this.SwitchEquipments();
        this.mSkillVars.mCameraID = 0;
        this.mSkillVars.mChantCameraID = 0;
        this.mSkillVars.mSkillCameraID = 0;
        this.GotoState<TacticsUnitController.State_Skill_Without_Animation>();
      }
    }

    public void ResetColorMod()
    {
      this.ColorMod = Color.get_white();
    }

    public void CacheIcons()
    {
      if (this.Unit == null)
        return;
      this.StartCoroutine(this.CacheIconsAsync());
    }

    [DebuggerHidden]
    private IEnumerator CacheIconsAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CCacheIconsAsync\u003Ec__IteratorF() { \u003C\u003Ef__this = this };
    }

    public static TacticsUnitController FindByUnitID(string unitID)
    {
      for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
      {
        if (TacticsUnitController.Instances[index].Unit != null && TacticsUnitController.Instances[index].Unit.UnitParam.iname == unitID)
          return TacticsUnitController.Instances[index];
      }
      return (TacticsUnitController) null;
    }

    public static TacticsUnitController FindByUniqueName(string uniqueName)
    {
      for (int index = TacticsUnitController.Instances.Count - 1; index >= 0; --index)
      {
        if (TacticsUnitController.Instances[index].UniqueName == uniqueName)
          return TacticsUnitController.Instances[index];
      }
      return (TacticsUnitController) null;
    }

    public void LoadRunAnimation(string animationName)
    {
      if (string.IsNullOrEmpty(animationName))
        return;
      this.LoadAnimationAsync("RUN_" + animationName, "CHM/" + animationName);
    }

    public void LoadRunAnimation(string animationName, string path)
    {
      if (string.IsNullOrEmpty(animationName))
        return;
      this.LoadAnimationAsync("RUN_" + animationName, path);
    }

    public void UnloadRunAnimation(string animationName)
    {
      this.UnloadAnimation("RUN_" + animationName);
    }

    public void SetRunAnimation(string animationName)
    {
      bool flag = false;
      string str = string.IsNullOrEmpty(animationName) ? "RUN" : "RUN_" + animationName;
      if (str == this.mRunAnimation)
        return;
      if (this.IsAnimationPlaying(this.mRunAnimation))
      {
        this.StopAnimation(this.mRunAnimation);
        flag = true;
      }
      this.mRunAnimation = str;
      if (!flag)
        return;
      this.PlayAnimation(this.mRunAnimation, true);
    }

    public int HPPercentage
    {
      get
      {
        return Mathf.Clamp((int) this.Unit.MaximumStatus.param.hp == 0 ? 100 : this.mCachedHP * 100 / (int) this.Unit.MaximumStatus.param.hp, 0, 100);
      }
    }

    public int VisibleHPValue
    {
      get
      {
        return this.mCachedHP;
      }
    }

    public ChargeIcon ChargeIcon
    {
      get
      {
        return this.mChargeIcon;
      }
      set
      {
      }
    }

    public DeathSentenceIcon DeathSentenceIcon
    {
      get
      {
        return this.mDeathSentenceIcon;
      }
      set
      {
      }
    }

    public void InitHPGauge(Canvas canvas, UnitGauge gaugeTemplate)
    {
      if (Object.op_Equality((Object) canvas, (Object) null) || Object.op_Equality((Object) gaugeTemplate, (Object) null))
        return;
      if (Object.op_Equality((Object) this.mHPGauge, (Object) null))
      {
        this.mHPGauge = (UnitGauge) Object.Instantiate<UnitGauge>((M0) gaugeTemplate);
        this.mHPGauge.SetOwner(this.Unit);
      }
      if (Object.op_Equality((Object) this.mAddIconGauge, (Object) null))
        this.mAddIconGauge = (UnitGaugeMark) ((Component) this.mHPGauge).GetComponent<UnitGaugeMark>();
      if (Object.op_Equality((Object) this.mChargeIcon, (Object) null))
        this.mChargeIcon = (ChargeIcon) ((Component) this.mHPGauge).GetComponent<ChargeIcon>();
      if (Object.op_Equality((Object) this.mDeathSentenceIcon, (Object) null))
      {
        this.mDeathSentenceIcon = (DeathSentenceIcon) ((Component) this.mHPGauge).GetComponent<DeathSentenceIcon>();
        this.mDeathSentenceIcon.Init(this.Unit);
      }
      this.ResetHPGauge();
    }

    public void SetHPGaugeMode(TacticsUnitController.HPGaugeModes mode, SkillData skill = null, Unit attacker = null)
    {
      if (!Object.op_Inequality((Object) this.mHPGauge, (Object) null))
        return;
      UnitGauge component = (UnitGauge) ((Component) this.mHPGauge).GetComponent<UnitGauge>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.Mode = (int) mode;
        this.OnUnitGaugeModeChange(mode);
      }
      if (mode == TacticsUnitController.HPGaugeModes.Attack)
      {
        if (skill == null || attacker == null)
          return;
        this.mHPGauge.Focus(skill.ElementType, (int) skill.ElementValue, attacker.Element, (int) attacker.CurrentStatus.element_assist[attacker.Element]);
      }
      else
        this.mHPGauge.DeactivateElementIcon();
    }

    public void OnUnitGaugeModeChange(TacticsUnitController.HPGaugeModes Mode)
    {
      if (!Object.op_Inequality((Object) this.mChargeIcon, (Object) null))
        return;
      if (this.Unit.CastSkill == null)
      {
        this.mChargeIcon.Close();
      }
      else
      {
        switch (Mode)
        {
          case TacticsUnitController.HPGaugeModes.Normal:
            this.mChargeIcon.Open();
            break;
          case TacticsUnitController.HPGaugeModes.Attack:
            this.mChargeIcon.Close();
            break;
          case TacticsUnitController.HPGaugeModes.Target:
            this.mChargeIcon.Open();
            break;
        }
      }
    }

    public void SetHPChangeYosou(int newHP)
    {
      if (!Object.op_Inequality((Object) this.mHPGauge, (Object) null) || !Object.op_Inequality((Object) this.mHPGauge.MainGauge, (Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      if (newHP < this.mCachedHP)
      {
        Color32[] colors;
        if (newHP <= 0)
        {
          colors = new Color32[1]
          {
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Damage : instance.Gauge_PlayerHP_Damage
          };
          colors[0].a = (__Null) (int) byte.MaxValue;
        }
        else
        {
          colors = new Color32[2]
          {
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base,
            this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Damage : instance.Gauge_PlayerHP_Damage
          };
          colors[0].a = (__Null) (int) (byte) (newHP * (int) byte.MaxValue / this.mCachedHP);
          colors[1].a = (__Null) (int) (byte) ((int) byte.MaxValue - colors[0].a);
        }
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else if (newHP > this.mCachedHP)
      {
        Color32[] colors = new Color32[2]{ this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base, this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Heal : instance.Gauge_PlayerHP_Heal };
        colors[0].a = (int) this.Unit.MaximumStatus.param.hp == 0 ? (__Null) 0 : (__Null) (int) (byte) (this.mCachedHP * (int) byte.MaxValue / (int) this.Unit.MaximumStatus.param.hp);
        colors[1].a = (__Null) (int) (byte) ((int) byte.MaxValue - colors[0].a);
        this.mHPGauge.MainGauge.Value = (int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : Mathf.Clamp01((float) newHP / (float) (int) this.Unit.MaximumStatus.param.hp);
        this.mHPGauge.MainGauge.UpdateColors(colors);
      }
      else
        this.ResetHPGauge();
    }

    public bool IsHPGaugeChanging
    {
      get
      {
        if (Object.op_Inequality((Object) this.mHPGauge, (Object) null) && ((Component) this.mHPGauge).get_gameObject().get_activeInHierarchy() && Object.op_Inequality((Object) this.mHPGauge.MainGauge, (Object) null))
          return this.mHPGauge.MainGauge.IsAnimating;
        return false;
      }
    }

    public RectTransform HPGaugeTransform
    {
      get
      {
        if (Object.op_Inequality((Object) this.mHPGauge, (Object) null))
          return (RectTransform) ((Component) this.mHPGauge).GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    public void ShowHPGauge(bool visible)
    {
      if (Object.op_Inequality((Object) this.mHPGauge, (Object) null))
        ((Component) this.mHPGauge).get_gameObject().SetActive(visible);
      this.mKeepHPGaugeVisible = -1f;
    }

    public void ResetHPGauge()
    {
      this.mCachedHP = (int) this.Unit.CurrentStatus.param.hp;
      if (!Object.op_Inequality((Object) this.mHPGauge, (Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      this.mHPGauge.MainGauge.Colors = new Color32[1]
      {
        this.Unit.Side != EUnitSide.Player ? instance.Gauge_EnemyHP_Base : instance.Gauge_PlayerHP_Base
      };
      this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, (int) this.Unit.MaximumStatus.param.hp, 0.0f);
    }

    public void UpdateHPRelative(int delta, float duration = 0.5f)
    {
      this.mCachedHP += delta;
      this.mCachedHP = Mathf.Clamp(this.mCachedHP, 0, (int) this.Unit.MaximumStatus.param.hp);
      if (Object.op_Equality((Object) this.mHPGauge, (Object) null))
        return;
      this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, (int) this.Unit.MaximumStatus.param.hp, duration);
      ((Component) this.mHPGauge).get_gameObject().SetActive(true);
      if ((double) this.mKeepHPGaugeVisible < 0.0)
        return;
      this.mKeepHPGaugeVisible = 1.5f;
    }

    private void UpdateGauges()
    {
      if (Object.op_Inequality((Object) this.mHPGauge, (Object) null) && (double) this.mKeepHPGaugeVisible >= 0.0 && ((Component) this.mHPGauge).get_gameObject().get_activeInHierarchy())
      {
        this.mKeepHPGaugeVisible -= Time.get_deltaTime();
        if ((double) this.mKeepHPGaugeVisible <= 0.0)
        {
          ((Component) this.mHPGauge).get_gameObject().SetActive(false);
          this.mKeepHPGaugeVisible = 0.0f;
        }
      }
      if (!Object.op_Inequality((Object) this.mAddIconGauge, (Object) null))
        return;
      this.mAddIconGauge.IsGaugeUpdate = this.IsHPGaugeChanging;
      this.mAddIconGauge.IsUnitDead = this.Unit.IsDead;
    }

    private void OnHitGaugeWeakRegist(Unit attacker)
    {
      if (this.mSkillVars == null || this.mSkillVars.Skill == null)
        return;
      SkillData skillData = this.mUnit.GetSkillData(this.mSkillVars.Skill.iname);
      if (skillData == null)
      {
        skillData = new SkillData();
        skillData.Setup(this.mSkillVars.Skill.iname, 1, 1, (MasterParam) null);
      }
      this.mHPGauge.ActivateElementIcon(true);
      this.mHPGauge.OnAttack(skillData.ElementType, (int) skillData.ElementValue, attacker.Element, (int) attacker.CurrentStatus.element_assist[attacker.Element]);
    }

    public void ShowOwnerIndexUI(bool show)
    {
      if (Object.op_Equality((Object) this.mOwnerIndexUI, (Object) null))
        return;
      this.mOwnerIndexUI.get_gameObject().SetActive(show);
    }

    public bool CreateOwnerIndexUI(Canvas canvas, GameObject templeteUI, JSON_MyPhotonPlayerParam param)
    {
      if (Object.op_Inequality((Object) this.mOwnerIndexUI, (Object) null))
        return true;
      if (Object.op_Equality((Object) canvas, (Object) null) || Object.op_Equality((Object) templeteUI, (Object) null) || param == null)
        return false;
      this.mOwnerIndexUI = (GameObject) Object.Instantiate<GameObject>((M0) templeteUI);
      if (Object.op_Equality((Object) this.mOwnerIndexUI, (Object) null))
        return false;
      DataSource.Bind<JSON_MyPhotonPlayerParam>(this.mOwnerIndexUI, param);
      UIProjector uiProjector = (UIProjector) ((Component) this).get_gameObject().AddComponent<UIProjector>();
      uiProjector.UIObject = this.mOwnerIndexUI.get_transform() as RectTransform;
      uiProjector.AutoDestroyUIObject = true;
      uiProjector.LocalOffset = Vector3.get_up();
      uiProjector.SetCanvas(canvas);
      return true;
    }

    public void ShowVersusCursor(bool show)
    {
      if (Object.op_Equality((Object) this.mVersusCursor, (Object) null))
        return;
      this.mVersusCursor.get_gameObject().SetActive(show);
    }

    public void PlayVersusCursor(bool play)
    {
      if (Object.op_Equality((Object) this.mVersusCursor, (Object) null) || Object.op_Equality((Object) this.mVersusCursorRoot, (Object) null))
        return;
      Animation component = (Animation) ((Component) this.mVersusCursorRoot).GetComponent<Animation>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).set_enabled(play);
    }

    public bool CreateVersusCursor(GameObject templeteUI)
    {
      if (Object.op_Inequality((Object) this.mVersusCursor, (Object) null))
        return true;
      if (Object.op_Equality((Object) templeteUI, (Object) null))
        return false;
      this.mVersusCursor = (GameObject) Object.Instantiate<GameObject>((M0) templeteUI);
      if (Object.op_Equality((Object) this.mVersusCursor, (Object) null))
        return false;
      this.mVersusCursor.get_transform().SetParent(((Component) this).get_gameObject().get_transform(), false);
      this.mVersusCursor.get_transform().set_localPosition(Vector3.op_Multiply(Vector3.get_up(), 1.3f));
      this.mVersusCursor.get_transform().set_localScale(new Vector3(0.4f, 0.4f, 0.4f));
      GameUtility.SetLayer(this.mVersusCursor, GameUtility.LayerUI, true);
      this.mVersusCursorRoot = this.mVersusCursor.get_transform().FindChild("Root");
      return true;
    }

    public void SetGimmickIcon(Unit TargetUnit)
    {
      if (TargetUnit == null || !Object.op_Inequality((Object) this.mAddIconGauge, (Object) null))
        return;
      if (TargetUnit.UnitType == EUnitType.Treasure)
        this.mAddIconGauge.ChangeAnimationByUnitType(TargetUnit.UnitType);
      if (TargetUnit.UnitType != EUnitType.Gem || TargetUnit.EventTrigger == null)
        return;
      this.mAddIconGauge.SetGemIcon(TargetUnit.EventTrigger.GimmickType);
    }

    public void HideGimmickIcon(EUnitType Type)
    {
      if (!Object.op_Inequality((Object) this.mAddIconGauge, (Object) null))
        return;
      this.mAddIconGauge.SetEndAnimationAll();
    }

    public void DeleteGimmickIconAll()
    {
      if (!Object.op_Inequality((Object) this.mAddIconGauge, (Object) null))
        return;
      this.mAddIconGauge.DeleteIconAll();
    }

    private TacticsUnitController.HideGimmickAnimation HideGimmickAnim
    {
      get
      {
        if (this.mHideGimmickAnim == null)
        {
          this.mHideGimmickAnim = new TacticsUnitController.HideGimmickAnimation();
          this.mHideGimmickAnim.Init(this);
        }
        return this.mHideGimmickAnim;
      }
      set
      {
      }
    }

    public void ScaleHide()
    {
      this.HideGimmickAnim.Enable = true;
      this.HideGimmickAnim.IsHide = true;
    }

    public void ScaleShow()
    {
      this.HideGimmickAnim.Enable = true;
      this.HideGimmickAnim.IsHide = false;
    }

    public void ResetScale()
    {
      this.HideGimmickAnim.ResetScale();
    }

    public bool IsA(string id)
    {
      if (this.Unit != null && this.Unit.UnitParam.iname == id)
        return true;
      return this.UniqueName == id;
    }

    public Unit Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    public Vector2 FieldActionPoint
    {
      get
      {
        return this.mFieldActionPoint;
      }
    }

    public bool TriggerFieldAction(Vector3 velocity, bool resetToMove = false)
    {
      Transform transform = ((Component) this).get_transform();
      this.mOnFieldActionEnd = !resetToMove ? new TacticsUnitController.FieldActionEndEvent(this.PlayIdleSmooth) : new TacticsUnitController.FieldActionEndEvent(this.MoveAgain);
      float num1 = Mathf.Floor((float) transform.get_position().x);
      float num2 = Mathf.Floor((float) transform.get_position().z);
      float num3 = num1 + 1f;
      float num4 = num2 + 1f;
      float num5 = float.MaxValue;
      EUnitDirection direction = EUnitDirection.NegativeX;
      this.mFieldActionPoint = new Vector2((float) ((Component) this).get_transform().get_position().x, (float) ((Component) this).get_transform().get_position().z);
      float num6 = 0.3f;
      if (velocity.x < 0.0)
      {
        float num7 = (float) transform.get_position().x - num1;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.NegativeX;
        }
      }
      else if (velocity.x > 0.0)
      {
        float num7 = num3 - (float) transform.get_position().x;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.PositiveX;
        }
      }
      if (velocity.z < 0.0)
      {
        float num7 = (float) transform.get_position().z - num2;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.NegativeY;
        }
      }
      else if (velocity.z > 0.0)
      {
        float num7 = num4 - (float) transform.get_position().z;
        if ((double) num7 < (double) num5)
        {
          num5 = num7;
          direction = EUnitDirection.PositiveY;
        }
      }
      if ((double) num5 <= 0.300000011920929)
      {
        IntVector2 offset = direction.ToOffset();
        int num7 = Mathf.FloorToInt((float) transform.get_position().x);
        int num8 = Mathf.FloorToInt((float) transform.get_position().z);
        int x = num7 + offset.x;
        int y1 = num8 + offset.y;
        if (this.mWalkableField != null && this.mWalkableField.isValid(x, y1) && this.mWalkableField.get(x, y1) >= 0)
        {
          float y2 = (float) GameUtility.RaycastGround(transform.get_position()).y;
          float num9 = GameUtility.RaycastGround((float) x + 0.5f, (float) y1 + 0.5f) - y2;
          switch (direction)
          {
            case EUnitDirection.PositiveX:
              this.mFieldActionPoint.x = (__Null) ((double) num3 + (double) num6);
              break;
            case EUnitDirection.PositiveY:
              this.mFieldActionPoint.y = (__Null) ((double) num4 + (double) num6);
              break;
            case EUnitDirection.NegativeX:
              this.mFieldActionPoint.x = (__Null) ((double) num1 - (double) num6);
              break;
            case EUnitDirection.NegativeY:
              this.mFieldActionPoint.y = (__Null) ((double) num2 - (double) num6);
              break;
          }
          GameSettings instance = GameSettings.Instance;
          this.mFieldActionDir = direction;
          if ((double) num9 <= (double) instance.Unit_FallAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionFall>();
            return true;
          }
          if ((double) num9 <= -(double) instance.Unit_StepAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJump>();
            return true;
          }
          if ((double) num9 >= (double) instance.Unit_JumpAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJumpUp>();
            return true;
          }
          if ((double) num9 >= (double) instance.Unit_StepAnimationThreshold)
          {
            this.GotoState<TacticsUnitController.State_FieldActionJump>();
            return true;
          }
        }
      }
      return false;
    }

    public bool IsPlayingFieldAction
    {
      get
      {
        return this.mStateMachine.IsInKindOfState<TacticsUnitController.State_FieldAction>();
      }
    }

    private void Awake()
    {
      this.mRunAnimation = "RUN";
      TacticsUnitController.Instances.Add(this);
      this.LoadEquipments = true;
      this.KeepUnitHidden = true;
      this.mStateMachine = new StateMachine<TacticsUnitController>(this);
    }

    private void SetMonochrome(bool enable)
    {
      if (enable)
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("MONOCHROME_ON");
          this.CharacterMaterials[index].DisableKeyword("MONOCHROME_OFF");
        }
      }
      else
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("MONOCHROME_OFF");
          this.CharacterMaterials[index].DisableKeyword("MONOCHROME_ON");
        }
      }
    }

    private void DisableColorBlending()
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].EnableKeyword("COLORBLEND_OFF");
        this.CharacterMaterials[index].DisableKeyword("COLORBLEND_ON");
      }
    }

    private void ApplyColorBlending(Color color)
    {
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].EnableKeyword("COLORBLEND_ON");
        this.CharacterMaterials[index].DisableKeyword("COLORBLEND_OFF");
        this.CharacterMaterials[index].SetColor("_blendColor", color);
      }
    }

    public void FadeBlendColor(Color color, float time)
    {
      this.mBlendMode = TacticsUnitController.ColorBlendModes.Fade;
      this.mBlendColor = color;
      this.mBlendColorTime = 0.0f;
      this.mBlendColorTimeMax = time;
    }

    private void UpdateColorBlending()
    {
      Vector3 position = ((Component) this).get_transform().get_position();
      StaticLightVolume volume = StaticLightVolume.FindVolume(position);
      Color directLit;
      Color indirectLit;
      if (Object.op_Equality((Object) volume, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        directLit = instance.Character_DefaultDirectLitColor;
        indirectLit = instance.Character_DefaultIndirectLitColor;
      }
      else
        volume.CalcLightColor(position, out directLit, out indirectLit);
      for (int index = 0; index < this.CharacterMaterials.Count; ++index)
      {
        this.CharacterMaterials[index].SetColor("_directLitColor", directLit);
        this.CharacterMaterials[index].SetColor("_indirectLitColor", indirectLit);
      }
      Color color;
      // ISSUE: explicit reference operation
      ((Color) @color).\u002Ector(0.0f, 0.0f, 0.0f, 0.0f);
      if (this.mBadStatus != null && this.mBadStatus.BlendColor.a > 0.0)
        color = Color.Lerp(this.mBadStatus.BlendColor, new Color(1f, 1f, 1f, 0.0f), (float) ((1.0 - (double) Mathf.Cos(Time.get_time() * 3.141593f)) * 0.5) * 0.5f);
      switch (this.mBlendMode)
      {
        case TacticsUnitController.ColorBlendModes.Fade:
          this.mBlendColorTime += Time.get_deltaTime();
          float num = Mathf.Clamp01(this.mBlendColorTime / this.mBlendColorTimeMax);
          if ((double) num >= 1.0)
          {
            this.mBlendMode = TacticsUnitController.ColorBlendModes.None;
            break;
          }
          color = Color.Lerp(this.mBlendColor, new Color(1f, 1f, 1f, 0.0f), num);
          break;
      }
      if (color.a > 0.0)
      {
        this.ApplyColorBlending(color);
        this.mEnableColorBlending = true;
      }
      else if (this.mEnableColorBlending)
      {
        this.mEnableColorBlending = false;
        this.DisableColorBlending();
      }
      if (this.ColorMod.r >= 1.0 && this.ColorMod.g >= 1.0 && this.ColorMod.b >= 1.0)
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
          this.CharacterMaterials[index].DisableKeyword("USE_COLORMOD");
      }
      else
      {
        for (int index = 0; index < this.CharacterMaterials.Count; ++index)
        {
          this.CharacterMaterials[index].EnableKeyword("USE_COLORMOD");
          this.CharacterMaterials[index].SetColor("_colorMod", this.ColorMod);
        }
      }
    }

    public bool HasCursor
    {
      get
      {
        return Object.op_Inequality((Object) this.mUnitCursor, (Object) null);
      }
    }

    private void GotoState<T>() where T : TacticsUnitController.State, new()
    {
      this.mStateMachine.GotoState<T>();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      GameUtility.DestroyGameObject((Component) this.mPet);
      GameUtility.DestroyGameObject((Component) this.mHPGauge);
      TacticsUnitController.Instances.Remove(this);
    }

    public void ShowCursor(UnitCursor prefab, Color color)
    {
      if (Object.op_Inequality((Object) this.mUnitCursor, (Object) null) || Object.op_Equality((Object) prefab, (Object) null))
        return;
      this.mUnitCursor = (UnitCursor) Object.Instantiate<UnitCursor>((M0) prefab);
      ((Component) this.mUnitCursor).get_transform().set_parent(((Component) this).get_transform());
      this.mUnitCursor.Color = color;
      ((Component) this.mUnitCursor).get_transform().set_localPosition(Vector3.op_Multiply(Vector3.get_up(), 0.3f));
    }

    public void CancelAction()
    {
      this.mCancelAction = true;
    }

    public void HideCursor(bool immediate = false)
    {
      if (Object.op_Equality((Object) this.mUnitCursor, (Object) null))
        return;
      if (immediate)
        GameUtility.DestroyGameObject((Component) this.mUnitCursor);
      else
        this.mUnitCursor.FadeOut();
      this.mUnitCursor = (UnitCursor) null;
    }

    public void SetupUnit(Unit unit)
    {
      this.mUnit = unit;
      this.mCachedHP = (int) unit.CurrentStatus.param.hp;
      this.UniqueName = unit.UniqueName;
      ((Component) this).get_transform().set_rotation(this.mUnit.Direction.ToRotation());
      this.SetupPet();
      this.SetupUnit(unit.UnitData, -1);
    }

    private void SetupPet()
    {
      if (this.mUnit.Job == null || string.IsNullOrEmpty(this.mUnit.Job.Param.pet))
        return;
      this.mPet = (PetController) new GameObject("Pet", new System.Type[1]
      {
        typeof (PetController)
      }).GetComponent<PetController>();
      this.mPet.Owner = ((Component) this).get_gameObject();
      this.mPet.PetID = this.mUnit.Job.Param.pet;
    }

    protected new void Start()
    {
      this.OnAnimationUpdate = new AnimationPlayer.AnimationUpdateEvent(this.AnimationUpdated);
      base.Start();
      if (this.Posture == TacticsUnitController.PostureTypes.Combat)
        this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_FIELD, true, false);
      else
        this.LoadUnitAnimationAsync("IDLE", TacticsUnitController.ANIM_IDLE_DEMO, false, false);
      this.GotoState<TacticsUnitController.State_WaitResources>();
    }

    public void LoadDefendSkillEffect(string skillEffectName)
    {
      DebugUtility.Log("LoadDefendSkillEffect: " + skillEffectName);
      if (string.IsNullOrEmpty(skillEffectName))
        return;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadDefendSkillEffectAsync(skillEffectName));
    }

    [DebuggerHidden]
    private IEnumerator LoadDefendSkillEffectAsync(string skillEffectName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadDefendSkillEffectAsync\u003Ec__Iterator10() { skillEffectName = skillEffectName, \u003C\u0024\u003EskillEffectName = skillEffectName, \u003C\u003Ef__this = this };
    }

    protected override void PostSetup()
    {
      GameSettings instance = GameSettings.Instance;
      float mapCharacterScale = instance.Quest.MapCharacterScale;
      this.RootMotionScale = mapCharacterScale;
      ((Component) this).get_transform().set_localScale(Vector3.op_Multiply(Vector3.get_one(), mapCharacterScale));
      CharacterSettings component = (CharacterSettings) this.UnitObject.GetComponent<CharacterSettings>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        Unit unit = this.Unit;
        if (unit != null)
        {
          if (unit.Side == EUnitSide.Enemy)
            this.VesselColor = instance.Character_EnemyGlowColor;
          else
            this.VesselColor = instance.Character_PlayerGlowColor;
          for (int index = 0; index < unit.BattleSkills.Count; ++index)
          {
            SkillParam skillParam = unit.BattleSkills[index].SkillParam;
            if (skillParam.type == ESkillType.Reaction && (skillParam.effect_type == SkillEffectTypes.Defend || skillParam.effect_type == SkillEffectTypes.DamageControl))
              this.LoadDefendSkillEffect(skillParam.defend_effect);
          }
        }
        component.SetSkeleton("small");
        if (Object.op_Inequality((Object) component.ShadowProjector, (Object) null))
        {
          GameObject gameObject = ((Component) component.ShadowProjector).get_gameObject();
          this.mShadow = (Projector) ((GameObject) Object.Instantiate((Object) gameObject, Vector3.op_Addition(((Component) this).get_transform().get_position(), gameObject.get_transform().get_position()), gameObject.get_transform().get_rotation())).GetComponent<Projector>();
          ((Component) this.mShadow).get_transform().SetParent(this.GetCharacterRoot(), true);
          this.mShadow.set_ignoreLayers(~(1 << LayerMask.NameToLayer("BG")));
          GameUtility.SetLayer((Component) this.mShadow, GameUtility.LayerHidden, true);
          Projector mShadow = this.mShadow;
          mShadow.set_orthographicSize(mShadow.get_orthographicSize() * mapCharacterScale);
        }
      }
      if (Object.op_Inequality((Object) this.mPet, (Object) null))
      {
        Transform transform1 = ((Component) this).get_transform();
        Transform transform2 = ((Component) this.mPet).get_transform();
        transform2.set_position(transform1.get_position());
        transform2.set_rotation(transform1.get_rotation());
      }
      if (this.Unit == null)
        return;
      this.CacheIcons();
    }

    public void SetRenkeiAura(GameObject eff)
    {
      if (Object.op_Inequality((Object) this.mRenkeiAuraEffect, (Object) null))
      {
        GameUtility.StopEmitters(this.mRenkeiAuraEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mRenkeiAuraEffect);
        this.mRenkeiAuraEffect = (GameObject) null;
      }
      if (!Object.op_Inequality((Object) eff, (Object) null))
        return;
      this.mRenkeiAuraEffect = Object.Instantiate((Object) eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
      this.mRenkeiAuraEffect.get_transform().SetParent(((Component) this).get_transform(), false);
    }

    public void StopRenkeiAura()
    {
      this.SetRenkeiAura((GameObject) null);
    }

    public void EnableChargeTargetUnit(GameObject eff, bool is_grn)
    {
      if (is_grn)
      {
        if (Object.op_Implicit((Object) this.mChargeGrnTargetUnitEffect) || !Object.op_Implicit((Object) eff))
          return;
        this.mChargeGrnTargetUnitEffect = Object.Instantiate((Object) eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
        if (!Object.op_Implicit((Object) this.mChargeGrnTargetUnitEffect))
          return;
        this.mChargeGrnTargetUnitEffect.get_transform().SetParent(((Component) this).get_transform(), false);
      }
      else
      {
        if (Object.op_Implicit((Object) this.mChargeRedTargetUnitEffect) || !Object.op_Implicit((Object) eff))
          return;
        this.mChargeRedTargetUnitEffect = Object.Instantiate((Object) eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
        if (!Object.op_Implicit((Object) this.mChargeRedTargetUnitEffect))
          return;
        this.mChargeRedTargetUnitEffect.get_transform().SetParent(((Component) this).get_transform(), false);
      }
    }

    public void DisableChargeTargetUnit()
    {
      if (Object.op_Implicit((Object) this.mChargeGrnTargetUnitEffect))
      {
        GameUtility.StopEmitters(this.mChargeGrnTargetUnitEffect);
        GameUtility.RequireComponent<OneShotParticle>(this.mChargeGrnTargetUnitEffect);
        this.mChargeGrnTargetUnitEffect = (GameObject) null;
      }
      if (!Object.op_Implicit((Object) this.mChargeRedTargetUnitEffect))
        return;
      GameUtility.StopEmitters(this.mChargeRedTargetUnitEffect);
      GameUtility.RequireComponent<OneShotParticle>(this.mChargeRedTargetUnitEffect);
      this.mChargeRedTargetUnitEffect = (GameObject) null;
    }

    public void SetDrainEffect(GameObject eff)
    {
      if (!Object.op_Inequality((Object) eff, (Object) null))
        return;
      Transform transform = GameUtility.findChildRecursively(((Component) this).get_transform(), "Bip001");
      if (Object.op_Equality((Object) transform, (Object) null))
        transform = ((Component) this).get_transform();
      this.mDrainEffect = Object.Instantiate((Object) eff, ((Component) this).get_transform().get_position(), ((Component) this).get_transform().get_rotation()) as GameObject;
      this.mDrainEffect.get_transform().SetParent(transform, false);
      this.mDrainEffect.RequireComponent<OneShotParticle>();
      this.mDrainEffect.SetActive(false);
    }

    public void BeginLoadPickupAnimation()
    {
      this.LoadUnitAnimationAsync("PICK", TacticsUnitController.ANIM_PICKUP, false, false);
    }

    public void UnloadPickupAnimation()
    {
      this.UnloadAnimation("PICK");
    }

    public void LoadGenkidamaAnimation(bool load)
    {
      if (load)
        this.LoadUnitAnimationAsync("GENK", TacticsUnitController.ANIM_GENKIDAMA, false, false);
      else
        this.UnloadAnimation("GENK");
    }

    public void PlayGenkidama()
    {
      this.PlayAnimation("GENK", true, 0.2f, 0.0f);
    }

    public void PlayPickup(GameObject pickupObject)
    {
      this.mPickupObject = pickupObject;
      this.GotoState<TacticsUnitController.State_Pickup>();
    }

    protected override void Update()
    {
      base.Update();
      if (this.mStateMachine != null)
        this.mStateMachine.Update();
      this.UpdateGauges();
      this.HideGimmickAnim.Update();
      this.execKnockBack();
    }

    public static EUnitDirection CalcUnitDirection(float x, float y)
    {
      float num = (float) ((57.2957801818848 * (double) Mathf.Atan2(y, x) + 360.0) % 360.0);
      if (325.0 <= (double) num || (double) num < 45.0)
        return EUnitDirection.PositiveX;
      if (45.0 <= (double) num && (double) num < 135.0)
        return EUnitDirection.PositiveY;
      return 135.0 <= (double) num && (double) num < 225.0 ? EUnitDirection.NegativeX : EUnitDirection.NegativeY;
    }

    public EUnitDirection CalcUnitDirectionFromRotation()
    {
      Transform transform = ((Component) this).get_transform();
      return TacticsUnitController.CalcUnitDirection((float) transform.get_forward().x, (float) transform.get_forward().z);
    }

    private bool CheckCollision(int x0, int y0, int x1, int y1)
    {
      if (!this.mWalkableField.isValid(x1, y1))
        return true;
      int num1 = this.mWalkableField.get(x0, y0);
      int num2 = this.mWalkableField.get(x1, y1);
      if (num2 >= 0)
        return BattleMap.MAX_GRID_MOVING < Mathf.Abs(num1 - num2);
      return true;
    }

    private void AnimationUpdated(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mCharacterSettings, (Object) null))
        return;
      this.mCharacterSettings.SetSkeleton("small");
    }

    public bool CollideGround
    {
      get
      {
        return this.mCollideGround;
      }
      set
      {
        this.mCollideGround = value;
      }
    }

    public void SnapToGround()
    {
      Transform transform = ((Component) this).get_transform();
      Vector3 position = transform.get_position();
      position.y = GameUtility.RaycastGround(position).y;
      transform.set_position(position);
    }

    protected override void LateUpdate()
    {
      base.LateUpdate();
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @this.mVelocity).get_sqrMagnitude() > 0.0)
      {
        Transform transform = ((Component) this).get_transform();
        transform.set_position(Vector3.op_Addition(transform.get_position(), Vector3.op_Multiply(this.mVelocity, Time.get_deltaTime())));
      }
      if (this.mCollideGround)
        this.SnapToGround();
      this.UpdateColorBlending();
    }

    public GridMap<int> WalkableField
    {
      set
      {
        this.mWalkableField = value;
      }
    }

    public float StartMove(Vector3[] route, float directionAngle = -1)
    {
      if (this.IgnoreMove)
      {
        this.IgnoreMove = false;
        return 0.0f;
      }
      ((Component) this).get_transform().set_position(route[0]);
      this.mPostMoveAngle = directionAngle;
      this.mRoute = route;
      this.mRoutePos = 0;
      this.GotoState<TacticsUnitController.State_Move>();
      float num1 = 0.0f;
      for (int index = 1; index < route.Length; ++index)
      {
        double num2 = (double) num1;
        Vector3 vector3 = Vector3.op_Subtraction(route[index - 1], route[index]);
        // ISSUE: explicit reference operation
        double magnitude = (double) ((Vector3) @vector3).get_magnitude();
        num1 = (float) (num2 + magnitude);
      }
      return num1 / this.mRunSpeed;
    }

    public void SetRunningSpeed(float speed)
    {
      this.mRunSpeed = speed;
    }

    public void StartRunning()
    {
      if (this.mStateMachine.IsInState<TacticsUnitController.State_RunLoop>())
        return;
      this.GotoState<TacticsUnitController.State_RunLoop>();
    }

    public void PlayIdle(float interpTime = 0.0f)
    {
      this.mIdleInterpTime = interpTime;
      this.GotoState<TacticsUnitController.State_Idle>();
    }

    public void StopRunning()
    {
      if (this.mStateMachine.IsInState<TacticsUnitController.State_Idle>())
        return;
      this.PlayIdle(0.0f);
    }

    public void PlayTakenAnimation()
    {
      this.GotoState<TacticsUnitController.State_Taken>();
    }

    public bool isIdle
    {
      get
      {
        if (!this.mStateMachine.IsInState<TacticsUnitController.State_Idle>())
          return this.mStateMachine.IsInState<TacticsUnitController.State_Down>();
        return true;
      }
    }

    public bool isMoving
    {
      get
      {
        if (!this.mStateMachine.IsInState<TacticsUnitController.State_Move>())
          return this.mStateMachine.IsInState<TacticsUnitController.State_RunLoop>();
        return true;
      }
    }

    protected override void OnVisibilityChange(bool visible)
    {
      if (!visible || !Object.op_Inequality((Object) this.mShadow, (Object) null))
        return;
      ((Component) this.mShadow).get_gameObject().set_layer(GameUtility.LayerDefault);
    }

    public bool IsLoadedPartially
    {
      get
      {
        return this.mLoadedPartially;
      }
    }

    public void ResetRotation()
    {
      ((Component) this).get_transform().set_rotation(this.mUnit.Direction.ToRotation());
    }

    private void UpdateRotation()
    {
      if (this.mUnit == null)
        return;
      ((Component) this).get_transform().set_rotation(Quaternion.Slerp(((Component) this).get_transform().get_rotation(), this.mUnit.Direction.ToRotation(), Time.get_deltaTime() * 10f));
    }

    public void LookAt(Vector3 position)
    {
      Transform transform = ((Component) this).get_transform();
      Vector3 vector3 = Vector3.op_Subtraction(position, transform.get_position());
      vector3.y = (__Null) 0.0;
      transform.set_rotation(Quaternion.LookRotation(vector3));
    }

    public void LookAt(Component target)
    {
      this.LookAt(target.get_transform().get_position());
    }

    public void PlayLookAt(Component target, float spin = 2)
    {
      this.PlayLookAt(target.get_transform().get_position(), spin);
    }

    public void PlayLookAt(Vector3 target, float spin = 2)
    {
      this.mSpinCount = spin;
      this.mLookAtTarget = target;
      this.GotoState<TacticsUnitController.State_LookAt>();
    }

    private void PlayIdleSmooth()
    {
      this.PlayIdle(0.1f);
    }

    private void MoveAgain()
    {
      this.GotoState<TacticsUnitController.State_Move>();
    }

    public void StepTo(Vector3 dest)
    {
      this.mStepStart = ((Component) this).get_transform().get_position();
      this.mStepEnd = dest;
      Vector3 vector3 = Vector3.op_Subtraction(this.mStepEnd, this.mStepStart);
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @vector3).get_magnitude() <= (double) GameSettings.Instance.Quest.AnimateGridSnapRadius)
        this.GotoState<TacticsUnitController.State_StepNoAnimation>();
      else
        this.GotoState<TacticsUnitController.State_Step>();
    }

    public void UpdateBadStatus()
    {
      if (BadStatusEffects.Effects == null || this.Unit == null)
        return;
      BadStatusEffects.Desc desc = (BadStatusEffects.Desc) null;
      bool flag = false;
      for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
      {
        if (this.Unit.IsUnitCondition(BadStatusEffects.Effects[index].Key))
        {
          if (desc == null)
            desc = BadStatusEffects.Effects[index];
          if (this.Unit.IsCurseUnitCondition(BadStatusEffects.Effects[index].Key))
            flag = true;
        }
      }
      if (!this.Unit.IsUnitCondition(EUnitCondition.DeathSentence))
      {
        if (Object.op_Inequality((Object) this.mDeathSentenceIcon, (Object) null))
          this.mDeathSentenceIcon.Close();
      }
      else
        this.DeathSentenceCountDown(false, 0.0f);
      if (this.IsCursed != flag && Object.op_Inequality((Object) BadStatusEffects.CurseEffect, (Object) null))
      {
        if (flag)
        {
          this.mCurseEffect = (GameObject) Object.Instantiate<GameObject>((M0) BadStatusEffects.CurseEffect);
          Transform transform = (Transform) null;
          if (!string.IsNullOrEmpty(BadStatusEffects.CurseEffectAttachTarget))
            transform = GameUtility.findChildRecursively(((Component) this).get_transform(), BadStatusEffects.CurseEffectAttachTarget);
          if (Object.op_Equality((Object) transform, (Object) null))
            transform = this.GetCharacterRoot();
          this.mCurseEffect.get_transform().SetParent(transform, false);
        }
        else
        {
          GameUtility.StopEmitters(this.mCurseEffect);
          this.mCurseEffect.AddComponent<OneShotParticle>();
          this.mCurseEffect = (GameObject) null;
        }
      }
      this.IsCursed = flag;
      if (desc == this.mBadStatus)
        return;
      if (Object.op_Inequality((Object) this.mBadStatusEffect, (Object) null))
      {
        GameUtility.StopEmitters(this.mBadStatusEffect);
        this.mBadStatusEffect.AddComponent<OneShotParticle>();
        this.mBadStatusEffect = (GameObject) null;
      }
      this.mBadStatus = desc;
      if (this.mBadStatus != null)
      {
        if (Object.op_Inequality((Object) this.mBadStatus.Effect, (Object) null))
        {
          this.mBadStatusEffect = (GameObject) Object.Instantiate<GameObject>((M0) this.mBadStatus.Effect);
          Transform transform1 = this.mBadStatusEffect.get_transform();
          Transform transform2 = (Transform) null;
          if (!string.IsNullOrEmpty(this.mBadStatus.AttachTarget))
            transform2 = GameUtility.findChildRecursively(((Component) this).get_transform(), this.mBadStatus.AttachTarget);
          if (Object.op_Equality((Object) transform2, (Object) null))
            transform2 = this.GetCharacterRoot();
          transform1.SetParent(transform2, false);
        }
        if (!string.IsNullOrEmpty(this.mBadStatus.AnimationName) && string.IsNullOrEmpty(this.mLoadedBadStatusAnimation))
        {
          this.mLoadedBadStatusAnimation = this.mBadStatus.AnimationName;
          this.LoadUnitAnimationAsync("BAD", this.mBadStatus.AnimationName, false, false);
        }
        if (this.Unit.IsUnitCondition(EUnitCondition.DeathSentence))
          this.mDeathSentenceIcon.Open();
      }
      this.SetMonochrome(this.mBadStatus != null && this.mBadStatus.Key == EUnitCondition.Stone);
    }

    public bool IsDeathSentenceCountDownPlaying()
    {
      if (Object.op_Inequality((Object) this.mDeathSentenceIcon, (Object) null))
        return this.mDeathSentenceIcon.IsDeathSentenceCountDownPlaying;
      return false;
    }

    public void DeathSentenceCountDown(bool isShow, float LifeTime = 0.0f)
    {
      if (isShow)
        this.ShowHPGauge(true);
      if (!Object.op_Inequality((Object) this.mDeathSentenceIcon, (Object) null))
        return;
      this.mDeathSentenceIcon.Open();
      this.mDeathSentenceIcon.Countdown(Mathf.Max(this.Unit.DeathCount, 0), LifeTime);
    }

    public void UpdateShields()
    {
      if (this.mUnit == null)
        return;
      int count = this.Shields.Count;
      for (int index = 0; index < count; ++index)
      {
        if ((int) this.Shields[index].Target.hp != this.Shields[index].LastHP || (int) this.Shields[index].Target.turn != this.Shields[index].LastTurn)
          this.Shields[index].Dirty = true;
      }
      List<Unit.UnitShield> shields = this.mUnit.Shields;
      for (int index1 = 0; index1 < shields.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (this.Shields[index2].Target == shields[index1])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.Shields.Add(new TacticsUnitController.ShieldState()
          {
            Target = shields[index1],
            LastHP = (int) shields[index1].hp,
            LastTurn = (int) shields[index1].turn
          });
      }
    }

    public void RemoveObsoleteShieldStates()
    {
      if (this.mUnit == null)
        return;
      for (int index = this.Shields.Count - 1; index >= 0; --index)
      {
        if (!this.mUnit.Shields.Contains(this.Shields[index].Target))
          this.Shields.RemoveAt(index);
      }
    }

    private void ShowCriticalEffect(Vector3 position, float yOffset)
    {
      Camera main = Camera.get_main();
      if (Object.op_Inequality((Object) main, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        FlashEffect flashEffect = (FlashEffect) ((Component) main).get_gameObject().AddComponent<FlashEffect>();
        flashEffect.Strength = instance.CriticalHit_FlashStrength;
        flashEffect.Duration = instance.CriticalHit_FlashDuration;
        CameraShakeEffect cameraShakeEffect = (CameraShakeEffect) ((Component) main).get_gameObject().AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = instance.CriticalHit_ShakeDuration;
        cameraShakeEffect.AmplitudeX = instance.CriticalHit_ShakeAmplitudeX;
        cameraShakeEffect.AmplitudeY = instance.CriticalHit_ShakeAmplitudeY;
        cameraShakeEffect.FrequencyX = instance.CriticalHit_ShakeFrequencyX;
        cameraShakeEffect.FrequencyY = instance.CriticalHit_ShakeFrequencyY;
      }
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        return;
      SceneBattle.Instance.PopupCritical(position, yOffset);
    }

    private void DrainGems(TacticsUnitController goal)
    {
      if (this.GemDrainEffects == null || this.DrainGemsOnHit <= 0)
        return;
      int num = this.DrainGemsOnHit + Random.Range(0, GameSettings.Instance.Gem_DrainCount_Randomness);
      Transform transform = ((Component) goal).get_transform();
      Vector3 vector3_1 = Vector3.op_Subtraction(((Component) this).get_transform().get_position(), transform.get_position());
      Vector3 centerPosition = this.CenterPosition;
      Vector3 vector3_2 = Vector3.op_Subtraction(goal.CenterPosition, transform.get_position());
      for (int index1 = 0; index1 < num; ++index1)
      {
        Quaternion quaternion = Quaternion.op_Multiply(Quaternion.AngleAxis((float) Random.Range(135, 225), vector3_1), Quaternion.AngleAxis((float) Random.Range(70, 110), Vector3.get_right()));
        for (int index2 = 0; index2 < this.GemDrainEffects.Length; ++index2)
        {
          if (!((Component) this.GemDrainEffects[index2]).get_gameObject().get_activeInHierarchy())
          {
            this.GemDrainEffects[index2].Reset();
            ((Component) this.GemDrainEffects[index2]).get_gameObject().SetActive(true);
            ((Component) this.GemDrainEffects[index2]).get_transform().set_position(centerPosition);
            ((Component) this.GemDrainEffects[index2]).get_transform().set_rotation(quaternion);
            this.GemDrainEffects[index2].TargetObject = transform;
            this.GemDrainEffects[index2].TargetOffset = vector3_2;
            if (Object.op_Inequality((Object) this.GemDrainHitEffect, (Object) null))
            {
              if (!GemParticleHitEffect.IsEnable)
                GemParticleHitEffect.IsEnable = true;
              ((Component) this.GemDrainEffects[index2]).get_gameObject().AddComponent<GemParticleHitEffect>();
              ((GemParticleHitEffect) ((Component) this.GemDrainEffects[index2]).get_gameObject().GetComponent<GemParticleHitEffect>()).EffectPrefab = this.GemDrainHitEffect;
              break;
            }
            break;
          }
        }
      }
    }

    public void InitShake(Vector3 basePosition, Vector3 direction)
    {
      if (this.mShaker == null)
        this.mShaker = new TacticsUnitController.ShakeUnit();
      this.mShaker.Init(basePosition, direction);
    }

    public bool ShakeStart
    {
      set
      {
        this.mShaker.ShakeStart = true;
      }
      get
      {
        return this.mShaker.ShakeStart;
      }
    }

    public bool IsShakeEnd()
    {
      return this.mShaker.ShakeCount <= 0;
    }

    public float GetShakeProgress()
    {
      return (float) (1 - this.mShaker.ShakeCount / this.mShaker.ShakeMaxCount);
    }

    public void AdvanceShake()
    {
      ((Component) this).get_transform().set_localPosition(this.mShaker.AdvanceShake());
    }

    public void SkillTurn(LogSkill Act, EUnitDirection AxisDirection)
    {
      if (this.mSkillVars == null || this.mSkillVars.mSkillSequence == null)
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      Vector3 vector3 = instance.CalcGridCenter(Act.self.x, Act.self.y);
      Vector3.get_zero();
      IntVector2 intVector2 = new IntVector2();
      Vector3 position;
      if (Act.CauseOfReaction != null)
      {
        position = instance.CalcGridCenter(Act.CauseOfReaction.x, Act.CauseOfReaction.y);
        intVector2.x = Act.CauseOfReaction.x;
        intVector2.y = Act.CauseOfReaction.y;
      }
      else
      {
        position = instance.CalcGridCenter(Act.pos.x, Act.pos.y);
        intVector2.x = Act.pos.x;
        intVector2.y = Act.pos.y;
      }
      switch (this.mSkillVars.mSkillSequence.SkillTurnType)
      {
        case SkillSequence.SkillTurnTypes.Target:
          if (Act.self.x == intVector2.x && Act.self.y == intVector2.y)
            break;
          this.LookAt(position);
          break;
        case SkillSequence.SkillTurnTypes.Axis:
          position.x = (__Null) (vector3.x + (double) Unit.DIRECTION_OFFSETS[(int) AxisDirection, 0]);
          position.z = (__Null) (vector3.z + (double) Unit.DIRECTION_OFFSETS[(int) AxisDirection, 1]);
          this.LookAt(position);
          break;
      }
    }

    public void SetArrowTrajectoryHeight(float Height)
    {
      this.mMapTrajectoryHeight = GameUtility.InternalToMapHeight(Height);
    }

    private bool HitTimerExists(TacticsUnitController target)
    {
      for (int index = 0; index < this.mSkillVars.HitTimers.Count; ++index)
      {
        if (Object.op_Equality((Object) this.mSkillVars.HitTimers[index].Target, (Object) target))
          return true;
      }
      return false;
    }

    public bool IsStartSkill()
    {
      return this.mSkillVars != null;
    }

    public SkillSequence.MapCameraTypes GetSkillCameraType()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null)
        return this.mSkillVars.mSkillSequence.MapCameraType;
      return SkillSequence.MapCameraTypes.None;
    }

    public bool IsSkillMirror()
    {
      if (this.mSkillVars != null && this.mSkillVars.mSkillSequence != null && !this.mSkillVars.mSkillSequence.NotMirror)
        return this.Unit.Side == EUnitSide.Enemy;
      return false;
    }

    public void LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes mask)
    {
      if ((mask & TacticsUnitController.DeathAnimationTypes.Normal) == (TacticsUnitController.DeathAnimationTypes) 0 || !Object.op_Equality((Object) this.FindAnimation("B_DEAD"), (Object) null))
        return;
      this.LoadUnitAnimationAsync("B_DEAD", "cmn_downstand0", false, false);
    }

    public void PlayDead(TacticsUnitController.DeathAnimationTypes type)
    {
      this.GotoState<TacticsUnitController.State_Dead>();
    }

    private void CalcCameraPos(AnimationClip clip, float normalizedTime, int cameraIndex, out Vector3 pos, out Quaternion rotation)
    {
      pos = Vector3.get_zero();
      rotation = Quaternion.get_identity();
      GameObject gameObject1 = new GameObject();
      GameObject gameObject2 = new GameObject("Camera001");
      GameObject gameObject3 = new GameObject("Camera002");
      Transform transform1 = gameObject1.get_transform();
      Transform transform2 = gameObject2.get_transform();
      Transform transform3 = gameObject3.get_transform();
      transform1.SetParent(((Component) this).get_transform(), false);
      transform2.SetParent(transform1, false);
      transform3.SetParent(transform1, false);
      clip.SampleAnimation(gameObject1, normalizedTime * clip.get_length());
      Transform transform4 = cameraIndex == 0 ? transform2 : transform3;
      pos = Vector3.op_Addition(Vector3.op_Subtraction(transform4.get_position(), ((Component) this).get_transform().get_position()), this.RootMotionInverse);
      rotation = transform4.get_rotation();
      Object.DestroyImmediate((Object) gameObject1);
      rotation = Quaternion.op_Multiply(rotation, GameUtility.Yaw180);
    }

    private void SetActiveCameraPosition(Vector3 position, Quaternion rotation)
    {
      Transform transform = ((Component) this.mSkillVars.mActiveCamera).get_transform();
      transform.set_position(position);
      transform.set_rotation(rotation);
    }

    private void AnimateCamera(AnimationClip cameraClip, float normalizedTime, int cameraIndex = 0)
    {
      this.AnimateCameraInterpolated(cameraClip, normalizedTime, 1f, TacticsUnitController.CameraState.Default, cameraIndex);
    }

    private void AnimateCameraInterpolated(AnimationClip cameraClip, float normalizedTime, float interpRate, TacticsUnitController.CameraState startState, int cameraIndex = 0)
    {
      Vector3 pos;
      Quaternion rotation;
      this.CalcCameraPos(cameraClip, normalizedTime, cameraIndex, out pos, out rotation);
      Vector3 position = Vector3.op_Addition(((Component) this).get_transform().get_position(), pos);
      Quaternion quaternion = rotation;
      if ((double) interpRate < 1.0)
      {
        position = Vector3.Lerp(startState.Position, position, interpRate);
        quaternion = Quaternion.Slerp(startState.Rotation, quaternion, interpRate);
      }
      this.SetActiveCameraPosition(position, Quaternion.op_Multiply(this.mSkillVars.mCameraShakeOffset, quaternion));
    }

    public void PlayDamageReaction(int damage, HitReactionTypes hitType = HitReactionTypes.Normal)
    {
      if (this.ShouldDefendHits || damage <= 0)
        return;
      this.PlayDamage(hitType);
    }

    [DebuggerHidden]
    private IEnumerator ShowHitPopup(TacticsUnitController target, bool critical, bool backstab, bool guard, bool weak, bool resist, bool hpDamage, int hpDamageValue, bool mpDamage, int mpDamageValue, bool absorb)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHitPopup\u003Ec__Iterator11() { guard = guard, target = target, critical = critical, backstab = backstab, weak = weak, resist = resist, absorb = absorb, hpDamage = hpDamage, hpDamageValue = hpDamageValue, mpDamage = mpDamage, mpDamageValue = mpDamageValue, \u003C\u0024\u003Eguard = guard, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Ecritical = critical, \u003C\u0024\u003Ebackstab = backstab, \u003C\u0024\u003Eweak = weak, \u003C\u0024\u003Eresist = resist, \u003C\u0024\u003Eabsorb = absorb, \u003C\u0024\u003EhpDamage = hpDamage, \u003C\u0024\u003EhpDamageValue = hpDamageValue, \u003C\u0024\u003EmpDamage = mpDamage, \u003C\u0024\u003EmpDamageValue = mpDamageValue, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator ShowHealPopup(TacticsUnitController target, int hpHeal, int mpHeal)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CShowHealPopup\u003Ec__Iterator12() { hpHeal = hpHeal, target = target, mpHeal = mpHeal, \u003C\u0024\u003EhpHeal = hpHeal, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003EmpHeal = mpHeal, \u003C\u003Ef__this = this };
    }

    private void HitTarget(TacticsUnitController target, HitReactionTypes hitReaction = HitReactionTypes.None)
    {
      bool flag1 = target.mHitInfo.hits.Count >= 2;
      bool isLast = this.mSkillVars.NumHitsLeft == 0;
      bool flag2 = !this.mSkillVars.UseBattleScene;
      bool flag3 = false;
      bool absorb = target.mHitInfo.shieldDamage > 0;
      int num1 = 0;
      int hpDamageValue;
      int mpDamageValue;
      if (flag1)
      {
        int index = target.mHitInfo.hits.Count - 1 - this.mSkillVars.NumHitsLeft;
        hpDamageValue = target.mHitInfo.hits[index].hp_damage;
        mpDamageValue = target.mHitInfo.hits[index].mp_damage;
      }
      else
      {
        hpDamageValue = target.mHitInfo.GetTotalHpDamage();
        mpDamageValue = target.mHitInfo.GetTotalMpDamage();
      }
      if (flag2 && hpDamageValue < 1 && num1 < 1)
        flag2 = true;
      int totalHpHeal = target.mHitInfo.GetTotalHpHeal();
      int totalMpHeal = target.mHitInfo.GetTotalMpHeal();
      if (hpDamageValue >= 1 || mpDamageValue >= 1)
        target.PlayDamageReaction(1, hitReaction);
      if (target.mKnockBackGrid != null && isLast)
        target.mKnockBackMode = TacticsUnitController.eKnockBackMode.START;
      if (this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw && Object.op_Inequality((Object) target, (Object) this) && this.mSkillVars.NumHitsLeft == this.mSkillVars.TotalHits - 1)
      {
        target.AutoUpdateRotation = false;
        target.LookAt(((Component) this).get_transform().get_position());
      }
      target.DrainGems(this);
      this.StartCoroutine(this.ShowHitPopup(target, target.ShowCriticalEffectOnHit, target.ShowBackstabEffectOnHit && Object.op_Inequality((Object) SceneBattle.Instance, (Object) null), target.ShouldDefendHits, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.Effective, target.ShowElementEffectOnHit == TacticsUnitController.EElementEffectTypes.NotEffective, false, 0, false, 0, absorb));
      if (!target.ShouldDefendHits && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
        this.SpawnHitEffect(((Component) target).get_transform().get_position(), isLast);
      if (!flag1 && !isLast)
        return;
      bool flag4 = false;
      if (this.mSkillVars.Skill.IsDamagedSkill() && !target.ShouldDodgeHits)
      {
        bool hpDamage = hpDamageValue >= 1;
        bool mpDamage = mpDamageValue >= 1;
        this.StartCoroutine(this.ShowHitPopup(target, false, false, false, false, false, hpDamage, hpDamageValue, mpDamage, mpDamageValue, false));
        if (mpDamage && isLast)
          flag3 = true;
        if (flag2 && hpDamage)
        {
          float duration = !isLast ? 0.1f : 0.5f;
          target.UpdateHPRelative(-hpDamageValue, duration);
          target.OnHitGaugeWeakRegist(this.mUnit);
          target.ChargeIcon.Close();
          if (target.mHitInfo.IsDefend())
          {
            if (this.mSkillVars.TotalHits <= 1 || this.mSkillVars.TotalHits - this.mSkillVars.NumHitsLeft <= 1)
              target.Unit.PlayBattleVoice("battle_0016");
          }
          else
          {
            int num2 = Mathf.Max(hpDamageValue, mpDamageValue);
            if (this.mSkillVars.TotalHits > 1)
              num2 /= this.mSkillVars.TotalHits;
            float num3 = (int) target.Unit.MaximumStatus.param.hp == 0 ? 1f : 1f * (float) num2 / (float) (int) target.Unit.MaximumStatus.param.hp;
            if ((double) num3 >= 0.75)
              target.Unit.PlayBattleVoice("battle_0036");
            else if ((double) num3 > 0.5)
              target.Unit.PlayBattleVoice("battle_0035");
            else
              target.Unit.PlayBattleVoice("battle_0034");
          }
        }
      }
      this.StartCoroutine(this.ShowHealPopup(target, totalHpHeal, totalMpHeal));
      if (totalHpHeal >= 1 && flag2)
      {
        target.UpdateHPRelative(totalHpHeal, 0.5f);
        if (!flag4 && this.Unit != target.Unit && totalHpHeal > 0)
        {
          target.Unit.PlayBattleVoice("battle_0018");
          flag4 = true;
        }
      }
      if (totalMpHeal >= 1)
      {
        flag3 = true;
        if (!flag4 && this.Unit != target.Unit)
        {
          target.Unit.PlayBattleVoice("battle_0018");
          flag4 = true;
        }
      }
      if (target.mHitInfo.IsBuffEffect())
      {
        SceneBattle.Instance.PopupParamChange(target.CenterPosition, target.mHitInfo.buff, target.mHitInfo.debuff);
        if (!flag4 && this.Unit != target.Unit && target.mHitInfo.buff.CheckEffect())
          target.Unit.PlayBattleVoice("battle_0018");
      }
      if (!flag3)
        return;
      UnitQueue.Instance.Refresh(target.Unit);
    }

    public void SkillEffectSelf()
    {
      if ((this.mHitInfoSelf == null || this.mHitInfoSelf.hits.Count <= 0) && (!this.mHitInfoSelf.buff.CheckEffect() && !this.mHitInfoSelf.debuff.CheckEffect()))
        return;
      bool flag1 = !this.mSkillVars.UseBattleScene;
      int totalHpDamage = this.mHitInfoSelf.GetTotalHpDamage();
      int totalMpDamage = this.mHitInfoSelf.GetTotalMpDamage();
      int totalHpHeal = this.mHitInfoSelf.GetTotalHpHeal();
      int totalMpHeal = this.mHitInfoSelf.GetTotalMpHeal();
      int number = totalHpDamage + this.mSkillVars.HpCostDamage;
      if (flag1 && totalHpDamage < 1 && totalHpHeal < 1)
        flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.mHitInfoSelf.debuff.bits.Length; ++index)
      {
        if (this.mHitInfoSelf.debuff.bits[index] != 0)
        {
          flag2 = true;
          break;
        }
      }
      if (number >= 1 || totalMpDamage >= 1 || flag2)
        this.PlayDamageReaction(1, HitReactionTypes.Normal);
      if (number > 0)
      {
        SceneBattle.Instance.PopupDamageNumber(this.CenterPosition, number);
        if (flag1)
        {
          this.UpdateHPRelative(-number, 0.5f);
          this.OnHitGaugeWeakRegist(this.mUnit);
          this.ChargeIcon.Close();
        }
      }
      if (totalHpHeal >= 1 || totalMpHeal >= 1)
      {
        SceneBattle.Instance.StartCoroutine(this.ShowHealPopup(this, totalHpHeal, totalMpHeal));
        if (Object.op_Inequality((Object) this.mDrainEffect, (Object) null))
        {
          this.mDrainEffect.get_transform().set_position(this.CenterPosition);
          this.mDrainEffect.get_transform().set_rotation(((Component) this).get_transform().get_rotation());
          this.mDrainEffect.SetActive(true);
        }
        if (flag1)
        {
          this.UpdateHPRelative(totalHpHeal, 0.5f);
          this.ChargeIcon.Close();
        }
      }
      if (this.mHitInfoSelf.IsBuffEffect())
        SceneBattle.Instance.StartCoroutine(this.DelayedPopupParamChange(this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff));
      this.UpdateBadStatus();
    }

    [DebuggerHidden]
    private IEnumerator DelayedPopupParamChange(BuffBit buff, BuffBit debuff)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CDelayedPopupParamChange\u003Ec__Iterator13() { buff = buff, debuff = debuff, \u003C\u0024\u003Ebuff = buff, \u003C\u0024\u003Edebuff = debuff, \u003C\u003Ef__this = this };
    }

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      if (this.mSkillVars == null)
        return;
      if (e is ToggleCamera)
      {
        this.mSkillVars.mCameraID = (e as ToggleCamera).CameraID;
        if (this.mSkillVars.mCameraID >= 0 && this.mSkillVars.mCameraID <= 1)
          return;
        Debug.LogError((object) ("Invalid CameraID: " + (object) this.mSkillVars.mCameraID));
        this.mSkillVars.mCameraID = 0;
      }
      else if (e is HitFrame && this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
      {
        HitFrame hitFrame = e as HitFrame;
        --this.mSkillVars.NumHitsLeft;
        for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
        {
          if (this.mSkillVars.Targets[index].ShouldDodgeHits)
          {
            this.mSkillVars.Targets[index].PlayDodge(this.mSkillVars.Targets[index].ShouldPerfectDodge);
            if (this.mSkillVars.mSkillEffect.AlwaysExplode && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Throw)
              this.SpawnHitEffect(((Component) this.mSkillVars.Targets[index]).get_transform().get_position(), this.mSkillVars.NumHitsLeft == 0);
          }
          else
            this.HitTarget(this.mSkillVars.Targets[index], hitFrame.ReactionType);
        }
      }
      else if (e is ChantFrame)
      {
        ChantFrame chantFrame = (ChantFrame) e;
        if (!Object.op_Inequality((Object) this.mSkillVars.mSkillEffect.ChantEffect, (Object) null))
          return;
        Vector3 spawnPos;
        Quaternion spawnRot;
        chantFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ChantEffect, out spawnPos, out spawnRot);
        this.mSkillVars.mChantEffect = (GameObject) Object.Instantiate((Object) this.mSkillVars.mSkillEffect.ChantEffect, spawnPos, spawnRot);
        GameUtility.SetLayer(this.mSkillVars.mChantEffect, ((Component) this).get_gameObject().get_layer(), true);
        if (!chantFrame.AttachEffect)
          return;
        this.mSkillVars.mChantEffect.get_transform().set_parent(chantFrame.BoneName.Length <= 0 ? this.UnitObject.get_transform() : GameUtility.findChildRecursively(this.UnitObject.get_transform(), chantFrame.BoneName));
      }
      else if (e is ProjectileFrame && Object.op_Inequality((Object) this.mSkillVars.mSkillEffect, (Object) null) && this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Ranged)
      {
        this.mSkillVars.mProjectileTriggered = true;
        if (Object.op_Inequality((Object) this.mSkillVars.mProjectile, (Object) null))
          return;
        ProjectileFrame projectileFrame = (ProjectileFrame) e;
        if (Object.op_Inequality((Object) this.mSkillVars.mSkillEffect.ProjectileEffect, (Object) null))
        {
          Vector3 spawnPos;
          Quaternion spawnRot;
          projectileFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.ProjectileEffect, out spawnPos, out spawnRot);
          this.mSkillVars.mProjectile = Object.Instantiate((Object) this.mSkillVars.mSkillEffect.ProjectileEffect, spawnPos, spawnRot) as GameObject;
        }
        else
        {
          Vector3 spawnPos;
          Quaternion spawnRot;
          projectileFrame.CalcPosition(this.UnitObject, Vector3.get_zero(), Quaternion.get_identity(), out spawnPos, out spawnRot);
          this.mSkillVars.mProjectile = new GameObject("PROJECTILE");
          Transform transform = this.mSkillVars.mProjectile.get_transform();
          transform.set_position(spawnPos);
          transform.set_rotation(spawnRot);
        }
        if (this.mSkillVars.mSkillEffect.ProjectileSound != null)
          this.mSkillVars.mSkillEffect.ProjectileSound.Play();
        if (this.mSkillVars.UseBattleScene)
        {
          if (this.mSkillVars.mProjectileThread != null)
            return;
          this.mSkillVars.mProjectileThread = this.StartCoroutine(this.AnimateProjectile(this.mSkillVars.mSkillEffect.ProjectileStart, this.mSkillVars.mSkillEffect.ProjectileStartTime, this.mSkillVars.mProjectile.get_transform().get_position(), Quaternion.get_identity(), (TacticsUnitController.ProjectileStopEvent) null));
        }
        else
        {
          Transform transform = ((Component) this).get_transform();
          MapProjectile mapProjectile = GameUtility.RequireComponent<MapProjectile>(this.mSkillVars.mProjectile);
          if (this.mSkillVars.mSkillEffect.MapHitEffectType == SkillEffect.MapHitEffectTypes.EachHits)
          {
            float num1 = 0.0f;
            Vector3 vector3 = Vector3.op_Subtraction(this.mSkillVars.mTargetPosition, transform.get_position());
            vector3.y = (__Null) 0.0;
            // ISSUE: explicit reference operation
            ((Vector3) @vector3).Normalize();
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              float num2 = Vector3.Dot(Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).get_transform().get_position(), transform.get_position()), vector3);
              if ((double) num2 >= (double) num1)
                num1 = num2;
            }
            this.mSkillVars.mTargetPosition = Vector3.op_Addition(transform.get_position(), Vector3.op_Multiply(vector3, num1));
            this.mSkillVars.mTargetPosition.y = GameUtility.RaycastGround(this.mSkillVars.mTargetPosition).y;
          }
          mapProjectile.StartCameraTargetPosition = transform.get_position();
          mapProjectile.EndCameraTargetPosition = this.mSkillVars.mTargetPosition;
          mapProjectile.GoalPosition = Vector3.op_Addition(this.mSkillVars.mTargetPosition, Vector3.op_Multiply(Vector3.get_up(), 0.5f));
          mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          mapProjectile.HitDelay = this.mSkillVars.mSkillEffect.MapProjectileHitDelay;
          mapProjectile.OnHit = new MapProjectile.HitEvent(this.OnProjectileHit);
          mapProjectile.OnDistanceUpdate = new MapProjectile.DistanceChangeEvent(this.OnProjectileDistanceChange);
          if (Object.op_Inequality((Object) this.mSkillVars.mActiveCamera, (Object) null) && this.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing && this.mSkillVars.mSkillSequence.MapCameraType == SkillSequence.MapCameraTypes.None)
            mapProjectile.CameraTransform = ((Component) this.mSkillVars.mActiveCamera).get_transform();
          if (this.mSkillVars.mSkillEffect.MapTrajectoryType != SkillEffect.TrajectoryTypes.Arrow)
            return;
          mapProjectile.IsArrow = true;
          float num = this.mMapTrajectoryHeight;
          if ((double) GameUtility.CalcDistance2D(Vector3.op_Subtraction(mapProjectile.GoalPosition, ((Component) mapProjectile).get_transform().get_position())) <= 1.20000004768372)
          {
            mapProjectile.IsArrow = false;
            mapProjectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
          }
          else
          {
            mapProjectile.TimeScale = this.mSkillVars.mSkillEffect.MapTrajectoryTimeScale;
            if (this.mSkillVars.mStartPosition.y >= (double) num)
              num = (float) this.mSkillVars.mStartPosition.y;
            mapProjectile.TopHeight = num + 0.5f;
          }
        }
      }
      else if (e is CameraShake)
      {
        this.mSkillVars.mCameraShakeSeedX = Random.get_value();
        this.mSkillVars.mCameraShakeSeedY = Random.get_value();
      }
      else
      {
        if (e is SRPG.AnimEvents.TargetState && Object.op_Inequality((Object) this.mSkillVars.mTargetController, (Object) null))
        {
          switch ((e as SRPG.AnimEvents.TargetState).State)
          {
            case SRPG.AnimEvents.TargetState.StateTypes.Stand:
              this.mSkillVars.mTargetController.PlayIdle(0.0f);
              break;
            case SRPG.AnimEvents.TargetState.StateTypes.Down:
              this.mSkillVars.mTargetController.PlayDown();
              break;
            case SRPG.AnimEvents.TargetState.StateTypes.Kirimomi:
              this.mSkillVars.mTargetController.PlayKirimomi();
              break;
          }
        }
        if (e is AuraFrame)
        {
          if (!this.mSkillVars.mAuraEnable || !Object.op_Inequality((Object) this.mSkillVars.mSkillEffect, (Object) null) || !Object.op_Inequality((Object) this.mSkillVars.mSkillEffect.AuraEffect, (Object) null))
            return;
          AuraFrame auraFrame = e as AuraFrame;
          Vector3 spawnPos;
          Quaternion spawnRot;
          auraFrame.CalcPosition(this.UnitObject, this.mSkillVars.mSkillEffect.AuraEffect, out spawnPos, out spawnRot);
          GameObject gameObject = Object.Instantiate((Object) this.mSkillVars.mSkillEffect.AuraEffect, spawnPos, spawnRot) as GameObject;
          Transform transform1 = gameObject.get_transform();
          Transform transform2 = (Transform) null;
          if (!string.IsNullOrEmpty(auraFrame.BoneName))
            transform2 = GameUtility.findChildRecursively(this.UnitObject.get_transform(), auraFrame.BoneName);
          if (Object.op_Equality((Object) transform2, (Object) null))
            transform2 = this.GetCharacterRoot();
          transform1.set_parent(transform2);
          this.mSkillVars.mAuras.Add(gameObject);
        }
        else
        {
          if (!(e is UnitVoiceEvent))
            return;
          bool flag = this.mSkillVars.MaxPlayVoice == this.mSkillVars.NumPlayVoice;
          --this.mSkillVars.NumPlayVoice;
          if (flag)
          {
            if (this.mSkillVars.Skill.IsReactionSkill() && this.mSkillVars.Skill.IsDamagedSkill())
            {
              this.Unit.PlayBattleVoice("battle_0013");
              return;
            }
            if (this.ShowCriticalEffectOnHit)
            {
              this.Unit.PlayBattleVoice("battle_0012");
              return;
            }
          }
          float num = 0.0f;
          if (this.mSkillVars.Targets != null)
          {
            for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            {
              TacticsUnitController target = this.mSkillVars.Targets[index];
              if (!target.ShouldDodgeHits)
                num = Mathf.Max((int) this.Unit.MaximumStatus.param.hp == 0 ? 1f : (float) (1.0 * (this.mSkillVars.TotalHits <= 1 ? (double) target.mHitInfo.GetTotalHpDamage() : (double) (target.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits))) / (float) (int) this.Unit.MaximumStatus.param.hp, num);
            }
          }
          if ((double) num >= 0.75)
            this.Unit.PlayBattleVoice("battle_0033");
          else if ((double) num > 0.5)
            this.Unit.PlayBattleVoice("battle_0032");
          else
            this.Unit.PlayBattleVoice("battle_0031");
        }
      }
    }

    protected override void OnEvent(AnimEvent e, float time, float weight)
    {
      if (this.mSkillVars == null || !(e is CameraShake))
        return;
      this.mSkillVars.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mSkillVars.mCameraShakeSeedX, this.mSkillVars.mCameraShakeSeedY);
    }

    [DebuggerHidden]
    private IEnumerator AnimateProjectile(AnimationClip clip, float length, Vector3 basePosition, Quaternion baseRotation, TacticsUnitController.ProjectileStopEvent callback)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAnimateProjectile\u003Ec__Iterator14() { length = length, clip = clip, basePosition = basePosition, baseRotation = baseRotation, callback = callback, \u003C\u0024\u003Elength = length, \u003C\u0024\u003Eclip = clip, \u003C\u0024\u003EbasePosition = basePosition, \u003C\u0024\u003EbaseRotation = baseRotation, \u003C\u0024\u003Ecallback = callback, \u003C\u003Ef__this = this };
    }

    private void CalcEnemyPos(AnimationClip clip, float normalizedTime, out Vector3 pos, out Quaternion rotation)
    {
      GameObject gameObject1 = new GameObject();
      GameObject gameObject2 = new GameObject("Enm_Distance_dummy");
      Transform transform1 = gameObject1.get_transform();
      Transform transform2 = gameObject2.get_transform();
      transform2.SetParent(transform1, false);
      transform1.SetParent(((Component) this).get_transform(), false);
      clip.SampleAnimation(gameObject1, normalizedTime * clip.get_length());
      pos = Vector3.op_Addition(transform2.get_position(), this.RootMotionInverse);
      Quaternion quaternion = Quaternion.op_Multiply(transform2.get_localRotation(), Quaternion.AngleAxis(90f, Vector3.get_right()));
      rotation = Quaternion.op_Multiply(transform1.get_rotation(), quaternion);
      Object.DestroyImmediate((Object) gameObject1);
    }

    private void StopAura()
    {
      if (this.mSkillVars.mAuras.Count > 0)
      {
        for (int index = this.mSkillVars.mAuras.Count - 1; index >= 0; --index)
        {
          GameObject mAura = this.mSkillVars.mAuras[index];
          GameUtility.RequireComponent<OneShotParticle>(mAura);
          GameUtility.StopEmitters(mAura);
        }
        this.mSkillVars.mAuras.Clear();
      }
      this.mSkillVars.mAuraEnable = false;
    }

    public void PlayPreSkillAnimation(Camera cam, Vector3 targetPos)
    {
      if (this.mSkillVars == null)
        return;
      this.mSkillVars.mTargetPosition = targetPos;
      this.mSkillVars.mActiveCamera = cam;
      this.GotoState<TacticsUnitController.State_PreSkill>();
    }

    public void PlayDamage(HitReactionTypes hitType)
    {
      if (this.mSkillVars != null || this.Unit.IsUnitCondition(EUnitCondition.Stone))
        return;
      switch (hitType)
      {
        case HitReactionTypes.Normal:
          this.GotoState<TacticsUnitController.State_NormalDamage>();
          break;
        case HitReactionTypes.Aerial:
          this.GotoState<TacticsUnitController.State_AerialDamage>();
          break;
        case HitReactionTypes.Kirimomi:
          this.PlayKirimomi();
          break;
      }
    }

    public void PlayDodge(bool perfectAvoid)
    {
      if (Object.op_Equality((Object) this.FindAnimation("B_DGE"), (Object) null))
        return;
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        if (perfectAvoid)
          SceneBattle.Instance.PopupPefectAvoid(this.CenterPosition, 0.0f);
        else
          SceneBattle.Instance.PopupMiss(this.CenterPosition, 0.0f);
      }
      if (this.mSkillVars != null)
        return;
      this.GotoState<TacticsUnitController.State_Dodge>();
    }

    public void LoadDyingAnimation()
    {
      this.LoadUnitAnimationAsync("B_DIE", "cmn_downstand0", false, false);
    }

    public void LoadDodgeAnimation()
    {
      this.LoadUnitAnimationAsync("B_DGE", "cmn_dodge0", false, false);
    }

    public void LoadDefendAnimations()
    {
      this.LoadUnitAnimationAsync("B_DEF", "cmn_guard0", false, false);
    }

    public void LoadDamageAnimations()
    {
      this.LoadUnitAnimationAsync("B_DMG0", "cmn_damage0", false, false);
      this.LoadUnitAnimationAsync("B_DMG1", "cmn_damageair0", false, false);
      this.LoadUnitAnimationAsync("B_DOWN", "cmn_down0", false, false);
    }

    public void LoadSkillSequence(SkillParam skillParam, bool loadJobAnimation, bool useBattleScene, bool is_cs = false, bool is_cs_sub = false)
    {
      this.mSkillVars = new TacticsUnitController.SkillVars();
      this.mSkillVars.UseBattleScene = useBattleScene;
      if (!useBattleScene)
        ;
      this.mSkillVars.Skill = skillParam;
      this.mSkillVars.mIsCollaboSkill = is_cs;
      this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, loadJobAnimation));
    }

    public bool HasPreSkillAnimation
    {
      get
      {
        if (this.mSkillVars != null)
          return Object.op_Inequality((Object) this.mSkillVars.mSkillStartAnimation, (Object) null);
        return false;
      }
    }

    public bool IsSkillParentPosZero
    {
      get
      {
        if (this.mSkillVars == null || Object.op_Equality((Object) this.mSkillVars.mSkillAnimation, (Object) null))
          return false;
        return this.mSkillVars.mSkillAnimation.IsParentPosZero;
      }
    }

    public bool IsPreSkillParentPosZero
    {
      get
      {
        if (this.mSkillVars == null || Object.op_Equality((Object) this.mSkillVars.mSkillStartAnimation, (Object) null))
          return false;
        return this.mSkillVars.mSkillStartAnimation.IsParentPosZero;
      }
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillSequenceAsync(SkillParam skillParam, bool loadJobAnimation)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillSequenceAsync\u003Ec__Iterator15() { skillParam = skillParam, loadJobAnimation = loadJobAnimation, \u003C\u0024\u003EskillParam = skillParam, \u003C\u0024\u003EloadJobAnimation = loadJobAnimation, \u003C\u003Ef__this = this };
    }

    public void LoadSkillEffect(string skillEffectName, bool is_cs_sub = false)
    {
      if (is_cs_sub)
        skillEffectName += "_sub";
      DebugUtility.Log("LoadSkillEffect: " + skillEffectName);
      if (string.IsNullOrEmpty(skillEffectName))
        return;
      this.AddLoadThreadCount();
      this.StartCoroutine(this.LoadSkillEffectAsync(skillEffectName));
    }

    [DebuggerHidden]
    private IEnumerator LoadSkillEffectAsync(string skillEffectName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CLoadSkillEffectAsync\u003Ec__Iterator16() { skillEffectName = skillEffectName, \u003C\u0024\u003EskillEffectName = skillEffectName, \u003C\u003Ef__this = this };
    }

    public SkillEffect LoadedSkillEffect
    {
      get
      {
        if (this.mSkillVars != null)
          return this.mSkillVars.mSkillEffect;
        return (SkillEffect) null;
      }
    }

    public void UnloadBattleAnimations()
    {
      this.UnloadAnimation("B_DMG0");
      this.UnloadAnimation("B_DOWN");
      this.UnloadAnimation("B_DGE");
      this.UnloadAnimation("B_DEF");
      this.UnloadAnimation("B_SKL");
      this.UnloadAnimation("B_CHA");
      this.UnloadAnimation("B_PRS");
      this.UnloadAnimation("B_BS");
      this.UnloadAnimation("B_RUN");
      this.UnloadAnimation("B_RUNL");
      this.LoadGenkidamaAnimation(false);
      this.UnloadAnimation("B_TOSS_LIFT");
      this.UnloadAnimation("B_TOSS_THROW");
    }

    private void ResetSkill()
    {
      this.mCollideGround = true;
      if (Object.op_Inequality((Object) this.mSkillVars.mTargetController, (Object) null))
        this.mSkillVars.mTargetController.mCollideGround = true;
      this.StopAura();
      this.mLastHitEffect = (GameObject) null;
      if (this.mSkillVars.HitTimerThread != null)
        this.StopCoroutine(this.mSkillVars.HitTimerThread);
      GameUtility.DestroyGameObject(this.mSkillVars.mProjectile);
      this.UnloadBattleAnimations();
      if (this.UseSubEquipment)
        this.ResetSubEquipments();
      this.mSkillVars = (TacticsUnitController.SkillVars) null;
      this.mHitInfoSelf = (LogSkill.Target) null;
    }

    private void FinishSkill()
    {
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        LogSkill.Target mHitInfo = this.mSkillVars.Targets[index].mHitInfo;
        if ((mHitInfo.IsFailCondition() || mHitInfo.IsCureCondition() || mHitInfo.IsBuffEffect()) && !this.mSkillVars.Targets[index].Unit.IsDead)
          this.mSkillVars.Targets[index].UpdateBadStatus();
      }
      if (!this.mSkillVars.UseBattleScene)
        this.StepTo(this.mSkillVars.mStartPosition);
      else
        this.PlayIdle(0.0f);
      this.ResetSkill();
    }

    private int CountSkillUnitVoice()
    {
      int num = 0;
      if (Object.op_Inequality((Object) this.mSkillVars.mSkillAnimation, (Object) null) && this.mSkillVars.mSkillAnimation.events != null)
      {
        for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
        {
          if (this.mSkillVars.mSkillAnimation.events[index] is UnitVoiceEvent)
            ++num;
        }
      }
      return num;
    }

    public void SetLastHitEffect(GameObject effect)
    {
      this.mLastHitEffect = effect;
    }

    public void SetHitInfo(LogSkill.Target target)
    {
      this.mHitInfo = target;
    }

    public void SetHitInfoSelf(LogSkill.Target selfTarget)
    {
      this.mHitInfoSelf = selfTarget;
    }

    public int CountSkillHits()
    {
      int num = 0;
      if (this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
      {
        if (Object.op_Inequality((Object) this.mSkillVars.mSkillAnimation, (Object) null) && this.mSkillVars.mSkillAnimation.events != null)
        {
          for (int index = 0; index < this.mSkillVars.mSkillAnimation.events.Length; ++index)
          {
            if (this.mSkillVars.mSkillAnimation.events[index] is HitFrame)
              ++num;
          }
        }
      }
      else if (this.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Ranged)
        num = 1;
      return num;
    }

    private void SpawnHitEffect(Vector3 pos, bool isLast)
    {
      this.mSkillVars.mSkillEffect.SpawnExplosionEffect(0, pos, Quaternion.get_identity());
      if (!isLast || !Object.op_Inequality((Object) this.mLastHitEffect, (Object) null))
        return;
      GameUtility.SpawnParticle(this.mLastHitEffect, pos, Quaternion.get_identity(), (GameObject) null);
      TimeManager.StartHitSlow(0.1f, 0.3f);
    }

    private void SpawnDefendHitEffect(SkillData defSkill, int useCount, int useCountMax)
    {
      if (defSkill == null)
        return;
      string defendEffect = defSkill.SkillParam.defend_effect;
      if (string.IsNullOrEmpty(defendEffect) || Object.op_Equality((Object) this.mDefendSkillEffects[defendEffect], (Object) null))
        return;
      GameObject defendSkillEffect = this.mDefendSkillEffects[defendEffect];
      if (!Object.op_Inequality((Object) defendSkillEffect, (Object) null))
        return;
      Animator component = (Animator) GameUtility.SpawnParticle(defendSkillEffect, ((Component) this).get_transform().get_position(), Quaternion.get_identity(), (GameObject) null).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetInteger("skill_count", useCount);
      if (useCountMax <= 0)
        return;
      component.SetFloat("skill_count_norm", (float) useCount / (float) useCountMax);
    }

    private void HitDelayed(TacticsUnitController target)
    {
      this.mSkillVars.TotalHits = 1;
      this.mSkillVars.NumHitsLeft = 0;
      if ((double) this.mSkillVars.mSkillEffect.HitColorBlendTime > 0.0)
        target.FadeBlendColor(this.mSkillVars.mSkillEffect.HitColor, this.mSkillVars.mSkillEffect.HitColorBlendTime);
      bool flag = true;
      if (this.mSkillVars.mSkillEffect.RangedHitReactionType != HitReactionTypes.None && target.ShouldDodgeHits)
      {
        target.PlayDodge(target.ShouldPerfectDodge);
        flag = this.mSkillVars.mSkillEffect.AlwaysExplode;
      }
      if (!flag)
        return;
      this.HitTarget(target, this.mSkillVars.mSkillEffect.RangedHitReactionType);
    }

    [DebuggerHidden]
    private IEnumerator AsyncHitTimer()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TacticsUnitController.\u003CAsyncHitTimer\u003Ec__Iterator17() { \u003C\u003Ef__this = this };
    }

    private void InternalStartSkill(TacticsUnitController[] targets, Vector3 targetPosition, Camera activeCamera)
    {
      if (this.mSkillVars.mSkillSequence == null)
      {
        Debug.LogError((object) "SkillSequence not loaded yet");
      }
      else
      {
        this.mCollideGround = !this.mSkillVars.UseBattleScene;
        this.mSkillVars.mActiveCamera = activeCamera;
        this.mSkillVars.Targets = new List<TacticsUnitController>((IEnumerable<TacticsUnitController>) targets);
        if (this.mSkillVars.UseBattleScene)
          this.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
        this.mSkillVars.HitTimerThread = this.StartCoroutine(this.AsyncHitTimer());
        this.mSkillVars.mStartPosition = ((Component) this).get_transform().get_position();
        if (targets.Length == 1)
        {
          this.mSkillVars.mTargetController = targets[0];
          this.mSkillVars.mTargetControllerPosition = ((Component) this.mSkillVars.mTargetController).get_transform().get_position();
          this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        }
        this.mSkillVars.mTargetPosition = !this.mSkillVars.UseBattleScene ? targetPosition : this.mSkillVars.mTargetControllerPosition;
        this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
        this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
        this.mSkillVars.mAuraEnable = Object.op_Inequality((Object) this.mSkillVars.mSkillEffect.AuraEffect, (Object) null);
        this.mSkillVars.mCameraID = 0;
        this.mSkillVars.mChantCameraID = 0;
        this.mSkillVars.mSkillCameraID = 0;
        if (this.mSkillVars.Skill.cast_type == ECastTypes.Jump)
        {
          this.GotoState<TacticsUnitController.State_JumpCastComplete>();
        }
        else
        {
          if (Object.op_Inequality((Object) this.mSkillVars.mSkillEffect, (Object) null) && this.mSkillVars.mSkillEffect.StartSound != null)
            this.mSkillVars.mSkillEffect.StartSound.Play();
          if (Object.op_Inequality((Object) this.mSkillVars.mSkillChantAnimation, (Object) null))
            this.GotoState<TacticsUnitController.State_SkillChant>();
          else if (this.mSkillVars.Skill.effect_type == SkillEffectTypes.Changing)
            this.GotoState<TacticsUnitController.State_ChangeGrid>();
          else if (this.mSkillVars.Skill.effect_type == SkillEffectTypes.Throw)
            this.GotoState<TacticsUnitController.State_Throw>();
          else
            this.GotoState<TacticsUnitController.State_Skill>();
        }
      }
    }

    public void StartSkill(Vector3 targetPosition, Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill)
    {
      this.mSkillVars.HitGrids = hitGrids;
      this.InternalStartSkill(targets, targetPosition, activeCamera);
    }

    public void StartSkill(TacticsUnitController target, Camera activeCamera, SkillParam skill)
    {
      this.InternalStartSkill(new TacticsUnitController[1]
      {
        target
      }, ((Component) target).get_transform().get_position(), activeCamera);
    }

    public void PlayKirimomi()
    {
      this.GotoState<TacticsUnitController.State_Kirimomi>();
    }

    public void PlayDown()
    {
      this.GotoState<TacticsUnitController.State_Down>();
    }

    public void SetHpCostSkill(int SkillHpCost)
    {
      this.mSkillVars.HpCostDamage = SkillHpCost;
    }

    private void OnProjectileDistanceChange(GameObject go, float distance)
    {
      MapProjectile component = (MapProjectile) go.GetComponent<MapProjectile>();
      if (this.mSkillVars.mSkillEffect.MapHitEffectType != SkillEffect.MapHitEffectTypes.EachHits)
        return;
      for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
      {
        if (!this.mSkillVars.HitTargets.Contains(this.mSkillVars.Targets[index]) && (double) component.CalcDepth(((Component) this.mSkillVars.Targets[index]).get_transform().get_position()) <= (double) distance + 0.5)
        {
          this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.get_time() + this.mSkillVars.mSkillEffect.MapProjectileHitDelay));
          this.mSkillVars.HitTargets.Add(this.mSkillVars.Targets[index]);
        }
      }
    }

    private void OnProjectileHit(GameObject go)
    {
      TacticsUnitController tacticsUnitController = this;
      --tacticsUnitController.mSkillVars.NumHitsLeft;
      if (Object.op_Inequality((Object) tacticsUnitController.mSkillVars.mProjectile, (Object) null))
      {
        GameUtility.StopEmitters(tacticsUnitController.mSkillVars.mProjectile);
        GameUtility.RequireComponent<OneShotParticle>(tacticsUnitController.mSkillVars.mProjectile);
        tacticsUnitController.mSkillVars.mProjectile = (GameObject) null;
      }
      if (Object.op_Inequality((Object) tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect, (Object) null))
        GameUtility.SpawnParticle(tacticsUnitController.mSkillVars.mSkillEffect.TargetHitEffect, tacticsUnitController.mSkillVars.mTargetPosition, Quaternion.get_identity(), (GameObject) null);
      switch (this.mSkillVars.mSkillEffect.MapHitEffectType)
      {
        case SkillEffect.MapHitEffectTypes.TargetRadial:
          for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
          {
            Vector3 vector3 = Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).get_transform().get_position(), this.mSkillVars.mTargetPosition);
            vector3.y = (__Null) 0.0;
            float num = Mathf.Abs((float) vector3.x) + Mathf.Abs((float) vector3.z);
            this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.get_time() + num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
          }
          break;
        case SkillEffect.MapHitEffectTypes.EachTargets:
          for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
            this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.get_time() + (float) index * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
          break;
        case SkillEffect.MapHitEffectTypes.Directional:
          Vector3 position1 = ((Component) tacticsUnitController).get_transform().get_position();
          position1.y = (__Null) 0.0;
          Vector3 forward = ((Component) tacticsUnitController).get_transform().get_forward();
          forward.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          ((Vector3) @forward).Normalize();
          for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
          {
            Vector3 position2 = ((Component) this.mSkillVars.Targets[index]).get_transform().get_position();
            position2.y = (__Null) 0.0;
            float num = Mathf.Max(Vector3.Dot(Vector3.op_Subtraction(position2, position1), forward), 0.0f);
            this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
          }
          break;
        case SkillEffect.MapHitEffectTypes.InstigatorRadial:
          for (int index = 0; index < this.mSkillVars.Targets.Count; ++index)
          {
            Vector3 vector3 = Vector3.op_Subtraction(((Component) this.mSkillVars.Targets[index]).get_transform().get_position(), ((Component) this).get_transform().get_position());
            vector3.y = (__Null) 0.0;
            float num = Mathf.Abs((float) vector3.x) + Mathf.Abs((float) vector3.z);
            this.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.mSkillVars.Targets[index], Time.get_time() + num * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
          }
          break;
      }
      this.mSkillVars.mProjectileHitsTarget = true;
    }

    public bool IsJumpCant()
    {
      if (this.Unit.CastSkill != null)
        return ECastTypes.Jump == this.Unit.CastSkill.CastType;
      return false;
    }

    public bool IsCastJumpStartComplete()
    {
      return this.mCastJumpStartComplete;
    }

    public bool IsCastJumpFallComplete()
    {
      return this.mCastJumpFallComplete;
    }

    public void CastJump()
    {
      this.GotoState<TacticsUnitController.State_JumpCast>();
    }

    public void CastJumpFall()
    {
    }

    public void SetCastJump()
    {
      this.CollideGround = true;
      this.SetVisible(false);
      this.mCastJumpStartComplete = true;
      this.mCastJumpFallComplete = false;
      Vector3 position = ((Component) this).get_transform().get_position();
      position.y = (__Null) (position.y + (double) this.mCastJumpOffsetY);
      ((Component) this).get_transform().set_position(position);
    }

    private void hideGimmickForTargetGrid(TacticsUnitController target)
    {
      if (!Object.op_Implicit((Object) target))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      Grid unitGridPosition = instance.Battle.GetUnitGridPosition(target.Unit);
      Unit gimmickAtGrid = instance.Battle.FindGimmickAtGrid(unitGridPosition, true);
      TacticsUnitController unitController = instance.FindUnitController(gimmickAtGrid);
      if (!Object.op_Inequality((Object) unitController, (Object) null))
        return;
      unitController.ScaleHide();
    }

    private void createEffect(TacticsUnitController target, GameObject go_effect)
    {
      GameObject go = Object.Instantiate((Object) go_effect, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
      if (!Object.op_Implicit((Object) go))
        return;
      go.get_transform().SetParent(((Component) target).get_transform(), false);
      GameUtility.RequireComponent<OneShotParticle>(go);
    }

    public Grid KnockBackGrid
    {
      set
      {
        this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
        this.mKnockBackGrid = value;
      }
    }

    public bool IsBusy
    {
      get
      {
        if (this.isIdle)
          return this.mKnockBackMode != TacticsUnitController.eKnockBackMode.IDLE;
        return true;
      }
    }

    private void execKnockBack()
    {
      if (this.mKnockBackMode == TacticsUnitController.eKnockBackMode.IDLE)
        return;
      if (this.mKnockBackGrid == null)
      {
        this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
      }
      else
      {
        SceneBattle instance = SceneBattle.Instance;
        if (!Object.op_Implicit((Object) instance))
        {
          this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
        }
        else
        {
          switch (this.mKnockBackMode)
          {
            case TacticsUnitController.eKnockBackMode.START:
              this.mKbPosStart = this.CenterPosition;
              this.mKbPosEnd = instance.CalcGridCenter(instance.Battle.GetUnitGridPosition(this.Unit));
              this.mKbPassedSec = 0.0f;
              this.mKnockBackMode = TacticsUnitController.eKnockBackMode.EXEC;
              this.createEffect(this, instance.KnockBackEffect);
              break;
            case TacticsUnitController.eKnockBackMode.EXEC:
              this.mKbPassedSec += Time.get_deltaTime();
              float num = this.mKbPassedSec / 0.4f;
              if ((double) num >= 1.0)
              {
                ((Component) this).get_transform().set_position(this.mKbPosEnd);
                this.hideGimmickForTargetGrid(this);
                this.mKnockBackMode = TacticsUnitController.eKnockBackMode.IDLE;
                break;
              }
              ((Component) this).get_transform().set_position(Vector3.Lerp(this.mKbPosStart, this.mKbPosEnd, num * (2f - num)));
              break;
          }
        }
      }
    }

    private class State_Skill_Without_Animation : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState;
      private Quaternion mTargetRotation;
      private float mWaitTime;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        if (self.mSkillVars.mSkillEffect.StopAura == SkillEffect.AuraStopTimings.BeforeHit)
          self.StopAura();
        if (Object.op_Inequality((Object) self.mSkillVars.mTargetController, (Object) null))
          this.mTargetRotation = ((Component) self.mSkillVars.mTargetController).get_transform().get_rotation();
        this.mWaitTime = self.mSkillVars.mSkillEffect.ProjectileStartTime;
        --self.mSkillVars.NumHitsLeft;
        for (int index = 0; index < self.mSkillVars.Targets.Count; ++index)
        {
          if (self.mSkillVars.Targets[index].ShouldDodgeHits)
          {
            self.mSkillVars.Targets[index].PlayDodge(self.mSkillVars.Targets[index].ShouldPerfectDodge);
            if (self.mSkillVars.mSkillEffect.AlwaysExplode && self.mSkillVars.Skill.effect_type != SkillEffectTypes.Changing)
              self.SpawnHitEffect(((Component) self.mSkillVars.Targets[index]).get_transform().get_position(), self.mSkillVars.NumHitsLeft == 0);
          }
          else
            self.HitTarget(self.mSkillVars.Targets[index], HitReactionTypes.None);
        }
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterHit)
          return;
        self.StopAura();
      }

      public override void Update(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        else
          self.GotoState<TacticsUnitController.State_RangedSkillEnd>();
      }
    }

    public enum PostureTypes
    {
      Combat,
      NonCombat,
    }

    public enum HPGaugeModes
    {
      Normal,
      Attack,
      Target,
    }

    private class HideGimmickAnimation
    {
      private float mWait = 1f;
      private float mSpeed = 5f;
      private Vector3 mBaseScale;
      private TacticsUnitController mGimmickController;
      public bool IsHide;
      public bool Enable;
      private float mCurrentWait;

      public void Init(TacticsUnitController GimmickController)
      {
        if (Object.op_Equality((Object) GimmickController, (Object) null) || GimmickController.Unit == null || !GimmickController.Unit.IsGimmick)
          return;
        this.mGimmickController = GimmickController;
        this.mBaseScale = ((Component) this.mGimmickController).get_transform().get_localScale();
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
      }

      public void ResetScale()
      {
        this.mCurrentWait = this.mWait;
        this.Enable = false;
        this.IsHide = true;
        ((Component) this.mGimmickController).get_transform().set_localScale(this.mBaseScale);
      }

      public void Update()
      {
        if (!this.Enable)
          return;
        if (this.IsHide)
        {
          if (0.0 < (double) this.mCurrentWait)
          {
            this.mCurrentWait -= this.mSpeed * Time.get_deltaTime();
            ((Component) this.mGimmickController).get_transform().set_localScale(Vector3.op_Multiply(this.mBaseScale, this.mCurrentWait / this.mWait));
            return;
          }
          ((Component) this.mGimmickController).get_transform().set_localScale(Vector3.get_zero());
          this.mCurrentWait = 0.0f;
        }
        else
        {
          if ((double) this.mWait > (double) this.mCurrentWait)
          {
            this.mCurrentWait += this.mSpeed * Time.get_deltaTime();
            ((Component) this.mGimmickController).get_transform().set_localScale(Vector3.op_Multiply(this.mBaseScale, this.mCurrentWait / this.mWait));
            return;
          }
          ((Component) this.mGimmickController).get_transform().set_localScale(this.mBaseScale);
          this.mCurrentWait = this.mWait;
        }
        this.Enable = false;
      }
    }

    private class State : SRPG.State<TacticsUnitController>
    {
    }

    private enum ColorBlendModes
    {
      None,
      Fade,
      Blink,
    }

    private class State_WaitResources : TacticsUnitController.State
    {
      public override void Update(TacticsUnitController self)
      {
        if (self.IsLoading)
          return;
        self.mLoadedPartially = true;
        if (self.mCharacterData.Movable)
        {
          self.LoadUnitAnimationAsync("RUN", TacticsUnitController.ANIM_RUN_FIELD, true, false);
          self.LoadUnitAnimationAsync("STEP", TacticsUnitController.ANIM_STEP, false, false);
          self.LoadUnitAnimationAsync("FLLP", TacticsUnitController.ANIM_FALL_LOOP, false, false);
          self.LoadUnitAnimationAsync("FLEN", TacticsUnitController.ANIM_FALL_END, false, false);
          self.LoadUnitAnimationAsync("CLMB", TacticsUnitController.ANIM_CLIMBUP, false, false);
        }
        self.PlayIdle(0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mUnit != null && self.mUnit.IsDead || self.KeepUnitHidden)
          return;
        self.SetVisible(true);
      }
    }

    private class State_Idle : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        string id = "IDLE";
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
        self.UpdateBadStatus();
        if (self.mBadStatus != null && !string.IsNullOrEmpty(self.mBadStatus.AnimationName) && (Object.op_Inequality((Object) self.FindAnimation("BAD"), (Object) null) && self.mBadStatus.AnimationName == self.mLoadedBadStatusAnimation))
          id = "BAD";
        if ((double) self.GetTargetWeight(id) >= 1.0)
          return;
        self.PlayAnimation(id, true, self.mIdleInterpTime, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        string animationName = self.mBadStatus == null ? (string) null : self.mBadStatus.AnimationName;
        if (!string.IsNullOrEmpty(animationName))
        {
          if (!string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) && self.mLoadedBadStatusAnimation != animationName)
          {
            if (Object.op_Implicit((Object) self.FindAnimation("BAD")))
            {
              self.UnloadAnimation("BAD");
              self.StopAnimation("BAD");
              self.mLoadedBadStatusAnimation = (string) null;
            }
          }
          else if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation))
          {
            self.LoadUnitAnimationAsync("BAD", animationName, false, false);
            self.mLoadedBadStatusAnimation = animationName;
          }
          else if (Object.op_Inequality((Object) self.FindAnimation("BAD"), (Object) null) && (double) self.GetTargetWeight("BAD") < 1.0)
            self.PlayAnimation("BAD", true, 0.25f, 0.0f);
        }
        else if (!string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) && Object.op_Implicit((Object) self.FindAnimation("BAD")))
        {
          self.UnloadAnimation("BAD");
          self.mLoadedBadStatusAnimation = (string) null;
          self.PlayAnimation("IDLE", true, 0.25f, 0.0f);
        }
        if (!self.AutoUpdateRotation)
          return;
        self.UpdateRotation();
      }
    }

    private class State_LookAt : TacticsUnitController.State
    {
      private float mSpinTime = 0.25f;
      private float mSpinCount = 2f;
      private float mJumpHeight = 0.2f;
      private float mTime;
      private Transform mTransform;
      private Vector3 mStartPosition;
      private Quaternion mEndRotation;

      public override void Begin(TacticsUnitController self)
      {
        this.mSpinCount = self.mSpinCount;
        this.mSpinTime = this.mSpinCount * 0.125f;
        this.mJumpHeight = this.mSpinCount * 0.1f;
        this.mTransform = ((Component) self).get_transform();
        Vector3 vector3 = Vector3.op_Subtraction(self.mLookAtTarget, this.mTransform.get_position());
        vector3.y = (__Null) 0.0;
        this.mStartPosition = this.mTransform.get_position();
        this.mEndRotation = Quaternion.LookRotation(vector3);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.get_deltaTime();
        float num = Mathf.Clamp01(this.mTime / this.mSpinTime);
        this.mTransform.set_rotation(Quaternion.op_Multiply(Quaternion.AngleAxis((float) ((1.0 - (double) num) * (double) this.mSpinCount * 360.0), Vector3.get_up()), this.mEndRotation));
        Vector3 mStartPosition = this.mStartPosition;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @mStartPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + (double) Mathf.Sin(num * 3.141593f) * (double) this.mJumpHeight);
        this.mTransform.set_position(mStartPosition);
        if ((double) num < 1.0)
          return;
        self.PlayIdle(0.0f);
      }
    }

    private class State_FieldAction : TacticsUnitController.State
    {
    }

    private class State_FieldActionClimpUp : TacticsUnitController.State_FieldAction
    {
    }

    private class State_FieldActionJumpUp : TacticsUnitController.State_FieldAction
    {
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;
      private float mDuration;
      private bool mFalling;
      private float mLastY;

      public override void Begin(TacticsUnitController self)
      {
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        this.mStartPosition = ((Component) self).get_transform().get_position();
        self.PlayAnimation("CLMB", true, 0.1f, 0.0f);
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Discard;
        self.mCollideGround = false;
        GameSettings instance = GameSettings.Instance;
        this.mDuration = instance.Unit_JumpMinTime + instance.Unit_JumpTimePerHeight * (float) (this.mEndPosition.y - this.mStartPosition.y);
      }

      public override void End(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
        self.mCollideGround = true;
      }

      public override void Update(TacticsUnitController self)
      {
        GameSettings instance = GameSettings.Instance;
        this.mTime += Time.get_deltaTime();
        float num1 = Mathf.Clamp01(this.mTime / this.mDuration);
        Vector3 zero = Vector3.get_zero();
        float num2 = instance.Unit_JumpZCurve.Evaluate(num1);
        float num3 = instance.Unit_JumpYCurve.Evaluate(num1);
        zero.x = (__Null) (double) Mathf.Lerp((float) this.mStartPosition.x, (float) this.mEndPosition.x, num2);
        zero.z = (__Null) (double) Mathf.Lerp((float) this.mStartPosition.z, (float) this.mEndPosition.z, num2);
        zero.y = (__Null) ((double) Mathf.Lerp((float) this.mStartPosition.y, (float) this.mEndPosition.y, num1) + (double) num3);
        ((Component) self).get_transform().set_position(zero);
        if (!this.mFalling && (double) num3 < (double) this.mLastY)
        {
          self.PlayAnimation("FLLP", true, 0.1f, 0.0f);
          this.mFalling = true;
        }
        this.mLastY = num3;
        if ((double) num1 < 1.0)
          return;
        self.mOnFieldActionEnd();
      }
    }

    private class State_FieldActionJump : TacticsUnitController.State_FieldAction
    {
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;
      private float mAnimRate;
      private float mJumpHeight;

      public override void Begin(TacticsUnitController self)
      {
        this.mStartPosition = ((Component) self).get_transform().get_position();
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        float num = (float) (this.mEndPosition.y - this.mStartPosition.y);
        this.mJumpHeight = Mathf.Abs(num);
        if ((double) num < 0.0)
          this.mJumpHeight *= 0.5f;
        this.mAnimRate = (float) (1.0 / ((double) GameUtility.CalcDistance2D(this.mStartPosition, this.mEndPosition) / (double) GameSettings.Instance.Quest.MapRunSpeedMax));
        ((Component) self).get_transform().set_rotation(self.mFieldActionDir.ToRotation());
        self.mCollideGround = false;
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime = Mathf.Clamp01(this.mTime + this.mAnimRate * Time.get_deltaTime());
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, this.mTime);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @vector3;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + (double) Mathf.Sin(this.mTime * 3.141593f) * (double) this.mJumpHeight);
        if ((double) this.mTime >= 1.0)
        {
          ((Component) self).get_transform().set_position(this.mEndPosition);
          self.mOnFieldActionEnd();
        }
        else
          ((Component) self).get_transform().set_position(vector3);
      }

      public override void End(TacticsUnitController self)
      {
        self.mCollideGround = true;
      }
    }

    private class State_FieldActionFall : TacticsUnitController.State_FieldAction
    {
      private float mDuration = 0.5f;
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;
      private float mTime;

      public override void Begin(TacticsUnitController self)
      {
        this.mStartPosition = ((Component) self).get_transform().get_position();
        this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
        GameSettings instance = GameSettings.Instance;
        if (Object.op_Inequality((Object) instance, (Object) null))
          this.mDuration = instance.Unit_FallMinTime + Mathf.Abs((float) (this.mStartPosition.y - this.mEndPosition.y)) * instance.Unit_FallTimePerHeight;
        ((Component) self).get_transform().set_rotation(self.mFieldActionDir.ToRotation());
        self.mCollideGround = false;
        self.PlayAnimation("FLLP", true, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mTime += Time.get_deltaTime();
        float num = Mathf.Clamp01(this.mTime / this.mDuration);
        Vector3 vector3 = Vector3.Lerp(this.mStartPosition, this.mEndPosition, num);
        vector3.y = (__Null) (double) Mathf.Lerp((float) this.mStartPosition.y, (float) this.mEndPosition.y, 1f - Mathf.Cos((float) ((double) num * 3.14159274101257 * 0.5)));
        if ((double) num >= 1.0)
        {
          ((Component) self).get_transform().set_position(this.mEndPosition);
          self.mOnFieldActionEnd();
        }
        else
          ((Component) self).get_transform().set_position(vector3);
      }

      public override void End(TacticsUnitController self)
      {
        self.mCollideGround = true;
      }
    }

    private class State_RunLoop : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation(self.mRunAnimation, true);
      }

      public override void Update(TacticsUnitController self)
      {
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private class State_StepNoAnimation : TacticsUnitController.State
    {
      public override void Update(TacticsUnitController self)
      {
        if (self.mCancelAction)
        {
          self.mCancelAction = false;
          self.PlayIdle(0.0f);
        }
        else
        {
          self.mStepStart = Vector3.Lerp(self.mStepStart, self.mStepEnd, Time.get_deltaTime() * 10f);
          Vector3 vector3 = Vector3.op_Subtraction(self.mStepStart, self.mStepEnd);
          // ISSUE: explicit reference operation
          if ((double) ((Vector3) @vector3).get_magnitude() < 0.00999999977648258)
          {
            ((Component) self).get_transform().set_position(self.mStepEnd);
            self.PlayIdle(0.1f);
          }
          else
            ((Component) self).get_transform().set_position(self.mStepStart);
        }
      }
    }

    private class State_Step : TacticsUnitController.State
    {
      private float mLandTime;

      public override void Begin(TacticsUnitController self)
      {
        AnimDef animation = self.GetAnimation("STEP");
        this.mLandTime = animation.Length;
        for (int index = 0; index < animation.events.Length; ++index)
        {
          if (animation.events[index] is SRPG.AnimEvents.Marker)
          {
            this.mLandTime = animation.events[index].End;
            break;
          }
        }
        self.PlayAnimation("STEP", false);
        self.SetSpeed("STEP", GameSettings.Instance.Quest.GridSnapSpeed);
      }

      public override void Update(TacticsUnitController self)
      {
        if (self.mCancelAction)
        {
          self.mCancelAction = false;
          self.PlayIdle(0.0f);
        }
        else
        {
          float remainingTime = self.GetRemainingTime("STEP");
          float num = Mathf.Clamp01((self.GetAnimation("STEP").Length - remainingTime) / this.mLandTime);
          Vector3 vector3 = Vector3.Lerp(self.mStepStart, self.mStepEnd, num);
          ((Component) self).get_transform().set_position(vector3);
          if ((double) remainingTime > 0.0)
            return;
          self.PlayIdle(0.2f);
        }
      }
    }

    private class State_Move : TacticsUnitController.State
    {
      private Vector3 mStartPos;
      private bool mAdjustDirection;
      private Quaternion mDesiredRotation;
      private float mRotationRate;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mRoutePos < self.mRoute.Length)
        {
          self.PlayAnimation(self.mRunAnimation, true, self.mMoveAnimInterpTime, 0.0f);
          this.LookToward(self, Vector3.op_Subtraction(self.mRoute[self.mRoutePos], ((Component) self).get_transform().get_position()));
          this.mStartPos = self.mRoute[self.mRoutePos];
        }
        else
          this.mStartPos = ((Component) self).get_transform().get_position();
        if ((double) self.mPostMoveAngle < 0.0)
          return;
        this.mAdjustDirection = true;
        this.mDesiredRotation = Quaternion.AngleAxis(self.mPostMoveAngle, Vector3.get_up());
        this.mRotationRate = 720f;
      }

      public override void Update(TacticsUnitController self)
      {
        Transform transform = ((Component) self).get_transform();
        if (self.mRoute.Length <= self.mRoutePos)
        {
          if (this.mAdjustDirection)
          {
            Quaternion rotation = transform.get_rotation();
            if ((double) Quaternion.Angle(rotation, this.mDesiredRotation) > 1.0)
            {
              transform.set_rotation(Quaternion.RotateTowards(rotation, this.mDesiredRotation, this.mRotationRate * Time.get_deltaTime()));
              return;
            }
          }
          transform.set_rotation(this.mDesiredRotation);
          self.PlayIdle(0.0f);
        }
        else
        {
          Vector3 vector3_1 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos], this.mStartPos);
          vector3_1.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          float sqrMagnitude = ((Vector3) @vector3_1).get_sqrMagnitude();
          if ((double) sqrMagnitude <= 9.99999974737875E-05)
          {
            transform.set_position(self.mRoute[self.mRoutePos]);
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle(0.0f);
          }
          else
          {
            if ((double) sqrMagnitude > 9.99999974737875E-05)
            {
              Vector3 vector3_2 = vector3_1;
              vector3_2.y = (__Null) 0.0;
              // ISSUE: explicit reference operation
              ((Vector3) @vector3_2).Normalize();
              if (self.TriggerFieldAction(vector3_2, true))
              {
                self.mMoveAnimInterpTime = 0.1f;
                return;
              }
              Vector3 a = Vector3.op_Addition(((Component) self).get_transform().get_position(), Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, self.mRunSpeed), Time.get_deltaTime()));
              ((Component) self).get_transform().set_position(a);
              float num1 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos]);
              if ((double) num1 < 0.5 && self.mRoutePos < self.mRoute.Length - 1)
              {
                Vector3 vector3_3 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos + 1], self.mRoute[self.mRoutePos]);
                vector3_3.y = (__Null) 0.0;
                // ISSUE: explicit reference operation
                ((Vector3) @vector3_3).Normalize();
                float num2 = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
                ((Component) self).get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector3_2), Quaternion.LookRotation(vector3_3), num2));
              }
              else if (self.mRoutePos > 1)
              {
                float num2 = GameUtility.CalcDistance2D(a, self.mRoute[self.mRoutePos - 1]);
                if ((double) num2 < 0.5)
                {
                  Vector3 vector3_3 = Vector3.op_Subtraction(self.mRoute[self.mRoutePos - 1], self.mRoute[self.mRoutePos - 2]);
                  vector3_3.y = (__Null) 0.0;
                  // ISSUE: explicit reference operation
                  ((Vector3) @vector3_3).Normalize();
                  float num3 = (float) (0.5 + (double) num2 / 0.5 * 0.5);
                  ((Component) self).get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector3_3), Quaternion.LookRotation(vector3_2), num3));
                }
                else
                  this.LookToward(self, vector3_2);
              }
              else
                this.LookToward(self, vector3_2);
            }
            Vector3 vector3_4 = Vector3.op_Subtraction(transform.get_position(), this.mStartPos);
            vector3_4.y = (__Null) 0.0;
            if ((double) Vector3.Dot(vector3_1, vector3_4) / (double) sqrMagnitude < 0.999899983406067)
              return;
            this.mStartPos = self.mRoute[self.mRoutePos];
            ((Component) self).get_transform().set_position(self.mRoute[self.mRoutePos]);
            ++self.mRoutePos;
            if (this.mAdjustDirection || self.mRoute.Length > self.mRoutePos)
              return;
            self.PlayIdle(0.0f);
          }
        }
      }

      private void LookToward(TacticsUnitController self, Vector3 v)
      {
        v.y = (__Null) 0.0;
        // ISSUE: explicit reference operation
        if ((double) ((Vector3) @v).get_magnitude() <= 9.99999974737875E-05)
          return;
        ((Component) self).get_transform().set_rotation(Quaternion.LookRotation(v));
      }
    }

    private class State_Pickup : TacticsUnitController.State
    {
      private Vector3 mObjectStartPos;
      private Vector3 mTopPos;
      private bool mPickedUp;
      private float mPostPickupDelay;

      public override void Begin(TacticsUnitController self)
      {
        self.AutoUpdateRotation = false;
        this.mPostPickupDelay = GameSettings.Instance.Quest.WaitAfterUnitPickupGimmick;
        self.PlayAnimation("PICK", false);
        self.SetEquipmentsVisible(false);
        this.mObjectStartPos = self.mPickupObject.get_transform().get_position();
        this.mTopPos = this.mObjectStartPos;
        if (Object.op_Inequality((Object) self.mCharacterSettings, (Object) null))
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local = @this.mTopPos;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local).y = (__Null) ((^local).y + (double) self.mCharacterSettings.Height * ((Component) self).get_transform().get_lossyScale().y);
        }
        MapPickup componentInChildren = (MapPickup) self.mPickupObject.GetComponentInChildren<MapPickup>();
        if (!Object.op_Inequality((Object) componentInChildren, (Object) null) || !Object.op_Inequality((Object) componentInChildren.Shadow, (Object) null))
          return;
        ((Component) componentInChildren.Shadow).get_gameObject().SetActive(false);
      }

      public override void Update(TacticsUnitController self)
      {
        Vector3 vector3 = Vector3.Lerp(this.mObjectStartPos, this.mTopPos, Mathf.Sin((float) ((double) self.GetNormalizedTime("PICK") * 3.14159274101257 * 0.5)));
        Transform transform = self.mPickupObject.get_transform();
        transform.set_rotation(Quaternion.LookRotation(Vector3.get_back()));
        transform.set_position(vector3);
        if ((double) self.GetRemainingTime("PICK") <= 0.0)
        {
          if (!this.mPickedUp)
          {
            MapPickup componentInChildren = (MapPickup) self.mPickupObject.GetComponentInChildren<MapPickup>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null) && componentInChildren.OnPickup != null)
              componentInChildren.OnPickup();
            this.mPickedUp = true;
          }
          this.mPostPickupDelay -= Time.get_deltaTime();
        }
        if ((double) this.mPostPickupDelay > 0.0)
          return;
        self.GotoState<TacticsUnitController.State_PostPickup>();
      }
    }

    private class State_PostPickup : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        ObjectAnimator.Get(self.mPickupObject).ScaleTo(Vector3.get_zero(), 0.2f, ObjectAnimator.CurveType.Linear);
        GameUtility.StopEmitters(self.mPickupObject);
        self.SetEquipmentsVisible(true);
      }

      public override void Update(TacticsUnitController self)
      {
        if (ObjectAnimator.Get(self.mPickupObject).isMoving)
          return;
        self.PlayIdle(0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        self.mPickupObject = (GameObject) null;
        self.UnloadPickupAnimation();
      }
    }

    private class State_Taken : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("PICK", false);
      }
    }

    private struct CameraState
    {
      public Vector3 Position;
      public Quaternion Rotation;
      public static TacticsUnitController.CameraState Default;

      public void CacheCurrent(Camera camera)
      {
        Transform transform = ((Component) camera).get_transform();
        this.Position = transform.get_position();
        this.Rotation = transform.get_rotation();
      }
    }

    public enum PointType
    {
      Damage,
      Heal,
    }

    public struct OutputPoint
    {
      public int Value;
      public TacticsUnitController.PointType PointType;
      public bool Critical;
    }

    private class SkillVars
    {
      public List<GameObject> mAuras = new List<GameObject>(4);
      public Quaternion mCameraShakeOffset = Quaternion.get_identity();
      public List<TacticsUnitController.OutputPoint> mOutputPoints = new List<TacticsUnitController.OutputPoint>(32);
      public List<TacticsUnitController.HitTimer> HitTimers = new List<TacticsUnitController.HitTimer>();
      public List<TacticsUnitController> HitTargets = new List<TacticsUnitController>(4);
      private int mDefaultLayer;
      public bool SuppressDamageOutput;
      public int HpCostDamage;
      public SkillParam Skill;
      public bool mIsCollaboSkill;
      public bool mIsCollaboSkillSub;
      public List<TacticsUnitController> Targets;
      public Vector3 mStartPosition;
      public Vector3 mTargetPosition;
      public Vector3 mTargetControllerPosition;
      public TacticsUnitController mTargetController;
      public bool mAuraEnable;
      public Camera mActiveCamera;
      public StatusParam Param;
      public int mCameraID;
      public int mChantCameraID;
      public int mSkillCameraID;
      public float mCameraShakeSeedX;
      public float mCameraShakeSeedY;
      public int MaxPlayVoice;
      public int NumPlayVoice;
      public int TotalHits;
      public int NumHitsLeft;
      public SkillSequence mSkillSequence;
      public AnimDef mSkillChantAnimation;
      public AnimDef mSkillAnimation;
      public AnimDef mSkillEndAnimation;
      public AnimDef mSkillStartAnimation;
      public AnimationClip mSkillChantCameraClip;
      public AnimationClip mSkillCameraClip;
      public AnimationClip mSkillEndCameraClip;
      public GameObject mChantEffect;
      public GameObject mProjectile;
      public bool mProjectileTriggered;
      public SkillEffect mSkillEffect;
      public Coroutine mProjectileThread;
      public bool UseBattleScene;
      public bool mProjectileHitsTarget;
      public Vector3[] HitGrids;
      public Coroutine HitTimerThread;
    }

    public enum EElementEffectTypes
    {
      NotEffective,
      Normal,
      Effective,
    }

    public class ShieldState
    {
      public Unit.UnitShield Target;
      public int LastHP;
      public int LastTurn;
      public bool Dirty;

      public void ClearDirty()
      {
        this.LastHP = (int) this.Target.hp;
        this.LastTurn = (int) this.Target.turn;
        this.Dirty = false;
      }
    }

    private class ShakeUnit
    {
      private float mShakeSpeed = 0.0125f;
      private int mShakeMaxCount = 8;
      private int mShakeCount = 8;
      private Vector3 mShakeBasePos;
      private Vector3 mShakeOffset;
      private bool mIsShakeStart;

      public bool ShakeStart
      {
        set
        {
          this.mIsShakeStart = value;
        }
        get
        {
          return this.mIsShakeStart;
        }
      }

      public int ShakeMaxCount
      {
        set
        {
        }
        get
        {
          return this.mShakeMaxCount;
        }
      }

      public int ShakeCount
      {
        set
        {
        }
        get
        {
          return this.mShakeCount;
        }
      }

      public void Init(Vector3 basePosition, Vector3 direction)
      {
        GameSettings instance = GameSettings.Instance;
        this.mShakeSpeed = instance.ShakeUnit_Offset;
        this.mShakeMaxCount = instance.ShakeUnit_MaxCount;
        this.mShakeBasePos = basePosition;
        Vector3 vector3 = Vector3.Cross(direction, new Vector3(0.0f, 1f, 0.0f));
        // ISSUE: explicit reference operation
        this.mShakeOffset = ((Vector3) @vector3).get_normalized();
        TacticsUnitController.ShakeUnit shakeUnit = this;
        shakeUnit.mShakeOffset = Vector3.op_Multiply(shakeUnit.mShakeOffset, this.mShakeSpeed);
        this.mShakeCount = this.mShakeMaxCount;
        this.mIsShakeStart = false;
      }

      public Vector3 AdvanceShake()
      {
        if (!this.mIsShakeStart)
          return this.mShakeBasePos;
        if (0 < this.mShakeCount)
        {
          if (this.mShakeCount % 2 == 0)
            this.mShakeOffset = Vector3.op_UnaryNegation(this.mShakeOffset);
          --this.mShakeCount;
          return Vector3.op_Addition(this.mShakeBasePos, this.mShakeOffset);
        }
        this.mIsShakeStart = false;
        return this.mShakeBasePos;
      }
    }

    private struct HitTimer
    {
      public TacticsUnitController Target;
      public float HitTime;

      public HitTimer(TacticsUnitController target, float hitTime)
      {
        this.Target = target;
        this.HitTime = hitTime;
      }
    }

    [Flags]
    public enum DeathAnimationTypes
    {
      Normal = 1,
    }

    private class State_Dead : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Discard;
        self.PlayAnimation("B_DEAD", false);
        self.Unit.PlayBattleVoice("battle_0028");
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6050", 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DEAD") > 0.0)
          return;
        Object.Destroy((Object) ((Component) self).get_gameObject());
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6051", 0.0f);
      }
    }

    private class State_Kirimomi : TacticsUnitController.State
    {
      private Quaternion mRotationOld;

      public override void Begin(TacticsUnitController self)
      {
        this.mRotationOld = self.UnitObject.get_transform().get_localRotation();
      }

      public override void Update(TacticsUnitController self)
      {
        self.UnitObject.get_transform().set_localRotation(Quaternion.AngleAxis(Time.get_time() * GameSettings.Instance.Quest.KirimomiRotationRate, Vector3.get_up()));
      }

      public override void End(TacticsUnitController self)
      {
        self.UnitObject.get_transform().set_localRotation(this.mRotationOld);
      }
    }

    private class State_Down : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DOWN", true);
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private class State_Dodge : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.Unit.PlayBattleVoice("battle_0017");
        self.PlayAnimation("B_DGE", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DGE") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_Defend : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DEF", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DEF") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_NormalDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DMG0", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_DMG0") > 0.100000001490116)
          return;
        self.PlayIdle(0.1f);
      }
    }

    private class State_AerialDamage : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_DMG1", false, 0.1f, 0.0f);
      }
    }

    private class State_SkillChant : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState = new TacticsUnitController.CameraState();

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        self.PlayAnimation("B_CHA", false);
        if (!Object.op_Inequality((Object) self.mSkillVars.mSkillEffect, (Object) null) || self.mSkillVars.mSkillEffect.ChantSound == null)
          return;
        self.mSkillVars.mSkillEffect.ChantSound.Play();
      }

      public override void Update(TacticsUnitController self)
      {
        self.mSkillVars.mChantCameraID = self.mSkillVars.mCameraID;
        float normalizedTime = self.GetNormalizedTime("B_CHA");
        if (self.mSkillVars.UseBattleScene && !self.mSkillVars.mIsCollaboSkillSub && Object.op_Inequality((Object) self.mSkillVars.mSkillChantCameraClip, (Object) null))
        {
          if (self.mSkillVars.mSkillSequence.InterpChantCamera)
            self.AnimateCameraInterpolated(self.mSkillVars.mSkillChantCameraClip, normalizedTime, 4f * normalizedTime, this.mStartCameraState, self.mSkillVars.mChantCameraID);
          else
            self.AnimateCamera(self.mSkillVars.mSkillChantCameraClip, normalizedTime, self.mSkillVars.mChantCameraID);
        }
        if ((double) normalizedTime < 1.0)
          return;
        if (Object.op_Inequality((Object) self.mSkillVars.mChantEffect, (Object) null))
        {
          if (Object.op_Inequality((Object) self.mSkillVars.mChantEffect.GetComponentInChildren<ParticleSystem>(), (Object) null))
          {
            GameUtility.StopEmitters(self.mSkillVars.mChantEffect);
            self.mSkillVars.mChantEffect.AddComponent<OneShotParticle>();
          }
          else
            Object.DestroyImmediate((Object) self.mSkillVars.mChantEffect);
          self.mSkillVars.mChantEffect = (GameObject) null;
        }
        if (self.mSkillVars.Skill.effect_type == SkillEffectTypes.Changing)
          self.GotoState<TacticsUnitController.State_ChangeGrid>();
        else if (self.mSkillVars.Skill.effect_type == SkillEffectTypes.Throw)
          self.GotoState<TacticsUnitController.State_Throw>();
        else
          self.GotoState<TacticsUnitController.State_Skill>();
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterChant)
          return;
        self.StopAura();
      }
    }

    private class State_Skill : TacticsUnitController.State
    {
      private TacticsUnitController.CameraState mStartCameraState;
      private Quaternion mTargetRotation;
      private float mWaitTime;

      public override void Begin(TacticsUnitController self)
      {
        if (self.mSkillVars.UseBattleScene)
          this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
        self.PlayAnimation("B_SKL", false);
        if (self.mSkillVars.mSkillEffect.StopAura == SkillEffect.AuraStopTimings.BeforeHit)
          self.StopAura();
        if (Object.op_Inequality((Object) self.mSkillVars.mTargetController, (Object) null))
          this.mTargetRotation = ((Component) self.mSkillVars.mTargetController).get_transform().get_rotation();
        this.mWaitTime = self.mSkillVars.mSkillEffect.ProjectileStartTime;
      }

      public override void End(TacticsUnitController self)
      {
        if (self.mSkillVars.mSkillEffect.StopAura != SkillEffect.AuraStopTimings.AfterHit)
          return;
        self.StopAura();
      }

      public override void Update(TacticsUnitController self)
      {
        self.mSkillVars.mSkillCameraID = self.mSkillVars.mCameraID;
        this.mWaitTime = Mathf.Max(this.mWaitTime - Time.get_deltaTime(), 0.0f);
        float normalizedTime = self.GetNormalizedTime("B_SKL");
        if (self.mSkillVars.UseBattleScene)
        {
          if (self.mSkillVars.mSkillAnimation.CurveNames.Contains("Enm_Distance_dummy") && Object.op_Inequality((Object) self.mSkillVars.mTargetController, (Object) null))
          {
            Vector3 pos;
            Quaternion rotation;
            self.CalcEnemyPos(self.mSkillVars.mSkillAnimation.animation, normalizedTime, out pos, out rotation);
            Transform transform = ((Component) self.mSkillVars.mTargetController).get_transform();
            transform.set_position(pos);
            transform.set_rotation(Quaternion.op_Multiply(rotation, this.mTargetRotation));
          }
          if (Object.op_Inequality((Object) self.mSkillVars.mSkillCameraClip, (Object) null) && !self.mSkillVars.mIsCollaboSkillSub)
          {
            if (self.mSkillVars.mSkillSequence.InterpSkillCamera)
              self.AnimateCameraInterpolated(self.mSkillVars.mSkillCameraClip, normalizedTime, 4f * normalizedTime, this.mStartCameraState, self.mSkillVars.mSkillCameraID);
            else
              self.AnimateCamera(self.mSkillVars.mSkillCameraClip, normalizedTime, self.mSkillVars.mSkillCameraID);
          }
        }
        if ((double) self.GetRemainingTime("B_SKL") > 0.0 || (double) this.mWaitTime > 0.0)
          return;
        if (self.mSkillVars.mSkillSequence.SkillType == SkillSequence.SkillTypes.Melee)
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        else
          self.GotoState<TacticsUnitController.State_RangedSkillEnd>();
      }
    }

    private class State_SkillEnd : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        if (!Object.op_Inequality((Object) self.FindAnimation("B_BS"), (Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f, 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) self.GetRemainingTime("B_BS") > 0.0)
          return;
        self.SkillEffectSelf();
        self.FinishSkill();
      }
    }

    private class State_RangedSkillEnd : TacticsUnitController.State
    {
      private float mAnimationLength;
      private float mStateTime;
      private bool mUnitAnimationEnded;
      private bool mHit;
      private bool mProjStartAnimEnded;
      private bool mProjEndAnimEnded;

      public override void Begin(TacticsUnitController self)
      {
        this.mAnimationLength = self.mSkillVars.mSkillSequence.EndLength;
        if (!self.mSkillVars.mProjectileTriggered)
          self.OnProjectileHit((GameObject) null);
        if (!Object.op_Inequality((Object) self.FindAnimation("B_BS"), (Object) null))
          return;
        self.PlayAnimation("B_BS", false, 0.1f, 0.0f);
      }

      public override void End(TacticsUnitController self)
      {
        self.mSkillVars.mTargetController = (TacticsUnitController) null;
      }

      private void OnHit()
      {
        this.self.OnProjectileHit(this.self.mSkillVars.mProjectile);
      }

      public override void Update(TacticsUnitController self)
      {
        this.mStateTime += Time.get_deltaTime();
        if (self.mSkillVars.UseBattleScene)
        {
          if (!this.mProjStartAnimEnded && Object.op_Inequality((Object) self.mSkillVars.mProjectile, (Object) null))
          {
            if (self.mSkillVars.mProjectileThread != null)
              return;
            this.mProjStartAnimEnded = true;
            if (Object.op_Inequality((Object) self.mSkillVars.mTargetController, (Object) null))
            {
              self.mSkillVars.mProjectileThread = self.StartCoroutine(self.AnimateProjectile(self.mSkillVars.mSkillEffect.ProjectileEnd, self.mSkillVars.mSkillEffect.ProjectileEndTime, self.mSkillVars.mTargetControllerPosition, Quaternion.get_identity(), new TacticsUnitController.ProjectileStopEvent(this.OnHit)));
              ParticleSystem component = (ParticleSystem) self.mSkillVars.mProjectile.GetComponent<ParticleSystem>();
              if (Object.op_Inequality((Object) component, (Object) null))
                component.Clear(true);
            }
          }
          if (!self.mSkillVars.mIsCollaboSkillSub)
          {
            if (Object.op_Inequality((Object) self.mSkillVars.mSkillEndCameraClip, (Object) null))
            {
              Vector3 pos;
              Quaternion rotation;
              self.CalcCameraPos(self.mSkillVars.mSkillEndCameraClip, Mathf.Clamp01(this.mStateTime / this.mAnimationLength), 0, out pos, out rotation);
              self.SetActiveCameraPosition(Vector3.op_Addition(self.mSkillVars.mTargetControllerPosition, pos), Quaternion.op_Multiply(self.mSkillVars.mCameraShakeOffset, rotation));
            }
            else
            {
              Transform battleCamera = GameSettings.Instance.Quest.BattleCamera;
              Vector3 position = battleCamera.get_position();
              Quaternion rotation = battleCamera.get_rotation();
              self.SetActiveCameraPosition(Vector3.op_Addition(self.mSkillVars.mTargetControllerPosition, position), Quaternion.op_Multiply(self.mSkillVars.mCameraShakeOffset, rotation));
            }
          }
        }
        if (!this.mUnitAnimationEnded && (double) self.GetRemainingTime("B_BS") <= 0.0)
        {
          if (self.mSkillVars.UseBattleScene)
            self.PlayAnimation("IDLE", true, 0.1f, 0.0f);
          this.mUnitAnimationEnded = true;
        }
        if (self.mSkillVars.mProjectileThread != null)
          return;
        if ((double) this.mStateTime >= (double) this.mAnimationLength && this.mHit)
        {
          self.SkillEffectSelf();
          self.FinishSkill();
        }
        else
        {
          if (this.mHit || !self.mSkillVars.mProjectileHitsTarget || self.mSkillVars.HitTimers.Count > 0)
            return;
          this.mHit = true;
        }
      }
    }

    private class State_PreSkill : TacticsUnitController.State
    {
      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_PRS", false);
        self.mSkillVars.mAuraEnable = false;
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
      }

      public override void End(TacticsUnitController self)
      {
        self.RootMotionMode = AnimationPlayer.RootMotionModes.Translate;
      }

      public override void Update(TacticsUnitController self)
      {
        float normalizedTime = self.GetNormalizedTime("B_PRS");
        self.AnimateCamera(self.mSkillVars.mSkillStartAnimation.animation, normalizedTime, self.mSkillVars.mCameraID);
        if ((double) normalizedTime < 1.0)
          return;
        self.PlayIdle(0.0f);
      }
    }

    private class State_JumpCast : TacticsUnitController.State
    {
      private float mCastTime = 0.5f;
      private float mBasePosY;
      private float mElapsed;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePosY = (float) ((Component) self).get_transform().get_position().y;
        this.mElapsed = 0.0f;
        self.CollideGround = false;
        self.mCastJumpStartComplete = false;
        self.mCastJumpFallComplete = false;
        self.PlayAnimation("CLMB", false, this.mCastTime / 10f, 0.0f);
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_7036", 0.0f);
      }

      public override void Update(TacticsUnitController self)
      {
        if ((double) this.mCastTime <= (double) this.mElapsed)
        {
          self.PlayIdle(0.0f);
        }
        else
        {
          this.mElapsed += Time.get_deltaTime();
          if ((double) this.mCastTime < (double) this.mElapsed)
            this.mElapsed = this.mCastTime;
          Vector3 position = ((Component) self).get_transform().get_position();
          position.y = (__Null) ((double) this.mBasePosY + (double) self.mCastJumpOffsetY * ((double) this.mElapsed / (double) this.mCastTime));
          ((Component) self).get_transform().set_position(position);
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.SetVisible(false);
        self.mCastJumpStartComplete = true;
      }
    }

    private class State_JumpCastComplete : TacticsUnitController.State
    {
      private static readonly Color SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
      private float mCastTime = 0.3f;
      private float mFallStartWait = 0.7f;
      private float TransWaitTime = 0.1f;
      private bool beforeVisible = true;
      private float ReturnTime = 0.3f;
      private TacticsUnitController.State_JumpCastComplete.MotionMode Mode;
      private GameObject mFallEffect;
      private GameObject mHitEffect;
      private Vector3 mBasePos;
      private IntVector2 mBaseMapPos;
      private float mElapsed;
      private TacticsUnitController[] mExcludes;
      private float TransStartTime;
      private bool isDirect;

      public override void Begin(TacticsUnitController self)
      {
        this.mBasePos = ((Component) self).get_transform().get_position();
        this.mBaseMapPos = new IntVector2(self.Unit.x, self.Unit.y);
        this.mElapsed = 0.0f;
        self.mCastJumpFallComplete = false;
        self.CollideGround = false;
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL_WAIT;
        self.AnimateVessel(1f, 0.0f);
        if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        {
          SceneBattle instance = SceneBattle.Instance;
          this.mExcludes = instance.GetActiveUnits();
          Array.Resize<TacticsUnitController>(ref this.mExcludes, this.mExcludes.Length + 1);
          this.mExcludes[this.mExcludes.Length - 1] = self;
          FadeController.Instance.BeginSceneFade(TacticsUnitController.State_JumpCastComplete.SceneFadeColor, 0.5f, this.mExcludes, (TacticsUnitController[]) null);
          this.mBasePos = instance.CalcGridCenter(this.mBaseMapPos.x, this.mBaseMapPos.y);
          self.mSkillVars.mStartPosition = this.mBasePos;
        }
        self.PlayAnimation("FLLP", true, 0.0f, 0.0f);
      }

      private void FallWaitUpdate(TacticsUnitController self)
      {
        if ((double) this.mElapsed >= (double) this.mFallStartWait)
        {
          this.EnterFall();
        }
        else
        {
          this.mElapsed += Time.get_deltaTime();
          if ((double) this.mElapsed < (double) this.mFallStartWait)
            return;
          this.mElapsed = this.mFallStartWait;
        }
      }

      private void EnterFall()
      {
        this.self.SetVisible(true);
        this.self.SetEquipmentsVisible(true);
        if (this.self.mSkillVars != null && Object.op_Inequality((Object) this.self.mSkillVars.mSkillEffect.AuraEffect, (Object) null))
        {
          this.mFallEffect = Object.Instantiate((Object) this.self.mSkillVars.mSkillEffect.AuraEffect, ((Component) this.self).get_transform().get_position(), ((Component) this.self).get_transform().get_rotation()) as GameObject;
          this.mFallEffect.get_transform().SetParent(((Component) this.self).get_transform());
        }
        Vector3 position = ((Component) this.self).get_transform().get_position();
        position.y = (__Null) (double) this.self.mCastJumpOffsetY;
        ((Component) this.self).get_transform().set_position(position);
        this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.FALL;
        this.mElapsed = 0.0f;
      }

      private void FallUpdate(TacticsUnitController self)
      {
        if ((double) this.mElapsed >= (double) this.mCastTime)
        {
          if (self.mSkillVars != null && Object.op_Inequality((Object) self.mSkillVars.mSkillEffect.TargetHitEffect, (Object) null))
            this.mHitEffect = Object.Instantiate((Object) self.mSkillVars.mSkillEffect.TargetHitEffect, self.JumpFallPos, Quaternion.get_identity()) as GameObject;
          this.Mode = TacticsUnitController.State_JumpCastComplete.MotionMode.RETURN;
          this.EnterReturn();
        }
        else
        {
          this.mElapsed += Time.get_deltaTime();
          if ((double) this.mElapsed >= (double) this.mCastTime)
            this.mElapsed = this.mCastTime;
          Vector3 jumpFallPos = self.JumpFallPos;
          jumpFallPos.y = (__Null) (self.JumpFallPos.y + (double) self.mCastJumpOffsetY * (1.0 - (double) this.mElapsed / (double) this.mCastTime));
          ((Component) self).get_transform().set_position(jumpFallPos);
        }
      }

      private void EnterReturn()
      {
        if (Object.op_Inequality((Object) this.mFallEffect, (Object) null))
        {
          GameUtility.RequireComponent<OneShotParticle>(this.mFallEffect);
          GameUtility.StopEmitters(this.mFallEffect);
        }
        if (Object.op_Inequality((Object) this.mHitEffect, (Object) null))
          GameUtility.RequireComponent<OneShotParticle>(this.mHitEffect);
        for (int index = 0; index < this.self.mSkillVars.Targets.Count; ++index)
        {
          Vector3 vector3 = Vector3.op_Subtraction(((Component) this.self.mSkillVars.Targets[index]).get_transform().get_position(), this.self.mSkillVars.mTargetPosition);
          vector3.y = (__Null) 0.0;
          float num = Mathf.Abs((float) vector3.x) + Mathf.Abs((float) vector3.z);
          this.self.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(this.self.mSkillVars.Targets[index], Time.get_time() + num * this.self.mSkillVars.mSkillEffect.MapHitEffectIntervals));
        }
        this.self.PlayAnimation("STEP", false);
        float num1 = (float) (this.mBaseMapPos.x - this.self.JumpMapFallPos.x);
        float num2 = (float) (this.mBaseMapPos.y - this.self.JumpMapFallPos.y);
        if ((double) num1 * (double) num1 <= 1.0 && (double) num2 * (double) num2 <= 1.0)
          this.isDirect = true;
        this.mElapsed = 0.0f;
        this.TransStartTime = Time.get_time();
      }

      private void ReturnUpdate(TacticsUnitController self)
      {
        if ((double) this.TransWaitTime > (double) Time.get_time() - (double) this.TransStartTime)
          return;
        this.mElapsed += Time.get_deltaTime();
        if ((double) this.mElapsed >= (double) this.ReturnTime)
        {
          this.mElapsed = this.ReturnTime;
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.mBasePos, self.JumpFallPos);
        ((Component) self).get_transform().set_position(Vector3.op_Addition(self.JumpFallPos, Vector3.op_Multiply(vector3, this.mElapsed / this.ReturnTime)));
        bool visible = this.beforeVisible;
        if (!this.isDirect)
        {
          if ((double) this.ReturnTime * 0.850000023841858 < (double) this.mElapsed)
            visible = true;
          else if ((double) this.ReturnTime * 0.150000005960464 < (double) this.mElapsed)
            visible = false;
        }
        if (this.beforeVisible != visible)
          self.SetVisible(visible);
        this.beforeVisible = visible;
      }

      public override void Update(TacticsUnitController self)
      {
        if (this.Mode == TacticsUnitController.State_JumpCastComplete.MotionMode.FALL_WAIT)
          this.FallWaitUpdate(self);
        else if (this.Mode == TacticsUnitController.State_JumpCastComplete.MotionMode.FALL)
        {
          this.FallUpdate(self);
        }
        else
        {
          if (this.Mode != TacticsUnitController.State_JumpCastComplete.MotionMode.RETURN)
            return;
          this.ReturnUpdate(self);
        }
      }

      public override void End(TacticsUnitController self)
      {
        self.CollideGround = true;
        self.mCastJumpFallComplete = true;
        ((Component) self).get_transform().set_position(this.mBasePos);
      }

      private enum MotionMode
      {
        FALL_WAIT,
        FALL,
        RETURN,
      }
    }

    private class State_ChangeGrid : TacticsUnitController.State
    {
      private bool mChanged;
      private bool mStartEffect;
      private GameObject mSelfEffect;
      private GameObject mTargetEffect;
      private ParticleSystem mSelfParticle;

      private GameObject CreateEffect(GameObject EffectPrefab, TacticsUnitController Parent)
      {
        GameObject go = Object.Instantiate((Object) EffectPrefab, ((Component) Parent).get_transform().get_position(), ((Component) Parent).get_transform().get_rotation()) as GameObject;
        if (Object.op_Inequality((Object) go, (Object) null))
        {
          go.RequireComponent<OneShotParticle>();
          go.get_transform().SetParent(((Component) Parent).get_transform());
        }
        return go;
      }

      private void ChangePosition()
      {
      }

      public override void Begin(TacticsUnitController self)
      {
        self.PlayAnimation("B_SKL", false);
        TacticsUnitController target = self.mSkillVars.Targets[0];
        target.AutoUpdateRotation = false;
        target.LookAt(self.CenterPosition);
      }

      [DebuggerHidden]
      private IEnumerator Wait(float Seconds, TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CWait\u003Ec__Iterator18() { self = self, Seconds = Seconds, \u003C\u0024\u003Eself = self, \u003C\u0024\u003ESeconds = Seconds, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator EffectEndWait(float Seconds, TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_ChangeGrid.\u003CEffectEndWait\u003Ec__Iterator19() { Seconds = Seconds, self = self, \u003C\u0024\u003ESeconds = Seconds, \u003C\u0024\u003Eself = self };
      }

      public override void Update(TacticsUnitController self)
      {
        if (!this.mStartEffect)
        {
          if ((double) self.GetNormalizedTime("B_SKL") < 1.0)
            return;
          self.StartCoroutine(this.Wait(0.2f, self));
        }
        else
        {
          if (!Object.op_Equality((Object) this.mSelfEffect, (Object) null) || !Object.op_Equality((Object) this.mTargetEffect, (Object) null))
            ;
          if (!Object.op_Inequality((Object) this.mSelfParticle, (Object) null) || (double) this.mSelfParticle.get_time() / (double) this.mSelfParticle.get_duration() < 1.0 || this.mChanged)
            return;
          this.mChanged = true;
          TacticsUnitController target = self.mSkillVars.Targets[0];
          Vector3 position = ((Component) self).get_transform().get_position();
          ((Component) self).get_transform().set_position(((Component) target).get_transform().get_position());
          ((Component) target).get_transform().set_position(position);
          self.mSkillVars.mStartPosition = ((Component) self).get_transform().get_position();
          self.StartCoroutine(this.EffectEndWait(0.2f, self));
        }
      }

      public override void End(TacticsUnitController self)
      {
      }
    }

    private enum eKnockBackMode
    {
      IDLE,
      START,
      EXEC,
    }

    private class State_Throw : TacticsUnitController.State
    {
      private const float START_WAIT_TIME = 0.1f;
      private const float TURN_WAIT_TIME = 0.1f;
      private const float ACC_WAIT_TIME = 0.3f;
      private const float FINISH_WAIT_TIME = 0.2f;
      private SceneBattle mSceneBattle;
      private TacticsUnitController mTargetTuc;

      public override void Begin(TacticsUnitController self)
      {
        this.mSceneBattle = SceneBattle.Instance;
        if (self.mSkillVars.Targets.Count > 0)
          this.mTargetTuc = self.mSkillVars.Targets[0];
        if (!Object.op_Implicit((Object) this.mSceneBattle) || !Object.op_Implicit((Object) this.mTargetTuc))
        {
          self.GotoState<TacticsUnitController.State_SkillEnd>();
        }
        else
        {
          self.LoadUnitAnimationAsync("B_TOSS_LIFT", "cmn_toss_lift0", false, false);
          self.LoadUnitAnimationAsync("B_TOSS_THROW", "cmn_toss_throw0", false, false);
          self.StartCoroutine(this.execPerformance(self));
        }
      }

      [DebuggerHidden]
      private IEnumerator execPerformance(TacticsUnitController self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003CexecPerformance\u003Ec__Iterator1A() { self = self, \u003C\u0024\u003Eself = self, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator lerpTurn(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpTurn\u003Ec__Iterator1B() { target = target, target_pos = target_pos, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Etarget_pos = target_pos };
      }

      [DebuggerHidden]
      private IEnumerator lerpPickUp(TacticsUnitController self, TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpPickUp\u003Ec__Iterator1C() { target = target, self = self, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Eself = self };
      }

      [DebuggerHidden]
      private IEnumerator lerpThrow(TacticsUnitController target, Vector3 target_pos)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpThrow\u003Ec__Iterator1D() { target = target, target_pos = target_pos, \u003C\u0024\u003Etarget = target, \u003C\u0024\u003Etarget_pos = target_pos };
      }

      [DebuggerHidden]
      private IEnumerator lerpBound(TacticsUnitController target)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new TacticsUnitController.State_Throw.\u003ClerpBound\u003Ec__Iterator1E() { target = target, \u003C\u0024\u003Etarget = target };
      }
    }

    private delegate void FieldActionEndEvent();

    private delegate void ProjectileStopEvent();
  }
}
