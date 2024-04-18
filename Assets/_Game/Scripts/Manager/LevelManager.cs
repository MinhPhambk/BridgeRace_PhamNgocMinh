using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level[] levelPrefabs;
    [SerializeField] private Character[] characters;
    [SerializeField] private Transform characterTransform;

    private int level = 0;
    private Level currentLevel;

    private void Start()
    {
        GameManager.Ins.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<CanvasMainMenu>();
    }

    public void LoadLevel(int idx)
    {
        DestroyCurrentLevel();

        if (idx >= levelPrefabs.Length)
        {
            return;
        }

        currentLevel = Instantiate(levelPrefabs[idx]);
        InitNavMesh();

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] is Bot)
            {
                (characters[i] as Bot).ChangeState(new PatrolState());
            }
        }
    }

    public void ActiveCharacter()
    {
        for (int i = 0; i < characters.Length; i++) 
        {
            characters[i].gameObject.SetActive(true);
        }
    }

    public void DeactiveCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(false);
        }
    }

    public void StopCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Stop();
        }
    }

    public void MoveCharacter()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Move();
            
            if (characters[i] is Bot && currentLevel!= null)
            {
                (characters[i] as Bot).ChangeState(new PatrolState());
            }
        }
    }

    public int NextLevel(bool isNext = true)
    {
        if (isNext && (level + 1 < levelPrefabs.Length))
        {
            level += 1;
        }

        return level;
    }

    public void DestroyCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            SimplePool.CollectAll();
        }
    }

    public void ResetCharacter()
    {
        StopCharacter();
        DeactiveCharacter();
        
        var rng = new Random();
        rng.Shuffle(characters);

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].ClearBrick();
            characters[i].TF.rotation = Quaternion.identity;
            characters[i].TF.position = characterTransform.position + new Vector3(-6 + i * 4, 0, 0); 
            
            if (characters[i] is Bot)
            {
                (characters[i] as Bot).ChangeState(null);
            }
            
            characters[i].ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    public Level GetCurrentLevel()
    {
        return this.currentLevel;
    }

    public void InitNavMesh()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.GetNavMesh());
    }
}