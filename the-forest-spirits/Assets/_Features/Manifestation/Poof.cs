using UnityEngine;

public class Poof : AutoMonoBehaviour
{
    public GameObject objectToCreate;

    private GameObject _creating;
    private Coroutine _dropping;

    public void SpawnObject() {
        _creating = Instantiate(objectToCreate, transform, instantiateInWorldSpace: true) as GameObject;
        if (_creating == null) {
            Debug.LogWarning("Failed to instantiate GameObject!", this);
            return;
        }

        _creating.transform.localPosition = Vector3.forward;

        _creating.transform.SetParent(transform.parent, worldPositionStays: true);
    }

    public void StartMovingDown() {
        if (_creating == null) {
            Debug.LogWarning("No GameObject to move!", this);
            return;
        }

        Transform targetTransform = _creating.transform;

        Bounds? bounds = _creating.GetWorldBounds();
        float offset = bounds.HasValue ? bounds.Value.extents.y : 0f;
        Vector3 ground = FindObjectOfType<Ground>().transform.position;
        Vector3 dest = new Vector3(targetTransform.position.x, ground.y + offset, targetTransform.position.z);
        _dropping = this.AutoLerp(targetTransform.position, dest, 1,
            Utility.EaseIn(Utility.EaseIn<Vector3>(Vector3.Lerp)), value => {
                if (targetTransform != null) {
                    targetTransform.position = value;
                }
            });
    }

    public void OnFinishPoof() {
        this.WaitThen(_dropping, () => { Destroy(gameObject); });
    }
}