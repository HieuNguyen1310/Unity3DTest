using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    public Color color = Color.green; // Set your desired color

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
    }
}
