using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._03.Member.CDH.Code
{
    public class Shared
    {
        public static TableMgr TableMgr;

        public static TableMgr InitTableMgr()
        {
            if(TableMgr == null)
            {
                TableMgr = new TableMgr();
                TableMgr.Init();
            }

            return TableMgr;
        }
    }
}
