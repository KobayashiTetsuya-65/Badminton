using UnityEngine;

public class CourtBuilder : MonoBehaviour
{
    [SerializeField] private float scale = 1f;
    [SerializeField] private bool isSingles = true;
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Material netMaterial;
    [SerializeField] private Material courtMaterial;
    public float courtLength = 13.4f;
    public float courtWidth = 0;
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        BuildCourt();
    }

    void BuildCourt()
    {
        courtLength = courtLength * scale;
        courtWidth = (isSingles ? 5.18f : 6.1f) * scale;

        floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.localScale = new Vector3(courtWidth, 1f, courtLength);
        floor.transform.position = new Vector3(0, -0.485f, 0);
        floor.name = "CourtFloor";
        floor.tag = "Floor";
        floor.layer = 8;
        if (courtMaterial != null)
        {
            floor.GetComponent<Renderer>().material = courtMaterial;
        }
        Rigidbody rigidbody = floor.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;

        GameObject net = GameObject.CreatePrimitive(PrimitiveType.Cube);
        net.transform.localScale = new Vector3(courtWidth, 1.55f, 0.05f);
        net.transform.position = new Vector3(0, 0.775f, 0);
        net.name = "Net";
        net.tag = "Net";
        if (netMaterial != null)
        {
            net.GetComponent<Renderer>().material = netMaterial;
        }
        Rigidbody _rb = net.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;

        CreateLine(new Vector3(courtWidth / 2f, 0.01f, 0), new Vector3(0.02f, 0.02f, courtLength), "RightLine");
        CreateLine(new Vector3(-courtWidth / 2f, 0.01f, 0), new Vector3(0.02f, 0.02f, courtLength), "LeftLine");
        CreateLine(new Vector3(0, 0.02f, courtLength / 2f), new Vector3(courtWidth, 0.02f, 0.02f), "BackLine");
        CreateLine(new Vector3(0, 0.02f, -courtLength / 2f), new Vector3(courtWidth, 0.02f, 0.02f), "FrontLine");
        CreateLine(new Vector3(0, 0.02f, 0), new Vector3(0.02f, 0.02f, courtLength), "CenterLine");
        CreateLine(new Vector3(0, 0.02f, 1.98f), new Vector3(courtWidth, 0.02f, 0.02f), "ShortServiceLineFront");
        CreateLine(new Vector3(0, 0.02f, -1.98f), new Vector3(courtWidth, 0.02f, 0.02f), "ShortServiceLineBack");
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
