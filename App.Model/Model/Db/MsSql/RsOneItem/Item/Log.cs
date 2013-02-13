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
    public class Log : BaseItem
	{
        public static string Error = "Error";
        public static string Warning = "Warning";
        public static string Info = "Info";
        public static string System = "System";

        #region Constructor
        public Log()
            : base(0)
        {
        }

        public Log(Cxt cxt, int id)
            : base(cxt, id)
        {
        }

        public Log(Cxt cxt, BaseItem item)
            : base(cxt, item)
        {
        }

        #endregion

        #region Properties

        #region Core

        public override RsOneItemTable TableName
        {
            get { return RsOneItemTable.Log; }
            set { base.TableName = value; }
        }

        #endregion

        #region Table Columns

        public string Type { get { return GetCol("Type"); } set { SetColumn("Type", value); } }
        public string Category { get { return GetCol("Category"); } set { SetColumn("Category", value); } }
        public string Message { get { return GetCol("Message"); } set { SetColumn("Message", value); } }
        #endregion

        #endregion

        #region Methods
        public static void WriteError(Cxt cxt, string message)
        {
            Write(cxt, Log.Error, Log.System, message);
        }

        public static void Write(Cxt cxt, Exception ex)
        {
            Write(cxt, ex, "");
        }

        public static void Write(Cxt cxt, Exception ex, string message)
        {
            if (ex == null)
            {
                ex = new Exception("Exception object was null. Using this dummy as replacement.");
            }

            Write(cxt, Log.Error, Log.System, AppException.GetError(ex, message));
        }

        public static void Write(Cxt cxt, string message)
        {
            Write(cxt, Log.Info, Log.System, message);
        }

        public static void Write(Cxt cxt, string type, string category, string message)
        {
            Log log = new Log(cxt, 0);

            log.Type = type;
            log.Category = category;
            log.Message = message;
            
            log.Save();
        }

        #endregion

        public static DataTable SelectAll()
        {
            return BaseCollection.ExecuteSql("select * from [Log] order by DateCreated desc");
        }

        public static void Clear()
        {
            BaseCollection.ExecuteSql("delete from [Log]");           
        }
    }
}
