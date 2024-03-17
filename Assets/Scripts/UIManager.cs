using DefiMetaverse.Utils;
using deVoid.UIFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : AutoSingletonMono<UIManager>
{
    [SerializeField] private UISettings _uISetting;
    public UIFrame uIFrame { get; private set; }

    public override void Awake()
    {
        base.Awake();
        InitUISettings(_uISetting);
        Debug.Log("ui mnger");
    }


    private void InitUISettings(UISettings uiSettings)
    {
        uIFrame = uiSettings.CreateUIInstance(true);
    }
}
