using UnityEngine;
using System;

[Serializable]
public struct MixingMotion
{
    public KinectGestures.Gestures m_Gesture;

    public string m_Name;
    /// <summary>
    /// The Sprite that should be displayed on the selection Areas to describe, which type of motion you should perform
    /// </summary>
    public Sprite m_DescriptionSprite;
    /// <summary>
    /// Sprite, that's displayed on the final result. (e.g. foam on top of the cocktail)
    /// </summary>
    public Sprite m_FinalDisplaySprite;
}