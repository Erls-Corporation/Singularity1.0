// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace App.Model 
{
    public abstract class BaseProcess
	{
        public Cxt Cxt;

        public BaseProcess(Cxt cxt)
        {
            Cxt = cxt;
        }

        public abstract void Start();
    }
}
