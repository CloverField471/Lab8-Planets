using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<Gravity> planetList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(planetList == null)
        {
            planetList = new List<Gravity>();
        }

        planetList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach(var planet in planetList)
        {
            if(planet != this)
            {
                Attract(planet);
            }
        }
    }
    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        //Get distance in meter
        float distance = direction.magnitude;

        //Calculate Gravity Force 
        float forceMagnitude = G * ((rb.mass * otherRb.mass) / Mathf.Pow(distance, 2));
        Vector3 finalForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(finalForce);
    }
}
