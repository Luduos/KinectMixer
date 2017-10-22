using UnityEngine;

public class SelectIngredientsState : State
{
    [SerializeField]
    private SelectionArea[] m_SelectionAreas;

    public override void OnEnter(LogicManager logicManager)
    {
        base.OnEnter(logicManager);
    }
}
