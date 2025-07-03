using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtBuilder : MonoBehaviour
{
    [SerializeField] private float scale = 1f;
    [SerializeField] private bool isSingles = true;
    [SerializeField] private Material lineMaterial;
    private float courtLength = 13.4f;
    // Start is called before the first frame update
    void Start()
    {
        BuildCourt();
    }

    void BuildCourt()
    {
        courtLength = courtLength * scale;
        float courtWidth = (isSingles ? 5.18f : 6.1f) * scale;

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.transform.localScale = new Vector3(courtWidth / 10f, 1, courtLength / 10f);
        floor.transform.position = new Vector3(0, 0.001f, 0);
        floor.name = "CourtFloor";

        GameObject net = GameObject.CreatePrimitive(PrimitiveType.Cube);
        net.transform.localScale = new Vector3(courtWidth, 1.55f, 0.05f);
        net.transform.position = new Vector3(0, 0.775f, 0);
        net.name = "Net";

        CreateLine(new Vector3(courtWidth / 2f, 0.01f, 0), new Vector3(0.02f, 0.02f, courtLength), "RightLine");
        CreateLine(new Vector3(-courtWidth / 2f, 0.01f, 0), new Vector3(0.02f, 0.02f, courtLength), "LeftLine");
        CreateLine(new Vector3(0, 0.01f, courtLength / 2f), new Vector3(courtWidth, 0.02f, 0.02f), "BackLine");
        CreateLine(new Vector3(0, 0.01f, -courtLength / 2f), new Vector3(courtWidth, 0.02f, 0.02f), "FrontLine");
        CreateLine(new Vector3(0, 0.01f, 0), new Vector3(0.02f, 0.02f, courtLength), "CenterLine");
        CreateLine(new Vector3(0, 0.01f, 1.98f), new Vector3(courtWidth, 0.02f, 0.02f), "ShortServiceLineFront");
        CreateLine(new Vector3(0, 0.01f, -1.98f), new Vector3(courtWidth, 0.02f, 0.02f), "ShortServiceLineBack");
        if (!isSingles)
        {
            CreateLine(new Vector3(0, 0.01f, -courtLength / 2f + 0.76f), new Vector3(courtWidth, 0.02f, 0.02f), "LongServiceLineBack");
            CreateLine(new Vector3(0, 0.01f, courtLength / 2f - 0.76f), new Vector3(courtWidth, 0.02f, 0.02f), "LongServiceLineFront");
        }
    }

    void CreateLine(Vector3 pos, Vector3 scale, string name)
    {
        GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cube);
        line.transform.position = pos;
        line.name = name;
        line.transform.localScale = scale;
        if (lineMaterial != null)
        {
            line.GetComponent<Renderer>().material = lineMaterial;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
