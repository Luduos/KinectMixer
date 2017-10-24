using UnityEngine;

public class LogicManager : MonoBehaviour {

    [SerializeField]
    private State[] m_States;
    private int m_CurrentStateID = 0;

    private Ingredient[] m_IngredientsOfCurrentSession;

    public Ingredient[] IngredientsOfCurrentSession
    {
        get { return m_IngredientsOfCurrentSession; }
        set { m_IngredientsOfCurrentSession = value; }
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
}

