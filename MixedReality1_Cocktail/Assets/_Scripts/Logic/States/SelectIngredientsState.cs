using UnityEngine;

public class SelectIngredientsState : State
{
    [SerializeField]
    private SelectionArea[] m_SelectionAreas;

    [SerializeField]
    private int m_NumberOfSelectableIngredients = 3;

    private int m_CurrentNumberOfSelectedIngredients = 0;

    private Ingredient[] m_SelectedIngredients;

    private void Start()
    {
        // Register function listening to event, when player enters seleciton area
        foreach (SelectionArea area in m_SelectionAreas)
        {
            area.OnAreaWasSelected += OnIngredientWasSelected;
        }

        m_SelectedIngredients = new Ingredient[m_NumberOfSelectableIngredients];
    }

    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);
        ShuffleIngredientsInSelectionArea();
        ActivateSelectionAreas(true);
    }

    protected override void OnLeaveState()
    {
        m_CurrentNumberOfSelectedIngredients = 0;
        ActivateSelectionAreas(false);
        LogicManager.IngredientsOfCurrentSession = m_SelectedIngredients;
        LogicManager.Switchstate();
    }

    private void ActivateSelectionAreas(bool enabled)
    {
        foreach (SelectionArea area in m_SelectionAreas)
        {
            area.gameObject.SetActive(enabled);
        }
    }

    /// <summary>
    /// Selects Random ingredients in every Selection Area
    /// </summary>
    private void ShuffleIngredientsInSelectionArea()
    {
        foreach(SelectionArea area in m_SelectionAreas)
        {
            area.ChooseRandomIngredient();
        }
    }

    /// <summary>
    /// Invoked by SelectionArea, once an ingredient was selected
    /// </summary>
    private void OnIngredientWasSelected(Ingredient selectedIngredient)
    {
        m_SelectedIngredients[m_CurrentNumberOfSelectedIngredients] = selectedIngredient;
        m_CurrentNumberOfSelectedIngredients++;
        ShuffleIngredientsInSelectionArea();
        if (m_CurrentNumberOfSelectedIngredients == m_NumberOfSelectableIngredients)
        {
            OnLeaveState();
        }
    }
}
