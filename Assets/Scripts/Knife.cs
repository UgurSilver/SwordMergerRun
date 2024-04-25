using EzySlice;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Material materialSlicedSide;
    public bool isCanSlice;
    public bool isCheckSlice;
    public int sliceableHp;
    public bool isFirstSword;

    private void Update()
    {
        if (isCheckSlice)
        {
            if (transform.parent.GetSiblingIndex() == 1)
            {
                if (!isFirstSword)
                {
                    isFirstSword = true;
                    Invoke(nameof(SetFirstSword), 0.2f);

                }
            }
            else
                isCanSlice = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sliceable") && isCanSlice)
        {
            isCheckSlice = false;
            isCanSlice = false;
            other.tag = "Untagged";

            Sliceable sliceableSc = other.GetComponent<Sliceable>();
            sliceableHp = sliceableSc.hp;

            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject slicedObjectTop = sliceObj?.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject slicedObjectDown = sliceObj?.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);

            SetSlicedObject(slicedObjectTop);
            SetSlicedObject(slicedObjectDown);

            transform.parent.GetComponent<SwordParentController>().SetHp(sliceableHp);
            Invoke(nameof(ResetSliceBool), 0.5f); //En genis objeyi kestikten sonra can 1 kere azalacak sekilde zamani ayarla
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
            Sliceable sliceableSc = obj.AddComponent<Sliceable>();
            sliceableSc.hp = sliceableHp;
            sliceableSc.SetSliceable();

            MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
            meshCollider.material = GameManager.Instance.bounceMat;
            meshCollider.convex = true;
            meshCollider.isTrigger = false;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = false;

            obj.tag = "Sliceable";

            int rnd = Random.Range(0, 2);
            if (rnd == 0)
                rb.AddTorque(Vector3.forward * 100);
            else
                rb.AddTorque(-Vector3.forward * 100);
        }
    }

    private void SetFirstSword()
    {
        print("First Sword SliceOn");
        isCanSlice = true;
    }

    private void ResetSliceBool()
    {
        isCheckSlice = true;
    }
}
