using UnityEngine;


public class MixingMotionState : State
{
    [SerializeField]
    private MotionDetector m_MotionDetector;

    [Tooltip("Shouldn't contain more than Motions than SelectionAreas.")]
    [SerializeField]
    private MixingMotion[] m_PossibleMixingMotions;

    private void Start()
    {
        if(null != m_MotionDetector)
        {
            m_MotionDetector.OnMotionSelected += SelectMotion;
            m_MotionDetector.enabled = false;
        }
    }

    /// <summary>
    /// Displays mixing motions, as long as there are possible mixing motions and selection areas
    /// that can display the mixing motion information
    /// </summary>
    /// <param name="logicManager"></param>
    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);
        m_MotionDetector.enabled = true;
        SelectionArea[] selectionAreas = logicManager.SelectionAreas;
        for (int i = 0; i < selectionAreas.Length; ++i)
        {
            if(m_PossibleMixingMotions.Length <= i)
                break;

            selectionAreas[i].DisplayMotion(m_PossibleMixingMotions[i]);
        }
    }

    /// <summary>
    /// Select motion from the list of possible mixing motions. Only accepts valid motions.
    /// </summary>
    /// <param name="motionID"></param>
    private void SelectMotion(int motionID)
    {
        bool invalid = motionID >= m_PossibleMixingMotions.Length || motionID < 0;
        if (!invalid)
        {
            LogicManager.MotionOfCurrentSession = m_PossibleMixingMotions[motionID];
            OnLeaveState();
        }
        else
        {
            print("Invalid motion ID");
        }
    }

    protected override void OnLeaveState()
    {
        m_MotionDetector.enabled = false;
        LogicManager.Switchstate();
    }
}
