using UnityEngine;

public class TrapThrower : MonoBehaviour
{
    public GameObject trapPrefab;

    public float shootForce = 500f;

    public Camera cam;

    private void Awake()
    {
        if (cam == null) { cam = Camera.main; }
        if (cam == null) { cam = FindFirstObjectByType<Camera>(); }
    }

    void OnAttack()
    {
        Vector3 spawnPosition = transform.position + cam.transform.forward * 0.1f + Vector3.up;
        GameObject trap = Instantiate(trapPrefab, spawnPosition, Quaternion.identity);

        Rigidbody trapRb = trap.GetComponent<Rigidbody>();
        Vector3 shootDirection = cam.transform.forward;
        trapRb.AddForce(shootDirection.normalized * shootForce);
    }


}
