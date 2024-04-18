using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWin : UICanvas
{
    public void RetryButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        LevelManager.Ins.ResetCharacter();
        LevelManager.Ins.ActiveCharacter();
        LevelManager.Ins.MoveCharacter();

        int level = LevelManager.Ins.NextLevel(false);
        LevelManager.Ins.LoadLevel(level);

        Close(0);
    }

    public void NextLevelButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI<CanvasGamePlay>();
        LevelManager.Ins.ResetCharacter();
        LevelManager.Ins.ActiveCharacter();
        LevelManager.Ins.MoveCharacter();

        int level = LevelManager.Ins.NextLevel(true);
        LevelManager.Ins.LoadLevel(level);

        Close(0);
    }
}