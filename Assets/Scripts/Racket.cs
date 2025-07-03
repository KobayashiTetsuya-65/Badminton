using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shuttle"))
        {
            Shuttle shuttle = other.GetComponent<Shuttle>();
            if (shuttle != null)
            {

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
