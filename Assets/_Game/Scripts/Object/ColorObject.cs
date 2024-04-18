using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorObject : GameUnit
{
    public ColorType color = ColorType.Default;

    [SerializeField] private Renderer render;
    [SerializeField] private ColorMaterial colorMaterial;

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        render.material = colorMaterial.GetColor(colorType);
    }
}