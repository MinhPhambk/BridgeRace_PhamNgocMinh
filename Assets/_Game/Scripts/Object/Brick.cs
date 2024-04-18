using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    private Stage stage;

    public void OnDespawn()
    {
        stage.RemoveBrick(this);
    }

    public void SetStage(Stage stage)
    {
        this.stage = stage;
    }
}
