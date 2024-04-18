using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        LevelManager.Ins.MoveCharacter();
        Close(0);
    }

    public void MainMenuButton()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
        LevelManager.Ins.ResetCharacter();
        LevelManager.Ins.DestroyCurrentLevel();
        UIManager.Ins.OpenUI<CanvasMainMenu>();
        Close(0);
    }
}
