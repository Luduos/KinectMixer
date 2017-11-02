using UnityEngine;
using System.Collections;

public class DisplayResultState : State
{
    [SerializeField]
    private float m_TimeToWaitUntilRestart = 3.0f;

    [SerializeField]
    private FinalResultDisplayer m_FinalResultDisplay;

    private float m_TimeWaited = 0.0f;

    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);

        m_FinalResultDisplay.gameObject.SetActive(true);

        string toPrint = logicManager.CurrentSessionInfo.ToString();
        print(toPrint);

        float finalR = 0.0f;
        float finalG = 0.0f;
        float finalB = 0.0f;

        string finalResultName = "";
        foreach(Ingredient i in logicManager.CurrentSessionInfo.IngredientsOfSession)
        {
            finalR += i.m_Color.r;
            finalG += i.m_Color.g;
            finalB += i.m_Color.b;

            finalResultName += i.m_Name.Substring(0, 3);
        }
        finalResultName += "-Cocktail";

        finalR /= logicManager.CurrentSessionInfo.IngredientsOfSession.Length;
        finalG /= logicManager.CurrentSessionInfo.IngredientsOfSession.Length;
        finalB /= logicManager.CurrentSessionInfo.IngredientsOfSession.Length;

        FinalResult finalResult = new FinalResult();
        finalResult.m_Color = new Color(finalR, finalG, finalB);
        finalResult.m_Name = finalResultName;
        m_FinalResultDisplay.DisplayFinalResult(finalResult);

        // Example of how to start the coroutine to leave the state after a set amount of seconds.
        //StartCoroutine(LeaveAfterDisplayingResult());
    }


    /// <summary>
    /// Start this coroutine to leave the state after m_TimeToWaitUntilRestart - seconds
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Call this function, if you want to restart the mixing session
    /// </summary>
    protected override void OnLeaveState()
    {
        m_FinalResultDisplay.gameObject.SetActive(false);

        GameObject rightHand = LogicManager.KinectInput.GetFirstFoundRightHand();
        rightHand.GetComponent<Hand>().ShowCocktailSprite(false);

        m_TimeWaited = 0.0f;
        LogicManager.Switchstate();
    }
}
