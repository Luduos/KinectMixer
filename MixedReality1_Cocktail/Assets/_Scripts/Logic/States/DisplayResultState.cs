using UnityEngine;
using System.Collections;

public class DisplayResultState : State
{
    [SerializeField]
    private float m_TimeToWaitUntilRestart = 3.0f;

    private float m_TimeWaited = 0.0f;

    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);
        string toPrint = logicManager.CurrentSessionInfo.ToString();
        print(toPrint);

        StartCoroutine(LeaveAfterDisplayingResult());
    }

    private IEnumerator LeaveAfterDisplayingResult()
    {
        while(m_TimeWaited < m_TimeToWaitUntilRestart)
        {
            m_TimeWaited += Time.deltaTime;
            yield return null;
        }
        
        OnLeaveState();
        yield break; 
    }

    protected override void OnLeaveState()
    {
        m_TimeWaited = 0.0f;
        LogicManager.Switchstate();
    }
}
