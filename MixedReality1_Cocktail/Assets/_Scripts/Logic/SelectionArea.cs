using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class SelectionArea : MonoBehaviour {
    public UnityAction<Ingredient> OnSelectedIngredient;

    [SerializeField]
    private Ingredient[] m_PossibleIngredients;

    [SerializeField]
    private float m_TimeToSelect = 2.0f;

    private float m_CurrentTimeInArea = 0.0f;

    private Ingredient m_CurrentIngredient;

    /// <summary>
    /// Choose random ingredients from the array of possible ingredients
    /// </summary>
    public void ChooseRandomIngredient()
    {
        int randomIngredientID = Random.Range(0, m_PossibleIngredients.Length);
        m_CurrentIngredient = m_PossibleIngredients[randomIngredientID];


    }

    /// <summary>
    /// Called, when other objects stays in collision box. Invokes event, when an ingredient was selected
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        m_CurrentTimeInArea += Time.deltaTime;
        if(m_CurrentTimeInArea > m_TimeToSelect)
        {
            m_CurrentTimeInArea = 0.0f;
            OnSelectedIngredient.Invoke(m_CurrentIngredient);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_CurrentTimeInArea = 0.0f;
    }
}
