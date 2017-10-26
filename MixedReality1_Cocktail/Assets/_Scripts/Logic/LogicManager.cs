using UnityEngine;

public class LogicManager : MonoBehaviour {

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

#region CurrentSessionInfo
    private Ingredient[] m_IngredientsOfCurrentSession;

    public Ingredient[] IngredientsOfCurrentSession
    {
        get { return m_IngredientsOfCurrentSession; }
        set
        {
            m_IngredientsOfCurrentSession = value;
            foreach(Ingredient i in m_IngredientsOfCurrentSession)
            {
                print("Selected Ingredient: " + i.m_Name + "\n");
            }
        }
    }

    private MixingMotion m_MotionOfCurrentSession;

    public MixingMotion MotionOfCurrentSession
    {
        get
        {
            return m_MotionOfCurrentSession;
        }

        set
        {
            m_MotionOfCurrentSession = value;
            print("Selected Motion: " + m_MotionOfCurrentSession.m_Name + "\n");
        }
    }
#endregion

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
}

