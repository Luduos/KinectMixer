using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(BoxCollider))]
public class SelectionCollision : MonoBehaviour {

    public UnityAction<bool> OnSelectionChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            if(null != OnSelectionChanged)
            {
                OnSelectionChanged.Invoke(true);
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            if (null != OnSelectionChanged)
            {
                OnSelectionChanged.Invoke(false);
            }
        }
    }
}
