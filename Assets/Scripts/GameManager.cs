using UnityEngine;
/// <summary>
/// “G‚Ì’e‚Ì—Ž‰º’n“_
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private GameObject playerobj;
    [SerializeField] private GameObject enemyobj;
    private bool markOnScene = false;
    private GameObject currentMaker;
    Shuttle shuttle;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        if(shuttle == null)
        {
            shuttle = FindObjectOfType<Shuttle>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shuttle.receive && !markOnScene)
        {
            Vector3 receivemarker = new Vector3 (shuttle.randomPos.x, 0f, shuttle.randomPos.z);
            currentMaker = Instantiate(markerPrefab, receivemarker, Quaternion.identity);
            markOnScene = true;
        }
        if (markOnScene)
        {
            //Vector3 point = new Vector3(playerobj.transform.position.x, 0f, playerobj.transform.position.z);
            //currentMaker.transform.position = point;
            if (shuttle.hit)
            {
                Destroy(currentMaker);
                markOnScene= false;
            }
        }
    }
}
