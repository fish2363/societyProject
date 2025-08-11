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

        if (PreviewBlock != null)
        {
            Destroy(PreviewBlock);
        }
        PreviewBlock = Instantiate(gameObject, Vector3.zero, Quaternion.identity);

        foreach (Renderer block in PreviewBlock.GetComponentsInChildren<Renderer>())
        {

           Material m = block.material;
            Color c = Color.green;
            c.a = 0.3f;
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
       
        if(PreviewBlock)
        {
            PreviewBlock.transform.position = grid.GetCellCenterWorld(cell);

            if (Input.GetMouseButtonDown(0))
            {

                if (Physics.Raycast(PreviewBlock.transform.position,Vector3.down*10,CanCreateLayerMask))
                {
                    GameObject go = Instantiate(BlockToBuild, PreviewBlock.transform.position, Quaternion.identity);
                    go.layer = LayerMask.NameToLayer("CantCreate");
                }
             
            }
        }
       
    }

    RaycastHit TryGetRaycastHit(Vector3 mousePos)
    {
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray,out hit,100,CanCreateLayerMask);

        return hit;
    }
  
}
