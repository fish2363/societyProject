
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Collections.AllocatorManager;

public class GridBuildingSystem : Singleton<GridBuildingSystem>
{
    [SerializeField] private Grid grid;
    public BuildingData blockToBuild;
    private GameObject previewBlock;
    [SerializeField] private LayerMask CanCreateLayerMask;

    private Vector3 previewBlockPos;
    private Vector3 detectScale;
    Vector3 drawPos;
    Vector3 baseSize;


    public void SetBuilding(BuildingData buildingData)
    {
        blockToBuild = buildingData;
    }
    void Update()
    {


        RaycastHit hit = TryGetRaycastHit(Input.mousePosition);
        Vector3 selectedPos = hit.point;
        Vector3Int cell = grid.WorldToCell(selectedPos);

        if (Input.GetKeyDown(KeyCode.E))
        {
            SetPriviewBlock(blockToBuild.buildingPrefab, cell);
        }

        if (previewBlock)
        {
            Vector3 CellPos = grid.GetCellCenterWorld(cell);
            previewBlockPos = new Vector3(CellPos.x, CellPos.y + (previewBlock.transform.localScale.y / 2 - 0.3f), CellPos.z);


            bool canCreate = CheckCanCreate();
            Color c = canCreate == true ? Color.green : Color.red;
            Renderer[] renderers = previewBlock.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                Material m = renderer.material;
                c.a = 0.4f;
                m.color = c;
            }



            previewBlock.transform.position = previewBlockPos;

            if (Input.GetMouseButtonDown(0))
            {
                if (canCreate)
                {
                    GameObject go = Instantiate(blockToBuild.buildingPrefab, previewBlockPos, Quaternion.identity);
                    go.layer = LayerMask.NameToLayer("CantCreate");
                    go.GetComponentsInChildren<BoxCollider>().ToList().ForEach(x => x.gameObject.layer = LayerMask.NameToLayer("CantCreate"));
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
        float height = previewBlock.GetComponent<BoxCollider>().size.y;
        detectScale = new Vector3(baseSize.x, height, baseSize.z);
        drawPos = previewBlock.transform.position;
        drawPos = new Vector3(drawPos.x, drawPos.y + (detectScale.y / 2), drawPos.z);

        Collider[] colliders = Physics.OverlapBox(drawPos, detectScale / 2);

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
        Gizmos.DrawWireCube(drawPos, detectScale);
    }
    public void SetPriviewBlock(GameObject gameObject, Vector3 currentPos)
    {
        if (previewBlock)
            Destroy(previewBlock);

        previewBlock = Instantiate(gameObject, currentPos, Quaternion.identity);
        previewBlock.GetComponent<BoxCollider>().isTrigger = false;
        BoxCollider boxColider = blockToBuild.basePrefab.GetComponent<BoxCollider>();
        baseSize = Vector3.Scale(boxColider.size, boxColider.transform.lossyScale);


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

