using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class GradiantImage : Image
{
    [SerializeField] private Color leftColor = new Color(0.9f, 1f, 1f); // E9FFFF
    [SerializeField] private Color rightColor = Color.white;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        Rect rect = rectTransform.rect;
        UIVertex vert = UIVertex.simpleVert;

        Vector3 bl = new Vector3(rect.xMin, rect.yMin); // bottom-left
        Vector3 tl = new Vector3(rect.xMin, rect.yMax); // top-left
        Vector3 tr = new Vector3(rect.xMax, rect.yMax); // top-right
        Vector3 br = new Vector3(rect.xMax, rect.yMin); // bottom-right

        // Image.color 곱해서 Inspector 색 적용
        Color finalLeft = leftColor * color;
        Color finalRight = rightColor * color;

        vert.position = bl; vert.color = finalLeft; vh.AddVert(vert);
        vert.position = tl; vert.color = finalLeft; vh.AddVert(vert);
        vert.position = tr; vert.color = finalRight; vh.AddVert(vert);
        vert.position = br; vert.color = finalRight; vh.AddVert(vert);

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 0);
    }
}
