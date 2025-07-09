using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool MarkerSetting = true;
    private float x = 0f;
    private float z = 5f;
    Transform _tr;
    // Start is called before the first frame update
    void Start()
    {
        _tr = transform;
        Set();
    }

    // Update is called once per frame
    void Update()
    {
        if (MarkerSetting)
        {
            float horizontal = 0f;
            float vertical = 0f;
            if (Input.GetKey(KeyCode.I))
            {
                vertical = speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.J))
            {
                horizontal = -speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.K))
            {
                vertical = -speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.L))
            {
                horizontal = speed * Time.deltaTime;
            }
            x += horizontal;
            z += vertical;
            x = Mathf.Clamp(x, -3.5f, 3.5f);
            z = Mathf.Clamp(z, 0.5f, 7.5f);
            _tr.position = new Vector3(x, 0f, z);
        }
    }
    public void Set()
    {
        x = 0f;
        z = 5f;
        _tr.position = new Vector3(x, 0f, z);
    }
}
