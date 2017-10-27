using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SelectionCollision : MonoBehaviour {

    private SelectionArea m_OwningSelectionArea;

    public void Init(SelectionArea owner)
    {
        m_OwningSelectionArea = owner;
    }

    /// <summary>
    /// Called, when other objects stays in collision box. Invokes event, when an ingredient was selected
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        m_OwningSelectionArea.CurrentTimeInArea += Time.deltaTime;
        if (m_OwningSelectionArea.CurrentTimeInArea > m_OwningSelectionArea.TimeUntilSelected)
        {
            m_OwningSelectionArea.CurrentTimeInArea = 0.0f;
            m_OwningSelectionArea.IngredientWasSelected();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_OwningSelectionArea.CurrentTimeInArea = 0.0f;
    }
}
