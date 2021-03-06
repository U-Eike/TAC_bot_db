﻿// Decompiled with JetBrains decompiler
// Type: UIUtility
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUtility
{
  private static Canvas mParticleCanvas;
  private static RectTransform mUIPool;
  private static Font fontCache;
  private static Canvas mCanvas2D;

  public static void InitParticleCanvas()
  {
    if (!Object.op_Equality((Object) UIUtility.mParticleCanvas, (Object) null))
      return;
    GameObject gameObject = new GameObject("ParticleCanvas", new System.Type[3]
    {
      typeof (Canvas),
      typeof (GraphicRaycaster),
      typeof (SRPG_CanvasScaler)
    });
    Object.DontDestroyOnLoad((Object) gameObject);
    UIUtility.mParticleCanvas = (Canvas) gameObject.GetComponent<Canvas>();
    UIUtility.mParticleCanvas.set_renderMode((RenderMode) 0);
    UIUtility.mParticleCanvas.set_sortingOrder(30000);
  }

  public static Canvas ParticleCanvas
  {
    get
    {
      UIUtility.InitParticleCanvas();
      return UIUtility.mParticleCanvas;
    }
  }

  public static Canvas PushCanvas(bool systemModal = false, int systemModalPriority = -1)
  {
    Canvas canvas = (Canvas) Object.Instantiate<Canvas>((M0) GameSettings.Instance.Canvas2D);
    Object.DontDestroyOnLoad((Object) ((Component) canvas).get_gameObject());
    CanvasStack canvasStack = (CanvasStack) ((Component) canvas).GetComponent<CanvasStack>();
    if (Object.op_Equality((Object) canvasStack, (Object) null))
      canvasStack = (CanvasStack) ((Component) canvas).get_gameObject().AddComponent<CanvasStack>();
    if (systemModal)
    {
      canvasStack.SystemModal = true;
      canvasStack.Priority = systemModalPriority;
    }
    ((Component) canvas).get_gameObject().SetActive(false);
    ((Component) canvas).get_gameObject().SetActive(true);
    return canvas;
  }

  public static void PopCanvas()
  {
    UIUtility.PopCanvas(false);
  }

  public static void PopCanvas(bool keepInstance)
  {
    if (!Object.op_Inequality((Object) CanvasStack.Top, (Object) null))
      return;
    if (!keepInstance)
      Object.DestroyImmediate((Object) ((Component) CanvasStack.Top).get_gameObject());
    else
      Object.DestroyImmediate((Object) ((Component) CanvasStack.Top).GetComponent<CanvasStack>());
  }

  public static void PopCanvasAll()
  {
    while (Object.op_Inequality((Object) CanvasStack.Top, (Object) null))
      UIUtility.PopCanvas();
  }

  public static void ResetInput()
  {
    EventSystem current = EventSystem.get_current();
    ((Behaviour) current).set_enabled(false);
    ((Behaviour) current).set_enabled(true);
  }

  public static GameObject ConfirmBox(string text, string confirmID, UIUtility.DialogResultEvent okEventListener, UIUtility.DialogResultEvent cancelEventListener, GameObject parent = null, bool systemModal = false, int systemModalPriority = -1)
  {
    if (string.IsNullOrEmpty(confirmID))
      return UIUtility.ConfirmBox(text, okEventListener, cancelEventListener, (GameObject) null, false, -1);
    GameSettings instance = GameSettings.Instance;
    if (PlayerPrefs.GetInt(confirmID, 0) != 0)
    {
      okEventListener((GameObject) null);
      return (GameObject) null;
    }
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (Object.op_Inequality((Object) parent, (Object) null))
      ((Component) canvas).get_transform().SetParent(parent.get_transform());
    Win_Btn_DecideCancel_FL_Check_C decideCancelFlCheckC = UIUtility.Instantiate<Win_Btn_DecideCancel_FL_Check_C>(instance.Dialogs.YesNoDialogWithCheckBox);
    ((Component) decideCancelFlCheckC).get_transform().SetParent(((Component) canvas).get_transform(), false);
    decideCancelFlCheckC.OnClickYes = okEventListener;
    decideCancelFlCheckC.OnClickNo = cancelEventListener;
    decideCancelFlCheckC.Text_Message.set_text(text);
    decideCancelFlCheckC.ConfirmID = confirmID;
    decideCancelFlCheckC.Toggle.set_isOn(false);
    UIUtility.FixFont(((Component) decideCancelFlCheckC).get_gameObject());
    return ((Component) decideCancelFlCheckC).get_gameObject();
  }

  public static GameObject ConfirmBox(string text, UIUtility.DialogResultEvent okEventListener, UIUtility.DialogResultEvent cancelEventListener, GameObject parent = null, bool systemModal = false, int systemModalPriority = -1)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (Object.op_Inequality((Object) parent, (Object) null))
      ((Component) canvas).get_transform().SetParent(parent.get_transform());
    Win_Btn_DecideCancel_FL_C btnDecideCancelFlC = UIUtility.Instantiate<Win_Btn_DecideCancel_FL_C>(instance.Dialogs.YesNoDialog);
    ((Component) btnDecideCancelFlC).get_transform().SetParent(((Component) canvas).get_transform(), false);
    btnDecideCancelFlC.OnClickYes = okEventListener;
    btnDecideCancelFlC.OnClickNo = cancelEventListener;
    btnDecideCancelFlC.Text_Message.set_text(text);
    UIUtility.FixFont(((Component) btnDecideCancelFlC).get_gameObject());
    return ((Component) btnDecideCancelFlC).get_gameObject();
  }

  private static void FixFont(GameObject go)
  {
    if (Object.op_Equality((Object) UIUtility.fontCache, (Object) null))
      UIUtility.fontCache = (Font) Resources.Load<Font>("TT_ModeMinA-B");
    foreach (Text componentsInChild in (Text[]) go.GetComponentsInChildren<Text>())
      componentsInChild.set_font(UIUtility.fontCache);
  }

  public static GameObject ConfirmBoxTitle(string title, string text, UIUtility.DialogResultEvent okEventListener, UIUtility.DialogResultEvent cancelEventListener, GameObject parent = null, bool systemModal = false, int systemModalPriority = -1)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (Object.op_Inequality((Object) parent, (Object) null))
      ((Component) canvas).get_transform().SetParent(parent.get_transform());
    Win_Btn_YN_Title_Flx winBtnYnTitleFlx = UIUtility.Instantiate<Win_Btn_YN_Title_Flx>(instance.Dialogs.YesNoDialogWithTitle);
    ((Component) winBtnYnTitleFlx).get_transform().SetParent(((Component) canvas).get_transform(), false);
    winBtnYnTitleFlx.OnClickYes = okEventListener;
    winBtnYnTitleFlx.OnClickNo = cancelEventListener;
    winBtnYnTitleFlx.Text_Title.set_text(title);
    winBtnYnTitleFlx.Text_Message.set_text(text);
    UIUtility.FixFont(((Component) winBtnYnTitleFlx).get_gameObject());
    return ((Component) winBtnYnTitleFlx).get_gameObject();
  }

  public static GameObject SystemMessage(string title, string msg, UIUtility.DialogResultEvent okEventListener, GameObject parent = null, bool systemModal = false, int systemModalPriority = -1)
  {
    GameSettings instance = GameSettings.Instance;
    Canvas canvas = UIUtility.PushCanvas(systemModal, systemModalPriority);
    if (Object.op_Inequality((Object) parent, (Object) null))
      ((Component) canvas).get_transform().SetParent(parent.get_transform());
    Win_Btn_Decide_Title_Flx btnDecideTitleFlx = UIUtility.Instantiate<Win_Btn_Decide_Title_Flx>(instance.Dialogs.YesDialogWithTitle);
    ((Component) btnDecideTitleFlx).get_transform().SetParent(((Component) canvas).get_transform(), false);
    btnDecideTitleFlx.Text_Title.set_text(title);
    btnDecideTitleFlx.Text_Message.set_text(msg);
    btnDecideTitleFlx.OnClickYes = okEventListener;
    UIUtility.FixFont(((Component) btnDecideTitleFlx).get_gameObject());
    return ((Component) btnDecideTitleFlx).get_gameObject();
  }

  public static GameObject NegativeSystemMessage(string title, string msg, UIUtility.DialogResultEvent okEventListener, GameObject parent = null, bool systemModal = false, int systemModalPriority = -1)
  {
    GameObject gameObject = UIUtility.SystemMessage(title, msg, okEventListener, parent, systemModal, systemModalPriority);
    SystemSound systemSound = !Object.op_Equality((Object) gameObject, (Object) null) ? (SystemSound) gameObject.GetComponentInChildren<SystemSound>() : (SystemSound) null;
    if (Object.op_Inequality((Object) systemSound, (Object) null))
      systemSound.Cue = SystemSound.ECue.Tap;
    return gameObject;
  }

  public static void SetImage(GameObject obj, Texture tex)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    RawImage component = (RawImage) obj.GetComponent<RawImage>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.set_texture(tex);
  }

  public static void SetImage(Component obj, Texture tex)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    UIUtility.SetImage(obj.get_gameObject(), tex);
  }

  public static void SetSprite(GameObject obj, Sprite tex)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    Image component = (Image) obj.GetComponent<Image>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.set_sprite(tex);
  }

  public static void SetSprite(Component obj, Sprite tex)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    UIUtility.SetSprite(obj.get_gameObject(), tex);
  }

  public static T Instantiate<T>(T prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab.get_gameObject()).GetComponent(typeof (T));
  }

  public static GameObject Instantiate(GameObject prefab)
  {
    if (Object.op_Equality((Object) CanvasStack.Top, (Object) null))
      UIUtility.PushCanvas(false, -1);
    RectTransform transform1 = (RectTransform) ((Component) CanvasStack.Top).get_transform();
    GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) prefab);
    gameObject.get_transform().SetParent((Transform) transform1, false);
    RectTransform transform2 = (RectTransform) prefab.get_transform();
    RectTransform transform3 = (RectTransform) gameObject.get_transform();
    ((Transform) transform3).set_localRotation(((Transform) transform2).get_localRotation());
    ((Transform) transform3).set_localScale(((Transform) transform2).get_localScale());
    ((Transform) transform3).set_position(((Transform) transform2).get_position());
    transform3.set_sizeDelta(transform2.get_sizeDelta());
    transform3.set_anchoredPosition(transform2.get_anchoredPosition());
    transform3.set_pivot(transform2.get_pivot());
    return gameObject;
  }

  public static GameObject Instantiate(ResourceRequest req)
  {
    return UIUtility.Instantiate((GameObject) req.get_asset());
  }

  public static T Instantiate<T>(Component prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab.get_gameObject()).GetComponent(typeof (T));
  }

  public static T Instantiate<T>(GameObject prefab) where T : Component
  {
    return (T) UIUtility.Instantiate(prefab).GetComponent(typeof (T));
  }

  public static T Instantiate<T>(ResourceRequest req) where T : Component
  {
    return (T) (!(req.get_asset() is GameObject) ? UIUtility.Instantiate<Component>((Component) req.get_asset()).get_gameObject() : UIUtility.Instantiate((GameObject) req.get_asset())).GetComponent(typeof (T));
  }

  public static void ToggleWindowState(GameObject go, bool open, bool keepActive)
  {
    bool activeInHierarchy = go.get_activeInHierarchy();
    if (open && !activeInHierarchy)
      go.SetActive(true);
    Animator component = (Animator) go.GetComponent<Animator>();
    if (!activeInHierarchy)
    {
      ((Behaviour) component).set_enabled(false);
      ((Behaviour) component).set_enabled(true);
    }
    if (GameUtility.ValidateAnimator(component))
      component.SetBool("close", !open);
    if (keepActive)
      return;
    UIDeactivator uiDeactivator = (UIDeactivator) go.GetComponent<UIDeactivator>();
    if (Object.op_Equality((Object) uiDeactivator, (Object) null))
      uiDeactivator = (UIDeactivator) go.AddComponent<UIDeactivator>();
    if (!open)
      ((Behaviour) uiDeactivator).set_enabled(true);
    else
      ((Behaviour) uiDeactivator).set_enabled(false);
  }

  public static void ToggleWindowState(GameObject go, bool open)
  {
    UIUtility.ToggleWindowState(go, open, false);
  }

  public static void ToggleWindowState(Component go, bool open)
  {
    UIUtility.ToggleWindowState(go.get_gameObject(), open, false);
  }

  public static void ToggleWindowState(Component go, bool open, bool keepActive)
  {
    UIUtility.ToggleWindowState(go.get_gameObject(), open, keepActive);
  }

  public static void SetText(GameObject go, string text)
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return;
    foreach (Text componentsInChild in (Text[]) go.GetComponentsInChildren<Text>(true))
      componentsInChild.set_text(text);
  }

  public static void SetText(Component go, string text)
  {
    UIUtility.SetText(go.get_gameObject(), text);
  }

  public static void SetButtonText(Button go, string text)
  {
    UIUtility.SetText((Component) go, text);
  }

  public static void SpawnParticle(GameObject particleObject, RectTransform targetRect, Vector2 anchor)
  {
    if (Object.op_Equality((Object) particleObject, (Object) null) || Object.op_Equality((Object) targetRect, (Object) null))
      return;
    Vector3[] vector3Array = new Vector3[4];
    targetRect.GetWorldCorners(vector3Array);
    Vector3 vector3_1 = Vector3.op_Subtraction(vector3Array[3], vector3Array[0]);
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3Array[1], vector3Array[0]);
    Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint((Camera) null, Vector3.op_Addition(Vector3.op_Addition(vector3Array[0], Vector3.op_Multiply(vector3_1, (float) anchor.x)), Vector3.op_Multiply(vector3_2, (float) anchor.y)));
    UIUtility.SpawnParticle(particleObject, screenPoint);
  }

  public static void SpawnParticle(GameObject particleObject, Vector2 screenPosition)
  {
    if (Object.op_Equality((Object) particleObject, (Object) null))
      return;
    GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) particleObject);
    gameObject.AddComponent<OneShotParticle>();
    RectTransform transform = gameObject.get_transform() as RectTransform;
    ((Transform) transform).SetParent(((Component) UIUtility.ParticleCanvas).get_transform(), false);
    Vector2 vector2;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(((Component) UIUtility.ParticleCanvas).get_transform() as RectTransform, screenPosition, (Camera) null, ref vector2);
    transform.set_anchoredPosition(vector2);
  }

  public static RectTransform Pool
  {
    get
    {
      if (Object.op_Equality((Object) UIUtility.mUIPool, (Object) null))
      {
        GameObject gameObject = new GameObject("UIPOOL", new System.Type[1]
        {
          typeof (RectTransform)
        });
        Object.DontDestroyOnLoad((Object) gameObject);
        UIUtility.mUIPool = gameObject.get_transform() as RectTransform;
        UIUtility.mUIPool.set_sizeDelta(new Vector2((float) Screen.get_width(), (float) Screen.get_height()));
      }
      return UIUtility.mUIPool;
    }
  }

  public static void AddEventListener(GameObject go, UnityEvent e, UIUtility.EventListener listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey278()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__2FE)));
  }

  public static void AddEventListener<T0>(GameObject go, UnityEvent<T0> e, UIUtility.EventListener1Arg<T0> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey279<T0>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__2FF)));
  }

  public static void AddEventListener<T0, T1>(GameObject go, UnityEvent<T0, T1> e, UIUtility.EventListener2Arg<T0, T1> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey27A<T0, T1>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__300)));
  }

  public static void AddEventListener<T0, T1, T2>(GameObject go, UnityEvent<T0, T1, T2> e, UIUtility.EventListener3Arg<T0, T1, T2> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1, T2>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey27B<T0, T1, T2>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__301)));
  }

  public static void AddEventListener<T0, T1, T2, T3>(GameObject go, UnityEvent<T0, T1, T2, T3> e, UIUtility.EventListener4Arg<T0, T1, T2, T3> listener)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    e.AddListener(new UnityAction<T0, T1, T2, T3>((object) new UIUtility.\u003CAddEventListener\u003Ec__AnonStorey27C<T0, T1, T2, T3>()
    {
      listener = listener,
      go = go
    }, __methodptr(\u003C\u003Em__302)));
  }

  public enum DialogResults
  {
    None,
    Yes,
    No,
  }

  public struct DialogState
  {
    public UIUtility.DialogResults Result;
    public bool DoNotAskAgain;
  }

  public delegate void DialogResultEvent(GameObject dialog);

  public delegate void EventListener(GameObject go);

  public delegate void EventListener1Arg<T0>(GameObject go, T0 arg0);

  public delegate void EventListener2Arg<T0, T1>(GameObject go, T0 arg0, T1 arg1);

  public delegate void EventListener3Arg<T0, T1, T2>(GameObject go, T0 arg0, T1 arg1, T2 arg2);

  public delegate void EventListener4Arg<T0, T1, T2, T3>(GameObject go, T0 arg0, T1 arg1, T2 arg2, T3 arg3);
}
