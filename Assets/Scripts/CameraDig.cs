using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CameraDig : MonoBehaviour
{
    [SerializeField]
    GameObject ringPrefab;
    Camera cam;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.GetComponent<Digger>() != null) {
                    hit.collider.GetComponent<Digger>().DeformMesh(hit.point);
                    GameObject ring = Instantiate(ringPrefab, new Vector3(hit.point.x, hit.point.y, -0.2f), Quaternion.Euler(90, 0, 0));

                    ring.AddComponent<ObservableTriggerTrigger>().OnTriggerEnterAsObservable()
                    .Subscribe(x => {
                        if (x.CompareTag("Ring")) {
                            Destroy(ring);
                        }
                    });
                }
            }
        }
    }
}
