using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class SelectionArea : MonoBehaviour {
    public UnityAction<Ingredient> OnEnteredSelectionArea;

    [SerializeField]
    private Ingredient[] m_PossibleIngredients;

    private Ingredient m_CurrentIngredient;

    public void ChooseRandomIngredient()
    {
        int randomIngredientID = Random.Range(0, m_PossibleIngredients.Length);
        m_CurrentIngredient = m_PossibleIngredients[randomIngredientID];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(null != OnEnteredSelectionArea)
        {
            OnEnteredSelectionArea.Invoke(m_CurrentIngredient);
        }
    }

}
