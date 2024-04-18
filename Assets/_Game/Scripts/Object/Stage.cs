using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Stage : MonoBehaviour
{
    [SerializeField] private List<Transform> brickPositions;
    [SerializeField] private List<Vector3> emptyPositions;
    [SerializeField] private List<Brick> bricks;
    [SerializeField] private Brick brickPrefab;

    private void Start()
    {
        bricks = new List<Brick>();
        emptyPositions = new List<Vector3>();

        for (int i = 0; i < brickPositions.Count; i++)
        {
            emptyPositions.Add(brickPositions[i].transform.position);
        }
    }

    public void InitBrickWithColor(ColorType color)
    {
        for (int i = 0; i < Constant.NUMBER_BRICKS_GENERATED_PER_PLAYER; i++)
        {
            GenerateBrickWithColor(color);
        }
    }

    public void GenerateBrickWithColor(ColorType color)
    {
        if (emptyPositions.Count > 0)
        {
            int idx = Random.Range(0, emptyPositions.Count);
            Brick brick = SimplePool.Spawn<Brick>(brickPrefab, emptyPositions[idx], Quaternion.identity);
            brick.SetStage(this);
            brick.ChangeColor(color);
            emptyPositions.RemoveAt(idx);
            bricks.Add(brick);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        emptyPositions.Add(brick.TF.position);
        bricks.Remove(brick);
    }

    public Brick GetBrickByColor(ColorType colorType)
    {
        Brick brick = null;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].color == colorType)
            {
                brick = bricks[i];
                break;
            }
        }

        return brick;
    }
}