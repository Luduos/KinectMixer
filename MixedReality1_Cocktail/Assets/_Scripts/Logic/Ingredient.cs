using UnityEngine;
using System;

[Serializable]
public struct Ingredient {
    [TextArea(1, 2)]
    public string m_Name;
    public Sprite m_Sprite;
    public Color m_Color;
}