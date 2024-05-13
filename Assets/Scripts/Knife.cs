using EzySlice;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Material materialSlicedSide;
    public bool isCanSlice;
    public bool isCheckSlice;
    public int sliceableHp;
    public Color sliceFxColor;
    public bool isFirstSword;
    private bool isWood;
    private WoodType woodType;
    private int sliceForce;

    private void Update()
    {
        if (isCheckSlice)
        {
            if (transform.parent.GetSiblingIndex() == 1)
            {
                if (!isFirstSword)
                {
                    isFirstSword = true;
                    Invoke(nameof(SetFirstSword), 0.1f);

                }
            }
            else
                isCanSlice = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sliceable") && isCanSlice && !GetComponent<SwordController>().isdead)
        {
            isCheckSlice = false;
            isCanSlice = false;
            other.tag = "Untagged";

            Sliceable sliceableSc = other.GetComponent<Sliceable>();
            sliceableHp = sliceableSc.hp;
            sliceFxColor = sliceableSc.sliceFxColor;
            materialSlicedSide = sliceableSc.insideMat;
            isWood = sliceableSc.isWood;
            woodType = sliceableSc.woodType;
            if (sliceableSc.isWood)
            {
                if (!GameManager.Instance.isBonus)
                {
                    if (sliceableSc.GetComponent<WoodController>()?.bonusFx != null)
                    {
                        sliceableSc.GetComponent<WoodController>().bonusFx.SetActive(false);
                    }
                }
                if (sliceableSc.transform.childCount > 0)
                    sliceableSc.transform.GetChild(0)?.SetParent(null);
                if (sliceableSc.transform.childCount > 0)
                    sliceableSc.transform.GetChild(0)?.SetParent(null);
                PlayerManager.Instance.PlayWoodSound();
            }
            else
                PlayerManager.Instance.PlayFruitSound();

            GameManager.Instance.UseSliceFx(other.transform.position, sliceFxColor);

            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject slicedObjectTop = sliceObj?.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject slicedObjectDown = sliceObj?.CreateLowerHull(other.gameObject, materialSlicedSide);
            if (GameManager.Instance.isFire)
            {
                GameManager.Instance.UseSmokeFx(slicedObjectTop?.transform);
                GameManager.Instance.UseSmokeFx(slicedObjectDown?.transform);
            }
            Destroy(other.gameObject);

            SetSlicedObject(slicedObjectTop);
            SetSlicedObject(slicedObjectDown);

            transform.parent.GetComponent<SwordParentController>().SetHp(sliceableHp);
            Invoke(nameof(ResetSliceBool), 0.1f); //En genis objeyi kestikten sonra can 1 kere azalacak sekilde zamani ayarla
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
            sliceableSc.insideMat = materialSlicedSide;
            sliceableSc.sliceFxColor = sliceFxColor;
            sliceableSc.isWood = isWood;
            sliceableSc.woodType = woodType;
            sliceableSc.SetSliceable();
            sliceableSc.CloseObject();

            MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
            meshCollider.material = GameManager.Instance.bounceMat;
            meshCollider.convex = true;
            meshCollider.isTrigger = false;

            Rigidbody rb = obj.AddComponent<Rigidbody>();
            rb.isKinematic = false;

            obj.tag = "Sliceable";

            int rnd = Random.Range(0, 2);

            if (sliceableSc.isWood)
                sliceForce = 0;
            else
                sliceForce = 100;

            if (rnd == 0)
                rb.AddTorque(Vector3.forward * sliceForce);
            else
                rb.AddTorque(-Vector3.forward * sliceForce);
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
        if (transform.parent.GetSiblingIndex() == 1)
            isCanSlice = true;
    }
}
