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
            ActivateMotionDetector(false);
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
        ActivateMotionDetector(true);
        LogicManager.ActivateSelectionAreas(true);

        SelectionArea[] selectionAreas = logicManager.SelectionAreas;
        for (int i = 0; i < selectionAreas.Length; ++i)
        {
            if(m_PossibleMixingMotions.Length <= i)
                break;

            selectionAreas[i].SelectionAreaDisplay.DisplayMixingMotion(m_PossibleMixingMotions[i]);
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
            SessionInformation info = LogicManager.CurrentSessionInfo;
            info.MotionOfSession = m_PossibleMixingMotions[motionID];
            LogicManager.CurrentSessionInfo = info;
            OnLeaveState();
        }
        else
        {
            print("Invalid motion ID");
        }
    }

    /// <summary>
    /// Activate / Deactivate the motion detector gameobject
    /// </summary>
    /// <param name="activated"></param>
    private void ActivateMotionDetector(bool activated)
    {
        if(null != m_MotionDetector)
        {
            m_MotionDetector.gameObject.SetActive(activated);
        }
    }

    protected override void OnLeaveState()
    {
        ActivateMotionDetector(false);
        LogicManager.ActivateSelectionAreas(false);
        LogicManager.Switchstate();
    }
}
