// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace App.Model
{
    public class URegEx
    {
        public const string RxName = @"^[a-zA-Z''-'\s]{1,40}$";
        public const string RxSsn = @"^\d{3}-\d{2}-\d{4}$";
        public const string RxPhoneNumber = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string RxEmail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        public const string RxURL = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";
        public const string RxZIPCode = @"^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$";
        public const string RxPassword = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$";
        public const string RxNonNegInt = @"^\d+$";
        public const string RxCurrencyNonNeg = @"^\d+(\.\d\d)?$";
        public const string RxCurrency = @"^(-)?\d+(\.\d\d)?$";

        public static bool IsMatch(object o, string regex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(o.ToString(),regex);
        }
    }
}
