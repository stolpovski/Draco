using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float G = 100f;


    public void Attract(Rigidbody body)
    {
        Vector3 direction = transform.position - body.position;

        body.AddForce(G * direction.normalized / direction.magnitude);
    }
}
