using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float G = 100f;


    public void Attract(Rigidbody body)
    {
        Vector3 direction = transform.position - body.position;

        // Apply downwards gravity to body
        body.AddForce(G * direction.normalized / direction.magnitude);
        // Allign bodies up axis with the centre of planet
        //body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
    }
}
