using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private Grid grid;
    public GameObject blockToBuild;
    private GameObject previewBlock;
    [SerializeField] private LayerMask CanCreateLayerMask;

    private Vector3 previewBlockPos;
    private Vector3 detectScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetPriviewBlock(blockToBuild);
        }

        RaycastHit hit = TryGetRaycastHit(Input.mousePosition);
        Vector3 selectedPos = hit.point;
        Vector3Int cell = grid.WorldToCell(selectedPos);

        if (previewBlock)
        {
            Vector3 CellPos = grid.GetCellCenterWorld(cell);
            previewBlockPos = new Vector3(CellPos.x, CellPos.y + (previewBlock.transform.localScale.y / 2 - 0.6f), CellPos.z);

           
            bool canCreate = CheckCanCreate();
            Color c = canCreate == true ? Color.green : Color.red;
            foreach (Renderer block in previewBlock.GetComponentsInChildren<Renderer>())
            {
                Material m = block.material;
                c.a = 0.0f;
                m.color = c;
            }
            previewBlock.transform.position = previewBlockPos;

            if (Input.GetMouseButtonDown(0))
            {
                if (canCreate)
                {
                    GameObject go = Instantiate(blockToBuild, previewBlockPos, Quaternion.identity);
                    go.layer = LayerMask.NameToLayer("CantCreate");
                }
                else
                {
                    Debug.Log("이미 설치된 블록이 있습니다.");
                }
                Destroy(previewBlock);
            }
        }
    }
    private bool CheckCanCreate()
    {
        detectScale = previewBlock.transform.localScale;
        detectScale = new Vector3(detectScale.x, detectScale.y, detectScale.z);
        Collider[] colliders = Physics.OverlapBox(previewBlockPos, detectScale / 2);

        bool value = true;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("CantCreate"))
            {
                value = false;
            }
        }

        return value;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(previewBlockPos, detectScale);
    }
    public void SetPriviewBlock(GameObject gameObject)
    {
        if (previewBlock)
            Destroy(previewBlock);

        previewBlock = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        previewBlock.GetComponent<BoxCollider>().isTrigger = false;


    }
    RaycastHit TryGetRaycastHit(Vector3 mousePos)
    {
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, CanCreateLayerMask);
        return hit;
    }
}
