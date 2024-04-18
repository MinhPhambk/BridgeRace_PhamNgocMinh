using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Character : ColorObject
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected Animator anim;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask stairLayer;
    [SerializeField] protected GameObject brickStack;

    private Stage stage;

    protected bool stopped = false;
    protected string currentAnim;
    protected Stack<Brick> bricks;

    private void Start()
    {
        bricks = new Stack<Brick>();
        ChangeColor(this.color);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BRICK))
        {
            Brick brick = other.GetComponent<Brick>();

            if (brick.color == this.color)
            {
                brick.OnDespawn();
                AddBrick(brick);
            }
        }
    }

    private void AddBrick(Brick brick)
    {
        brick.TF.SetParent(brickStack.transform);
        brick.TF.rotation = this.TF.rotation;
        brick.TF.position = brickStack.transform.position + bricks.Count * new Vector3(0f, 0.25f, 0f);
        bricks.Push(brick);
    }

    private void RemoveBrick()
    {
        if (bricks.Count > 0)
        {
            Brick brick = bricks.Pop();
            Destroy(brick.gameObject);
        }
    }

    protected bool CheckMove(Vector3 newPosition)
    {
        bool moved = true;

        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit hit, 2f, stairLayer))
        {
            Stair stair = hit.collider.gameObject.GetComponent<Stair>();

            if (stair.color != this.color && bricks.Count > 0)
            {
                stair.ChangeColor(color);
                RemoveBrick();
                stage.GenerateBrickWithColor(color);
            }

            if (stair.color != this.color && bricks.Count == 0 && TF.forward.z > 0)
            {
                moved = false;
            }
        }

        return moved;
    }

    protected Vector3 NextMove(Vector3 newPosition)
    {
        if (Physics.Raycast(newPosition, Vector3.down, out RaycastHit hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }

        return TF.position;
    }

    public void SetStage(Stage stage) 
    {
        this.stage = stage;
    }

    public Stage GetStage() 
    {
        return this.stage;
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (!string.IsNullOrEmpty(currentAnim))
            {
                anim.ResetTrigger(currentAnim);
            }

            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void ClearBrick()
    {
        while (bricks.Count > 0)
        {
            Brick brick = bricks.Pop();
            Destroy(brick.gameObject);
        }
        bricks.Clear();
    }

    public virtual void Stop()
    {
        stopped = true;
    }

    public virtual void Move()
    {
        stopped = false;
    }

    public int GetBrickCount()
    {
        return bricks.Count;
    }
}
