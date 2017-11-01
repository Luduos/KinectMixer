using System.Collections.Generic;
using UnityEngine;


public class SelectIngredientsState : State
{
    [Tooltip("How many ingredients can the player choose, before switching over to the mixing motions?")]
    [SerializeField]
    private int m_NumberOfIngredientChoices = 3;

    [Tooltip("How long does the player have to stay inside the selection area, to actually select an ingredient?")]
    [SerializeField]
    private float m_TimeUntilSelected = 1.0f;

    private int m_CurrentNumberOfSelectedIngredients = 0;

    private Ingredient[] m_SelectedIngredients;

    // List of areas which have been selected (hovered over) by a Kinect Hand
    private List<SelectionArea> m_CurrentlyHoveredAreas = new List<SelectionArea>();

    private void Start()
    {
        // Register function listening to event, when player enters seleciton area
        foreach (SelectionArea area in LogicManager.SelectionAreas)
        {
            area.OnAreaHoverEnterEvent += OnAreaHoverEnter;
            area.OnAreaHoverLeaveEvent += OnAreaHoverLeave;
            area.TimeUntilSelected = m_TimeUntilSelected;
        }

        m_SelectedIngredients = new Ingredient[m_NumberOfIngredientChoices];
    }

    /// <summary>
    /// Chooses random ingredients in the seleciton areas, activates ingredient selection and activates the selection areas
    /// </summary>
    /// <param name="logicManager"></param>
    public override void OnEnterState(LogicManager logicManager)
    {
        base.OnEnterState(logicManager);
        ShuffleIngredientsInSelectionArea();
        ActivateIngredientSelection(true);
        LogicManager.ActivateSelectionAreas(true);
    }

    /// <summary>
    /// Resets ingredient selection, saves the selected ingredients and calls the switch to the next state
    /// </summary>
    protected override void OnLeaveState()
    {
        m_CurrentNumberOfSelectedIngredients = 0;
        ActivateIngredientSelection(false);
        SessionInformation info = LogicManager.CurrentSessionInfo;
        info.IngredientsOfSession = m_SelectedIngredients;
        LogicManager.CurrentSessionInfo = info;
        LogicManager.Switchstate();
    }

    /// <summary>
    /// Activates / Deactivates Ingredient selection by enabling / disabling the
    /// collision boxes of the selection areas. Trigger events will not be propagated,
    /// if a collision box is disabled.
    /// </summary>
    /// <param name="enabled"></param>
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

    private void OnAreaHoverEnter(SelectionArea selectedArea)
    {
        if (!m_CurrentlyHoveredAreas.Contains(selectedArea))
        {
            m_CurrentlyHoveredAreas.Add(selectedArea);
        } 
        // TODO: Handle Selection Timer and highlight selected area
    }

    private void OnAreaHoverLeave(SelectionArea selectedArea)
    {
        if (m_CurrentlyHoveredAreas.Contains(selectedArea))
        {
            m_CurrentlyHoveredAreas.Remove(selectedArea);
        }
    }

    private void FinalizeIngredientSelection()
    {

        m_SelectedIngredients[m_CurrentNumberOfSelectedIngredients] = m_CurrentlyHoveredAreas[0].CurrentIngredient;
        m_CurrentNumberOfSelectedIngredients++;
        ShuffleIngredientsInSelectionArea();
        if (m_CurrentNumberOfSelectedIngredients == m_NumberOfIngredientChoices)
        {
            OnLeaveState();
        }
    }
}
