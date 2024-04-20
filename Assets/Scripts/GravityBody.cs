using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    HashSet<GravityAttractor> planets = new();
    GravityAttractor planet;
    Rigidbody body;

    void Awake()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Planet"))
        {
            planets.Add(go.GetComponent<GravityAttractor>());
        }
        
        body = GetComponent<Rigidbody>();

        // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
        body.useGravity = false;
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        foreach (GravityAttractor planet in planets)
        {
            planet.Attract(body);

        }
        // Allow this body to be influenced by planet's gravity
    }
}
