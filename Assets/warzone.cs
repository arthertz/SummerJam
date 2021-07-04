using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warzone : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public List<Vector2> warzoneBoundaries = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(warzoneBoundaries[0], warzoneBoundaries[1]);
        Gizmos.DrawLine(warzoneBoundaries[1], warzoneBoundaries[2]);
        Gizmos.DrawLine(warzoneBoundaries[2], warzoneBoundaries[3]);
        Gizmos.DrawLine(warzoneBoundaries[3], warzoneBoundaries[0]);
    }

    IEnumerator spawnObstacle()
    {
        //Vector2 location = new Vector2(Random.Range(warzoneBoundaries[0].x, warzoneBoundaries[2].x), ) UNDER CONSTRUCTION
        Instantiate(obstaclePrefab);
        yield return new WaitForSeconds(5f);
    }
}
