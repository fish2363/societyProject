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
    private Vector3 lastSelectedPosition;
    private Vector3 detectScale;

    
    public void SetPriviewBlock(GameObject gameObject)
    {
        if(previewBlock)
            Destroy(previewBlock);

        previewBlock = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        previewBlock.GetComponent<BoxCollider>().isTrigger = false;

        foreach (Renderer block in previewBlock.GetComponentsInChildren<Renderer>())
        {
            Material m = block.material;
            Color c = Color.green;
            c.a = 0.0f;
            m.color = c;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(previewBlockPos, detectScale);
    }
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

            previewBlock.transform.position = previewBlockPos;

            if (Input.GetMouseButtonDown(0))
            {
                
                detectScale = previewBlock.transform.localScale;
                detectScale = new Vector3(detectScale.x - 0.4f, detectScale.y - 0.4f,detectScale.z - 0.4f);
                Collider[] colliders = Physics.OverlapBox(previewBlockPos, detectScale/2);
             
                bool canCreate = true;
                foreach (Collider collider in colliders)
                {
                    if(collider.gameObject.layer == LayerMask.NameToLayer("CantCreate"))
                    {
                        canCreate = false;
                    }
                }

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
    
    RaycastHit TryGetRaycastHit(Vector3 mousePos)
    {
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, CanCreateLayerMask);
        return hit;
    }
}
