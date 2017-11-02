using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class SelectIngredientsState : State
{
    public UnityAction<Ingredient> OnSelectionFinalized;

    [Tooltip("How many ingredients can the player choose, before switching over to the mixing motions?")]
    [SerializeField]
    private int m_NumberOfIngredientChoices = 3;

    [Tooltip("How long does the player have to stay inside the selection area, to actually select an ingredient?")]
    [SerializeField]
    private float m_TimeUntilSelected = 1.0f;

    [SerializeField]
    private float m_TimeToDisplaySelectedIngredient = 2.0f;

    private int m_CurrentNumberOfSelectedIngredients = 0;

    private Ingredient[] m_SelectedIngredients;

    // List of areas which have been selected (hovered over) by a Kinect Hand
    private System.Collections.Generic.List<SelectionArea> m_CurrentlyHoveredAreas = new System.Collections.Generic.List<SelectionArea>();

    private Coroutine m_TimerCoroutine;

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
            CheckSelection();
        } 
    }

    private void OnAreaHoverLeave(SelectionArea selectedArea)
    {
        if (m_CurrentlyHoveredAreas.Contains(selectedArea))
        {
            m_CurrentlyHoveredAreas.Remove(selectedArea);
            selectedArea.SelectionAreaDisplay.NoHighlights();
            CheckSelection();
        }
    }

    /// <summary>
    /// Starts / Stops timer, depending on whether or not we have more than on selected area
    /// </summary>
    private void CheckSelection()
    {
        if(m_CurrentlyHoveredAreas.Count == 1)
        {
            StartTimer();
            m_CurrentlyHoveredAreas[0].SelectionAreaDisplay.HighlightOnHovered();
        }
        else
        {
            StopTimer();
            DisplayInvalidHovering();
        }
    }

    private void StartTimer()
    {
        m_TimerCoroutine = StartCoroutine(SelectionTimer());
    }

    private void DisplayInvalidHovering()
    {
        foreach(SelectionArea area in m_CurrentlyHoveredAreas)
        {
            area.SelectionAreaDisplay.HighlightOnInvalidHovered();
        }
    }

    private void StopTimer()
    {
        if(null != m_TimerCoroutine)
        {
            StopCoroutine(m_TimerCoroutine);
            print("Stopped timer");
        }
    }


    private IEnumerator SelectionTimer()
    {
        float deltaTime = 0.0f;
        while(deltaTime < m_TimeUntilSelected)
        {
            deltaTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(DisplaySelectedIngredient());
    }

    /// <summary>
    /// This is called, once the ingredient was selected. It starts the feedback for the user
    /// which selection was made (f.e. animating an object into the mixer or showing green background)
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplaySelectedIngredient()
    {
        m_CurrentlyHoveredAreas[0].SelectionAreaDisplay.HighlightOnSelected();
        if(null != OnSelectionFinalized)
        {
            OnSelectionFinalized.Invoke(m_CurrentlyHoveredAreas[0].CurrentIngredient);
        }
        ActivateIngredientSelection(false);

        float timeWaited = 0.0f;
        while(timeWaited < m_TimeToDisplaySelectedIngredient)
        {
            timeWaited += Time.deltaTime;
            yield return null;
        }
        FinalizeIngredientSelection();
    }

    private void FinalizeIngredientSelection()
    {
        m_SelectedIngredients[m_CurrentNumberOfSelectedIngredients] = m_CurrentlyHoveredAreas[0].CurrentIngredient;
        m_CurrentlyHoveredAreas[0].SelectionAreaDisplay.NoHighlights();
        m_CurrentNumberOfSelectedIngredients++;
        ShuffleIngredientsInSelectionArea();

        m_CurrentlyHoveredAreas.Clear();
        ActivateIngredientSelection(true);

        if (m_CurrentNumberOfSelectedIngredients == m_NumberOfIngredientChoices)
        {
            OnLeaveState();
        }
        else
        {
            CheckSelection();
        }
    }
}
