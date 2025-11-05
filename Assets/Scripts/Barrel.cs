
using UnityEngine;

public class Barrel : MonoBehaviour, ITrappable
{
    private bool _beingCaptured = false;
    public bool isBeingCaptured { get => _beingCaptured; set => _beingCaptured = value; }

    public bool CaptureAnimation(GameObject trap)
    {
        float shrink = Mathf.Lerp(transform.localScale.x, 0,Time.deltaTime * 2); //Time.time * 20f) * 0.1f + scale;
        transform.localScale = new Vector3(shrink, shrink, shrink);

        transform.position = Vector3.MoveTowards(transform.position, trap.transform.position, 0.003f); 

        GetComponent<Rigidbody>().isKinematic = true;

        if (shrink < 0.05f)
            return false;

        return true;
    }

    public int PointValue()
    {
        return 1;
    }
}
