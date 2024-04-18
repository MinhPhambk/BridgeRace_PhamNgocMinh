using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void Update()
    {
        if (!stopped)
        {
            if (Input.GetMouseButton(0))
            {
                if (JoystickController.GetDirection() != Vector3.zero)
                {
                    TF.forward = JoystickController.GetDirection();
                    Vector3 newPosition = speed * Time.deltaTime * JoystickController.GetDirection() + TF.position;

                    if (CheckMove(newPosition))
                    {
                        TF.position = NextMove(newPosition);
                    }
                }

                ChangeAnim(Constant.ANIM_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnim(Constant.ANIM_IDLE);
            }
        }
        else
        {
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }
}
