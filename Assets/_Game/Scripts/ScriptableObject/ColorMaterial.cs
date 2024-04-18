using UnityEngine;

[CreateAssetMenu(fileName = "ColorMaterial", menuName = "ScriptableObjects/ColorMaterial", order = 1)]
public class ColorMaterial : ScriptableObject
{
    [SerializeField] private Material[] colorMaterials;

    public Material GetColor(ColorType color)
    {
        return colorMaterials[(int)color];
    }
}