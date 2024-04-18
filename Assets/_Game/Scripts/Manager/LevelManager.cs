using System;
using UnityEngine;
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
        }
    }

    public Level GetCurrentLevel()
    {
        return this.currentLevel;
    }
}