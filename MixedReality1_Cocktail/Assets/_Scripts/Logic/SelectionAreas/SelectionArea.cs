using UnityEngine;
using UnityEngine.Events;
public class SelectionArea : MonoBehaviour {
    public UnityAction<Ingredient> OnAreaWasSelected;

    [SerializeField]
    private SelectionCollision m_SelectionCollider;

    [SerializeField]
    private SelectionAreaDisplayer m_SelectionAreaDisplayer;

    [SerializeField]
    private float m_TimeToSelect = 2.0f;

    private float m_CurrentTimeInArea = 0.0f;

    [SerializeField]
    private Ingredient[] m_PossibleIngredients;

    private Ingredient m_CurrentIngredient;

    public float TimeToSelect
    {
        get
        {
            return m_TimeToSelect;
        }
    }

    public float CurrentTimeInArea
    {
        get
        {
            return m_CurrentTimeInArea;
        }

        set
        {
            m_CurrentTimeInArea = value;
        }
    }

    private void Start()
    {
        if(null != m_SelectionCollider)
        {
            m_SelectionCollider.Init(this);
        }
    }

    public void IngredientWasSelected()
    {
        OnAreaWasSelected.Invoke(m_CurrentIngredient);
    }

    /// <summary>
    /// Choose random ingredients from the array of possible ingredients
    /// </summary>
    public void ChooseRandomIngredient()
    {
        int randomIngredientID = Random.Range(0, m_PossibleIngredients.Length);
        m_CurrentIngredient = m_PossibleIngredients[randomIngredientID];

        m_SelectionAreaDisplayer.DisplayIngredient(m_CurrentIngredient);
    }
}
