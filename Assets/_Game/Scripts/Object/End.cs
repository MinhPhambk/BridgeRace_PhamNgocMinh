using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Player>() == null ? other.GetComponent<Bot>() : other.GetComponent<Player>();

        if (character != null)
        {
            GameManager.Ins.ChangeState(GameState.Pause);
            LevelManager.Ins.StopCharacter();
            UIManager.Ins.CloseUI<CanvasGamePlay>();

            if (character is Player)
            {
                UIManager.Ins.OpenUI<CanvasWin>();
            }
            else
            {
                UIManager.Ins.OpenUI<CanvasLose>();
            }

            character.TF.eulerAngles = Vector3.up * 180;
            character.ClearBrick();
            character.ChangeAnim(Constant.ANIM_DANCE);
        }
    }
}