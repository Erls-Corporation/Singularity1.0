// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace App.Model
{
    public class BaseList : List<BaseItem>
	{
        public BaseList()
        { 
        }

        #region Save
        public virtual void Add(ItemAttributes items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Add(items[i]);
            }
        }

        public virtual void Add(ServiceCategories items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Add(items[i]);
            }
        }

        public virtual void Save()
        {
            Save(Config.ConnectionString);
        }

        public virtual void Save(string connectionString)
        {
            SqlTransaction t = null;

            try
            {
                t = SqlHelper.BeginTransaction(connectionString);

                Save(t);

                SqlHelper.CommitTransaction(t);
            }
            catch (Exception ex)
            {
                SqlHelper.RollbackTransaction(t);

                throw ex;
            }
        }

        public virtual void Save(SqlTransaction t)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Save(t);
            }
        }
       

        #endregion

    }
}
