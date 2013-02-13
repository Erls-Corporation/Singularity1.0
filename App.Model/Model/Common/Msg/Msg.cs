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
    public class Msg
	{
        public const string AccountCreatedOk = "Your account has been created successfully.";

        public static string ActivationEmailOk(string userName)
        {
            return "We have sent you an account activation email at <b>" + userName + "</b>. If you do not see activation email in your inbox, please check your Spam or Junk folder and add us to your safe sender list.";
        }

        public static string UserNameExists(string userName)
        {
            return "Email <b>" + userName + "</b> is already registered, please use different email address.";
        }

        public static string UserNameNotExists(string userName)
        {
            return "<b>" + userName + "</b> does not exist in our database.";
        }

        public static string NewAccountCreatedOk(string userName)
        {
            return AccountCreatedOk + " " + ActivationEmailOk(userName);
        }
    }
}
