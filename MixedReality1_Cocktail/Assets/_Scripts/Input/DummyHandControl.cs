using UnityEngine;

public class DummyHandControl : MonoBehaviour {

    [SerializeField]
    private float m_MovementSpeed = 1.0f;

	void FixedUpdate () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, m_MovementSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, - m_MovementSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-m_MovementSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(m_MovementSpeed * Time.deltaTime, 0, 0);
        }
    }
}
