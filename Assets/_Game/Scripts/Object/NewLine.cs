using System;
using System.Collections.Generic;
using UnityEngine;

public class NewLine : MonoBehaviour
{
    [SerializeField] private Stage stage;

    private List<ColorType> characterColors;

    private void Start()
    {
        characterColors = new List<ColorType>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null && !characterColors.Contains(character.color))
        {
            character.SetStage(stage);
            characterColors.Add(character.color);
            stage.InitBrickWithColor(character.color);
        }
    }
}