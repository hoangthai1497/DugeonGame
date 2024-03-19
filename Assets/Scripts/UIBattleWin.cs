using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;
using Dugeon.Define;

public class UIBattleWin : APanelController
{
    [SerializeField] private Button _okBtn;

    protected override void AddListeners()
    {
        base.AddListeners();
        _okBtn.onClick.AddListener(OnClickButtonOk);
    }

    private void OnClickButtonOk()
    {
        //change scene
        UIManager.Instance.uIFrame.HidePanel(ScreenIds.UIBattleWin);
    }
}
