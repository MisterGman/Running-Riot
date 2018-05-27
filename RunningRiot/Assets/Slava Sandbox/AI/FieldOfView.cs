using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    [SerializeField]
    private float viewRadius;
    [SerializeField]
    private float viewAngle;

    [SerializeField]
    private float meshResolution;

    [SerializeField]
    private MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    private GameObject player;

    [SerializeField]
    private bool debug;
    [SerializeField]
    private float currAngle;
    private Transform rightRotationObject;

    private bool once;
	// Use this for initialization
	void Start () {
        rightRotationObject = GetComponentInChildren<FuckEulerAngles>().transform;
        player = GameObject.FindGameObjectWithTag("Player");
        viewMesh = new Mesh();
        viewMesh.name = "ViewMesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine(debugFunc());
    }
	IEnumerator debugFunc()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log(currAngle);
        StartCoroutine(debugFunc());
    }
	// Update is called once per frame
	void LateUpdate () {
        DrawFieldOfView();
        FindVisible();
	}
    void FindVisible()
    {
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        float distToTarget = Vector3.Distance(transform.position, player.transform.position);
        Debug.DrawRay(transform.position,rightRotationObject.right*viewRadius, Color.black);
        if (Vector3.Angle(rightRotationObject.right, dirToTarget) < viewAngle/2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position,dirToTarget, out hit, viewRadius))
            {
                if(hit.collider.tag == player.tag && !once)
                {
                    once = true;
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    transform.parent.SendMessage("Alert");
                }
            }
        }
    }
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle/2 + stepAngleSize*i;
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle,true)* viewRadius,Color.red);
            //Debug.Log(angle);
            currAngle = angle;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount-2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle,true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, viewRadius))
        {
            return new ViewCastInfo(true,hit.point,hit.distance,globalAngle);
        }
        return new ViewCastInfo(false, transform.position+dir*viewRadius, viewRadius, globalAngle);
    }
    private struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public void ChangeColorOfMesh(Color color)
    {
        viewMeshFilter.GetComponent<MeshRenderer>().material.color = color;
    }
    public void deleteMesh()
    {
        Destroy(viewMeshFilter.gameObject);
        
    }
}
