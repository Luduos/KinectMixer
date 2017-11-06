using UnityEngine;
using System;

[Serializable]
public struct FinalResult
{
    [TextArea(1, 2)]
    public string m_Name;
    public Color m_Color;
    public Sprite m_Foam;
    public Color m_FoamColor;
}
