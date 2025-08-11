using UnityEngine;
using UnityEngine.UIElements;

public class GridBuildingSystem : MonoBehaviour
{
    public Grid grid;
    public GameObject BlockToBuild;
    private GameObject PreviewBlock;
    public LayerMask CanCreateLayerMask;

    private Vector3 lastSelectedPosition;
    private void SetPriviewBlock(GameObject gameObject)
    {
      
        PreviewBlock = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        PreviewBlock.GetComponent<BoxCollider>().isTrigger = false;

        foreach (Renderer block in PreviewBlock.GetComponentsInChildren<Renderer>())
        {
            Material m = block.material;
            Color c = Color.green;
            c.a = 0.0f;
            m.color = c;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetPriviewBlock(BlockToBuild);
        }

        RaycastHit hit = TryGetRaycastHit(Input.mousePosition);
        Vector3 selectedPos = hit.point;
        Vector3Int cell = grid.WorldToCell(selectedPos);

        if (PreviewBlock)
        {
            Vector3 CellPos = grid.GetCellCenterWorld(cell);
            Vector3 previewBlockPos = new Vector3(CellPos.x, CellPos.y + (PreviewBlock.transform.localScale.y / 2 - 0.6f), CellPos.z);

            PreviewBlock.transform.position = previewBlockPos;

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 rayStart = PreviewBlock.transform.position + Vector3.up *10f;

                if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hitInfo, 100f))
                {
                    
                    if (hitInfo.collider.gameObject.layer != LayerMask.NameToLayer("CantCreate"))
                    {
                      
                        GameObject go = Instantiate(BlockToBuild, previewBlockPos, Quaternion.identity);
                        go.layer = LayerMask.NameToLayer("CantCreate");
                    }
                    else
                    {
                        Debug.Log("이미 설치된 블록이 있습니다.");
                    }
                }

                Destroy(PreviewBlock);
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
