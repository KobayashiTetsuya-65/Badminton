using UnityEngine;

public class Racket : MonoBehaviour
{

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
}
