using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joystickCircle;
    [SerializeField] private RectTransform joystickDot;
    [SerializeField] private GameObject joystickPanel;

    private static Vector3 direction;
    private Vector3 startPosition;

    void Awake()
    {
        direction = Vector3.zero;
        joystickPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
                joystickCircle.anchoredPosition = startPosition;
                joystickDot.anchoredPosition = startPosition;
                joystickPanel.SetActive(true);
            }

            if (Input.GetMouseButtonUp(0))
            {
                joystickPanel.SetActive(false);
                direction = Vector3.zero;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 nextPosition = Input.mousePosition - new Vector3(Screen.width, Screen.height) / 2;
                joystickDot.anchoredPosition = Vector3.ClampMagnitude((nextPosition - startPosition), 110f) + startPosition;
                direction = new Vector3(nextPosition.x - startPosition.x, 0f, nextPosition.y - startPosition.y).normalized;
            }
        }
        else
        {
            joystickPanel.SetActive(false);
        }
    }

    private void OnDisable()
    {
        direction = Vector3.zero;
    }

    public static Vector3 GetDirection()
    {
        return direction;
    }
}
