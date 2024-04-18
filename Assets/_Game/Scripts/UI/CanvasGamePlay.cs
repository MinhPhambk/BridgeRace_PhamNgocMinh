using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    public void SettingButton()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
        LevelManager.Ins.StopCharacter();
        UIManager.Ins.OpenUI<CanvasSetting>();
        Close(0);
    }
}
