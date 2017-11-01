using UnityEngine;
using UnityEngine.Events;
public class SelectionArea : MonoBehaviour {
    public UnityAction<SelectionArea> OnAreaHoverEnterEvent;
    public UnityAction<SelectionArea> OnAreaHoverLeaveEvent;

    [SerializeField]
    private SelectionCollision m_SelectionCollider;

    [SerializeField]
    private SelectionAreaDisplayer m_SelectionAreaDisplay;

    [SerializeField]
    private Ingredient[] m_PossibleIngredients;

    private Ingredient m_CurrentIngredient;

    private float m_TimeUntilSelected = 2.0f;

    public float TimeUntilSelected
    {
        get
        {
            return m_TimeUntilSelected;
        }

        set
        {
            m_TimeUntilSelected = value;
        }
    }

    private float m_CurrentTimeInArea = 0.0f;

    public SelectionAreaDisplayer SelectionAreaDisplay
    {
        get
        {
            return m_SelectionAreaDisplay;
        }

        set
        {
            m_SelectionAreaDisplay = value;
        }
    }

    public Ingredient CurrentIngredient
    {
        get
        {
            return m_CurrentIngredient;
        }

        set
        {
            m_CurrentIngredient = value;
        }
    }

    private void Start()
    {
        if(null != m_SelectionCollider)
        {
            m_SelectionCollider.OnSelectionChanged += OnSelectionChanged;
        }
    }

    /// <summary>
    /// Activates / deactivates collider. If deactivated, player cannot select an ingredient
    /// by putting his hand / foot inside the collision box anymore.
    /// </summary>
    /// <param name="enable"></param>
    public void EnableCollider(bool enable)
    {
        if(null != m_SelectionCollider)
        {
            m_SelectionCollider.gameObject.SetActive(enable);
        }
    }

   private void OnSelectionChanged(bool IsSelected)
    {
        if (IsSelected)
        {
            OnAreaHoverEnterEvent.Invoke(this);
        }
        else
        {
            OnAreaHoverLeaveEvent.Invoke(this);
        }
    }

    /// <summary>
    /// Choose random ingredients from the array of possible ingredients
    /// </summary>
    public void ChooseRandomIngredient()
    {
        int randomIngredientID = Random.Range(0, m_PossibleIngredients.Length);
        CurrentIngredient = m_PossibleIngredients[randomIngredientID];

        SelectionAreaDisplay.DisplayIngredient(CurrentIngredient);
    }
}
