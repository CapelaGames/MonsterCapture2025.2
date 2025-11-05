using UnityEngine;

public interface ITrappable
{
    public bool isBeingCaptured { get; set; }

    public bool CaptureAnimation(GameObject trap);
    public int PointValue();
}
