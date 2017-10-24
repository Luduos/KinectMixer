using UnityEngine;
using System;

[Serializable]
public struct MixingMotion
{
    public enum MixingMotionType
    {
        Horizontal,
        Vertical,
        Circle
    }

    public string m_Name;
    public Sprite m_Sprite;
    public MixingMotionType m_Type;
}