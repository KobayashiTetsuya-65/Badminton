using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtBuilder : MonoBehaviour
{
    [SerializeField] private bool isSingles = true;
    [SerializeField] private Material line;
    private float courtLength = 13.4f;
    // Start is called before the first frame update
    void Start()
    {
        BuildCourt();
    }

    void BuildCourt()
    {
        float courtWidth = isSingles ? 5.18f : 6.1f;

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.localScale = new Vector3(courtWidth / 10f, 1, courtLength / 10f);
        floor.transform.position = new Vector3(0, -0.5f, 0);
        floor.name = "CourtFloor";

        GameObject net = GameObject.CreatePrimitive(PrimitiveType.Cube);
        net.transform.localScale = new Vector3(courtWidth, 1.55f, 0.05f);
        net.transform.position = new Vector3(0, 0.775f, 0);
        net.name = "Net";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
