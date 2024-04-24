using EzySlice;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Material materialSlicedSide;
    public bool isCanSlice;
    public bool isCheckSlice;

    private void Update()
    {
        if (isCheckSlice)
        {
            if (transform.parent.GetSiblingIndex() == 1)
                isCanSlice = true;
            else
                isCanSlice = false;
        }
    }


    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sliceable") && isCanSlice)
        {
            print("Slice");
            isCheckSlice = false;
            isCanSlice = false;
            other.transform.tag = "Untagged";
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject slicedObjectTop = sliceObj?.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject slicedObjectDown = sliceObj?.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);

            SetSlicedObject(slicedObjectTop);
            SetSlicedObject(slicedObjectDown);

            Invoke(nameof(ResetSliceBool), 0.2f);

        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sliceable") && isCanSlice)
        {
            isCheckSlice = false;
            isCanSlice = false;
            other.tag = "Untagged";
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject slicedObjectTop = sliceObj?.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject slicedObjectDown = sliceObj?.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);

            SetSlicedObject(slicedObjectTop);
            SetSlicedObject(slicedObjectDown);

            Invoke(nameof(ResetSliceBool), 0.2f);

        }
    }
    private SlicedHull Slice(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    private void SetSlicedObject(GameObject obj)
    {
        if (obj is not null)
        {
            MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
            meshCollider.material = GameManager.Instance.bounceMat;
            meshCollider.convex = true;
            meshCollider.isTrigger = false;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = false;

            obj.tag = "Sliceable";

            int rnd = Random.Range(0, 2);
            if(rnd==0)
            rb.AddTorque(Vector3.forward * 100);
            else
            rb.AddTorque(-Vector3.forward * 100);
        }
    }

    private void ResetSliceBool()
    {
        isCheckSlice = true;
    }
}
