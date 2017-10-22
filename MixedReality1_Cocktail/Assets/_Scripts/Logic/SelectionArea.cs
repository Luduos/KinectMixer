using UnityEngine;

public enum SelectionAreaType
{
    Fruit,
    Alcohol,
    NonAlcohol
}

[RequireComponent(typeof(BoxCollider))]
public class SelectionArea : MonoBehaviour {

    [SerializeField]
    private SelectionAreaType AreaType;

    private void OnTriggerEnter(Collider other)
    {
        // TODO
    }

    private void OnCollisionEnter(Collision collision)
    {
        //TODO
    }
}
