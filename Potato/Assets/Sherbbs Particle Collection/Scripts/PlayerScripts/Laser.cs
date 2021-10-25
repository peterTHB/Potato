using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    private LineRenderer lineRenderer;
    private List<Vector3> points ;
    public Arrow arrow;
    // Start is called before the first frame update
    void Start()
    {
        points = new List<Vector3>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startPoint.position);

        lineRenderer.SetPosition(1, endPoint.position);
        //UpdateTrajectory();
    }

    public void UpdateTrajectory()
    {
        Vector3 force = arrow.GetForce();
        Vector3 velocity = force * Time.fixedDeltaTime;
        float duration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = duration / 20.0f;
        points.Clear();

        for (int i = 0; i < 20; i++)
        {
            float stepsize = stepTime * i;
            Vector3 movementVec = new Vector3(velocity.x * stepTime, velocity.y * stepTime - 0.5f * Physics.gravity.y * stepTime * stepTime, velocity.z * stepTime);
            points.Add(-movementVec + startPoint.position);
            
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }
}
