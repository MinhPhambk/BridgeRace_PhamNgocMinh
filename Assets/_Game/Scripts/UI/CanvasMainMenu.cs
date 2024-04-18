using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        GameManager.Ins.ChangeState(GameState.Gameplay);
        UIManager.Ins.OpenUI<CanvasGamePlay>();        
        LevelManager.Ins.ActiveCharacter();
        LevelManager.Ins.MoveCharacter();

        int level = LevelManager.Ins.NextLevel(false);
        LevelManager.Ins.LoadLevel(level);

        Close(0);
    }
}

