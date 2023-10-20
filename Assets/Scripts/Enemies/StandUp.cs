using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class StandUp : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        m_RigidBody = transform.root.GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        transform.up = Vector2.up;
        var motionX = m_RigidBody.velocity.x;
        m_SpriteRenderer.flipX = !(motionX > 0);
    }
}
