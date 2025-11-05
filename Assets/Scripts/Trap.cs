using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Trap : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<ITrappable>(out ITrappable pal))
        {
            if (pal.isBeingCaptured) return;

            ScoreManager.instance.currentScore += pal.PointValue();
            ScoreManager.instance.UpdateGUI();
            StartCoroutine(Capture(pal, other.gameObject));
        }
    }

    IEnumerator Capture(ITrappable pal, GameObject palGO)
    {
        bool isAnimationPlaying = true;
        float scale = gameObject.transform.localScale.x;

        pal.isBeingCaptured = true;

        Vector3 endPosition = transform.position + Vector3.up * 1f;

        while (isAnimationPlaying)
        {
            rb.isKinematic = true; // Freeze physics

            float wave = Mathf.Sin(Time.time * 20f) * 0.1f + scale;
            transform.localScale = new Vector3(wave, wave, wave);
            transform.position = Vector3.MoveTowards(transform.position, endPosition, 0.005f);

            isAnimationPlaying = pal.CaptureAnimation(gameObject);
            yield return null;
        }

        transform.localScale = new Vector3(scale, scale, scale);
        rb.isKinematic = false;
        Destroy(palGO);
    }
}
