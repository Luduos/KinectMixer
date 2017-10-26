using UnityEngine;

public class SelectIngredientsState : State
{
    [Tooltip("How many ingredients can the player choose, before switching over to the mixing motions?")]
    [SerializeField]
    private int m_NumberOfIngredientChoices = 3;

    private int m_CurrentNumberOfSelectedIngredients = 0;

    private Ingredient[] m_SelectedIngredients;

    private void Start()
    {
        // Register function listening to event, when player enters seleciton area
        foreach (SelectionArea area in LogicManager.SelectionAreas)
        {
            area.OnAreaWasSelected += OnIngredientWasSelected;
        }

        m_SelectedIngredients = new Ingredient[m_NumberOfIngredientChoices];
    }

    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);
        ShuffleIngredientsInSelectionArea();
        ActivateIngredientSelection(true);
    }

    protected override void OnLeaveState()
    {
        m_CurrentNumberOfSelectedIngredients = 0;
        ActivateIngredientSelection(false);
        LogicManager.IngredientsOfCurrentSession = m_SelectedIngredients;
        LogicManager.Switchstate();
    }

    private void ActivateIngredientSelection(bool enabled)
    {
        foreach (SelectionArea area in LogicManager.SelectionAreas)
        {
            area.EnableCollider(enabled);
        }
    }

    /// <summary>
    /// Selects Random ingredients in every Selection Area
    /// </summary>
    private void ShuffleIngredientsInSelectionArea()
    {
        foreach(SelectionArea area in LogicManager.SelectionAreas)
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
        if (m_CurrentNumberOfSelectedIngredients == m_NumberOfIngredientChoices)
        {
            OnLeaveState();
        }
    }
}
