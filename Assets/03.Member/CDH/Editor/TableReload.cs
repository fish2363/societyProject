using UnityEngine;
using UnityEditor;
using Assets._03.Member.CDH.Code;

public class TableReload : MonoBehaviour
{
    [MenuItem("CS_Util/Table/CSV &F1", false, 1)]
    static public void ParserTableCsv()
    {
        Shared.InitTableMgr();
        Shared.TableMgr.Save();
    }
}
