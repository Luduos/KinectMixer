using UnityEngine;
using System;

public class LogicManager : MonoBehaviour {

    [SerializeField]
    private KinectInput m_KinectInput;

    [SerializeField]
    private State[] m_States;
    private int m_CurrentStateID = 0;

    [SerializeField]
    private SelectionArea[] m_SelectionAreas;

    public SelectionArea[] SelectionAreas
    {
        get
        {
            return m_SelectionAreas;
        }

        set
        {
            m_SelectionAreas = value;
        }
    }

    private SessionInformation m_CurrentSessionInfo;

    public SessionInformation CurrentSessionInfo
    {
        get
        {
            return m_CurrentSessionInfo;
        }

        set
        {
            m_CurrentSessionInfo = value;
        }
    }

    public KinectInput KinectInput
    {
        get
        {
            return m_KinectInput;
        }

        set
        {
            m_KinectInput = value;
        }
    }

    void Start ()
    {
        m_States[m_CurrentStateID].OnEnterState(this);
    }
	
    /// <summary>
    /// Switches m_CurrentState to the next state and enters it.
    /// </summary>
	public void Switchstate()
    {
        m_CurrentStateID = (m_CurrentStateID + 1) % m_States.Length;
        m_States[m_CurrentStateID].OnEnterState(this);
    }

    

    /// <summary>
    /// Activate / Deactivate Selection areas. Deactivated Selection areas will not be rendered
    /// or updated.
    /// </summary>
    /// <param name="activated"></param>
    public void ActivateSelectionAreas(bool activated)
    {
        foreach(SelectionArea currentArea in m_SelectionAreas)
        {
            currentArea.gameObject.SetActive(activated);
        }
    }
}

[Serializable]
public struct SessionInformation
{
    public Ingredient[] IngredientsOfSession;
    public MixingMotion MotionOfSession;

    public override string ToString()
    {
        string infoString = "";
        for(int i = 0; i < IngredientsOfSession.Length; ++i)
        {
            Ingredient ingredient = IngredientsOfSession[i];
            infoString += "Selected Ingredient no " + (i+1) + ":" + ingredient.m_Name + "\n";
        }
        string motionName = MotionOfSession.m_Name;
        if(motionName.Length > 0)
        {
            infoString += "Selected Motion: " + motionName;
        }
        return infoString;
    }
}

