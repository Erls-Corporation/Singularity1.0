// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 *  <tr>
          <td align="right">
            <b>Location</b>:
          </td>
          <td>
            <asp:DropDownList ID="cmbCountry" runat="server">
              <asp:ListItem Value="AF">Afghanistan (افغانستان) </asp:ListItem>
              <asp:ListItem Value="AX">Aland Islands </asp:ListItem>
              <asp:ListItem Value="AL">Albania (Shqipëria) </asp:ListItem>
              <asp:ListItem Value="DZ">Algeria (الجزائر) </asp:ListItem>
              <asp:ListItem Value="AS">American Samoa </asp:ListItem>
              <asp:ListItem Value="AD">Andorra </asp:ListItem>
              <asp:ListItem Value="AO">Angola </asp:ListItem>
              <asp:ListItem Value="AI">Anguilla </asp:ListItem>
              <asp:ListItem Value="AQ">Antarctica </asp:ListItem>
              <asp:ListItem Value="AG">Antigua and Barbuda </asp:ListItem>
              <asp:ListItem Value="AR">Argentina </asp:ListItem>
              <asp:ListItem Value="AM">Armenia (Հայաստան) </asp:ListItem>
              <asp:ListItem Value="AW">Aruba </asp:ListItem>
              <asp:ListItem Value="AU">Australia </asp:ListItem>
              <asp:ListItem Value="AT">Austria (Österreich) </asp:ListItem>
              <asp:ListItem Value="AZ">Azerbaijan (Azərbaycan) </asp:ListItem>
              <asp:ListItem Value="BS">Bahamas </asp:ListItem>
              <asp:ListItem Value="BH">Bahrain (البحرين) </asp:ListItem>
              <asp:ListItem Value="BD">Bangladesh </asp:ListItem>
              <asp:ListItem Value="BB">Barbados </asp:ListItem>
              <asp:ListItem Value="BY">Belarus (Белару́сь) </asp:ListItem>
              <asp:ListItem Value="BE">Belgium (België) </asp:ListItem>
              <asp:ListItem Value="BZ">Belize </asp:ListItem>
              <asp:ListItem Value="BJ">Benin (Bénin) </asp:ListItem>
              <asp:ListItem Value="BM">Bermuda </asp:ListItem>
              <asp:ListItem Value="BT">Bhutan</asp:ListItem>
              <asp:ListItem Value="BO">Bolivia </asp:ListItem>
              <asp:ListItem Value="BA">Bosnia and Herzegovina (Bosna i Hercegovina) </asp:ListItem>
              <asp:ListItem Value="BW">Botswana </asp:ListItem>
              <asp:ListItem Value="BV">Bouvet Island </asp:ListItem>
              <asp:ListItem Value="BR">Brazil (Brasil) </asp:ListItem>
              <asp:ListItem Value="IO">British Indian Ocean Territory </asp:ListItem>
              <asp:ListItem Value="BN">Brunei (Brunei Darussalam) </asp:ListItem>
              <asp:ListItem Value="BG">Bulgaria (България) </asp:ListItem>
              <asp:ListItem Value="BF">Burkina Faso </asp:ListItem>
              <asp:ListItem Value="BI">Burundi (Uburundi) </asp:ListItem>
              <asp:ListItem Value="KH">Cambodia (Kampuchea) </asp:ListItem>
              <asp:ListItem Value="CM">Cameroon (Cameroun) </asp:ListItem>
              <asp:ListItem Value="CA">Canada </asp:ListItem>
              <asp:ListItem Value="CV">Cape Verde (Cabo Verde) </asp:ListItem>
              <asp:ListItem Value="KY">Cayman Islands </asp:ListItem>
              <asp:ListItem Value="CF">Central African Republic (République Centrafricaine) </asp:ListItem>
              <asp:ListItem Value="TD">Chad (Tchad) </asp:ListItem>
              <asp:ListItem Value="CL">Chile </asp:ListItem>
              <asp:ListItem Value="CN">China</asp:ListItem>
              <asp:ListItem Value="CX">Christmas Island </asp:ListItem>
              <asp:ListItem Value="CC">Cocos Islands </asp:ListItem>
              <asp:ListItem Value="CO">Colombia </asp:ListItem>
              <asp:ListItem Value="KM">Comoros (Comores) </asp:ListItem>
              <asp:ListItem Value="CG">Congo </asp:ListItem>
              <asp:ListItem Value="CD">Congo, Democratic Republic of the </asp:ListItem>
              <asp:ListItem Value="CK">Cook Islands </asp:ListItem>
              <asp:ListItem Value="CR">Costa Rica </asp:ListItem>
              <asp:ListItem Value="CI">Côte d'Ivoire </asp:ListItem>
              <asp:ListItem Value="HR">Croatia (Hrvatska) </asp:ListItem>
              <asp:ListItem Value="CU">Cuba </asp:ListItem>
              <asp:ListItem Value="CY">Cyprus (Κυπρος) </asp:ListItem>
              <asp:ListItem Value="CZ">Czech Republic (Česko) </asp:ListItem>
              <asp:ListItem Value="DK">Denmark (Danmark) </asp:ListItem>
              <asp:ListItem Value="DJ">Djibouti </asp:ListItem>
              <asp:ListItem Value="DM">Dominica </asp:ListItem>
              <asp:ListItem Value="DO">Dominican Republic </asp:ListItem>
              <asp:ListItem Value="EC">Ecuador </asp:ListItem>
              <asp:ListItem Value="EG">Egypt (مصر) </asp:ListItem>
              <asp:ListItem Value="SV">El Salvador </asp:ListItem>
              <asp:ListItem Value="GQ">Equatorial Guinea (Guinea Ecuatorial) </asp:ListItem>
              <asp:ListItem Value="ER">Eritrea (Ertra) </asp:ListItem>
              <asp:ListItem Value="EE">Estonia (Eesti) </asp:ListItem>
              <asp:ListItem Value="ET">Ethiopia (Ityop'iya) </asp:ListItem>
              <asp:ListItem Value="FK">Falkland Islands </asp:ListItem>
              <asp:ListItem Value="FO">Faroe Islands </asp:ListItem>
              <asp:ListItem Value="FJ">Fiji </asp:ListItem>
              <asp:ListItem Value="FI">Finland (Suomi) </asp:ListItem>
              <asp:ListItem Value="FR">France </asp:ListItem>
              <asp:ListItem Value="GF">French Guiana </asp:ListItem>
              <asp:ListItem Value="PF">French Polynesia </asp:ListItem>
              <asp:ListItem Value="TF">French Southern Territories </asp:ListItem>
              <asp:ListItem Value="GA">Gabon </asp:ListItem>
              <asp:ListItem Value="GM">Gambia </asp:ListItem>
              <asp:ListItem Value="GE">Georgia (საქართველო) </asp:ListItem>
              <asp:ListItem Value="DE">Germany (Deutschland) </asp:ListItem>
              <asp:ListItem Value="GH">Ghana </asp:ListItem>
              <asp:ListItem Value="GI">Gibraltar </asp:ListItem>
              <asp:ListItem Value="GR">Greece (Ελλάς) </asp:ListItem>
              <asp:ListItem Value="GL">Greenland </asp:ListItem>
              <asp:ListItem Value="GD">Grenada </asp:ListItem>
              <asp:ListItem Value="GP">Guadeloupe </asp:ListItem>
              <asp:ListItem Value="GU">Guam </asp:ListItem>
              <asp:ListItem Value="GT">Guatemala </asp:ListItem>
              <asp:ListItem Value="GG">Guernsey </asp:ListItem>
              <asp:ListItem Value="GN">Guinea (Guinée) </asp:ListItem>
              <asp:ListItem Value="GW">Guinea-Bissau (Guiné-Bissau) </asp:ListItem>
              <asp:ListItem Value="GY">Guyana </asp:ListItem>
              <asp:ListItem Value="HT">Haiti (Haïti) </asp:ListItem>
              <asp:ListItem Value="HM">Heard Island and McDonald Islands </asp:ListItem>
              <asp:ListItem Value="HN">Honduras </asp:ListItem>
              <asp:ListItem Value="HK">Hong Kong </asp:ListItem>
              <asp:ListItem Value="HU">Hungary (Magyarország) </asp:ListItem>
              <asp:ListItem Value="IS">Iceland (Ísland) </asp:ListItem>
              <asp:ListItem Value="IN">India </asp:ListItem>
              <asp:ListItem Value="ID">Indonesia </asp:ListItem>
              <asp:ListItem Value="IR">Iran (ایران) </asp:ListItem>
              <asp:ListItem Value="IQ">Iraq (العراق) </asp:ListItem>
              <asp:ListItem Value="IE">Ireland </asp:ListItem>
              <asp:ListItem Value="IM">Isle of Man </asp:ListItem>
              <asp:ListItem Value="IL">Israel (ישראל) </asp:ListItem>
              <asp:ListItem Value="IT">Italy (Italia) </asp:ListItem>
              <asp:ListItem Value="JM">Jamaica </asp:ListItem>
              <asp:ListItem Value="JP">Japan</asp:ListItem>
              <asp:ListItem Value="JE">Jersey </asp:ListItem>
              <asp:ListItem Value="JO">Jordan (الاردن) </asp:ListItem>
              <asp:ListItem Value="KZ">Kazakhstan (Қазақстан) </asp:ListItem>
              <asp:ListItem Value="KE">Kenya </asp:ListItem>
              <asp:ListItem Value="KI">Kiribati </asp:ListItem>
              <asp:ListItem Value="KW">Kuwait (الكويت) </asp:ListItem>
              <asp:ListItem Value="KG">Kyrgyzstan (Кыргызстан) </asp:ListItem>
              <asp:ListItem Value="LA">Laos</asp:ListItem>
              <asp:ListItem Value="LV">Latvia (Latvija) </asp:ListItem>
              <asp:ListItem Value="LB">Lebanon (لبنان) </asp:ListItem>
              <asp:ListItem Value="LS">Lesotho </asp:ListItem>
              <asp:ListItem Value="LR">Liberia </asp:ListItem>
              <asp:ListItem Value="LY">Libya (ليبيا) </asp:ListItem>
              <asp:ListItem Value="LI">Liechtenstein </asp:ListItem>
              <asp:ListItem Value="LT">Lithuania (Lietuva) </asp:ListItem>
              <asp:ListItem Value="LU">Luxembourg (Lëtzebuerg) </asp:ListItem>
              <asp:ListItem Value="MO">Macao </asp:ListItem>
              <asp:ListItem Value="MK">Macedonia (Македонија) </asp:ListItem>
              <asp:ListItem Value="MG">Madagascar (Madagasikara) </asp:ListItem>
              <asp:ListItem Value="MW">Malawi </asp:ListItem>
              <asp:ListItem Value="MY">Malaysia </asp:ListItem>
              <asp:ListItem Value="MV">Maldives</asp:ListItem>
              <asp:ListItem Value="ML">Mali </asp:ListItem>
              <asp:ListItem Value="MT">Malta </asp:ListItem>
              <asp:ListItem Value="MH">Marshall Islands </asp:ListItem>
              <asp:ListItem Value="MQ">Martinique </asp:ListItem>
              <asp:ListItem Value="MR">Mauritania (موريتانيا) </asp:ListItem>
              <asp:ListItem Value="MU">Mauritius </asp:ListItem>
              <asp:ListItem Value="YT">Mayotte </asp:ListItem>
              <asp:ListItem Value="MX">Mexico (México) </asp:ListItem>
              <asp:ListItem Value="FM">Micronesia </asp:ListItem>
              <asp:ListItem Value="MD">Moldova </asp:ListItem>
              <asp:ListItem Value="MC">Monaco </asp:ListItem>
              <asp:ListItem Value="MN">Mongolia (Монгол Улс) </asp:ListItem>
              <asp:ListItem Value="ME">Montenegro (Црна Гора) </asp:ListItem>
              <asp:ListItem Value="MS">Montserrat </asp:ListItem>
              <asp:ListItem Value="MA">Morocco (المغرب) </asp:ListItem>
              <asp:ListItem Value="MZ">Mozambique (Moçambique) </asp:ListItem>
              <asp:ListItem Value="MM">Myanmar (Burma) </asp:ListItem>
              <asp:ListItem Value="NA">Namibia </asp:ListItem>
              <asp:ListItem Value="NR">Nauru (Naoero) </asp:ListItem>
              <asp:ListItem Value="NP">Nepal (नेपाल) </asp:ListItem>
              <asp:ListItem Value="NL">Netherlands (Nederland) </asp:ListItem>
              <asp:ListItem Value="AN">Netherlands Antilles </asp:ListItem>
              <asp:ListItem Value="NC">New Caledonia </asp:ListItem>
              <asp:ListItem Value="NZ">New Zealand </asp:ListItem>
              <asp:ListItem Value="NI">Nicaragua </asp:ListItem>
              <asp:ListItem Value="NE">Niger </asp:ListItem>
              <asp:ListItem Value="NG">Nigeria </asp:ListItem>
              <asp:ListItem Value="NU">Niue </asp:ListItem>
              <asp:ListItem Value="NF">Norfolk Island </asp:ListItem>
              <asp:ListItem Value="MP">Northern Mariana Islands </asp:ListItem>
              <asp:ListItem Value="KP">North Korea</asp:ListItem>
              <asp:ListItem Value="NO">Norway (Norge) </asp:ListItem>
              <asp:ListItem Value="OM">Oman (عمان) </asp:ListItem>
              <asp:ListItem Value="PK" Selected="True">Pakistan (پاکستان) </asp:ListItem>
              <asp:ListItem Value="PW">Palau (Belau) </asp:ListItem>
              <asp:ListItem Value="PS">Palestinian Territories </asp:ListItem>
              <asp:ListItem Value="PA">Panama (Panamá) </asp:ListItem>
              <asp:ListItem Value="PG">Papua New Guinea </asp:ListItem>
              <asp:ListItem Value="PY">Paraguay </asp:ListItem>
              <asp:ListItem Value="PE">Peru (Perú) </asp:ListItem>
              <asp:ListItem Value="PH">Philippines (Pilipinas) </asp:ListItem>
              <asp:ListItem Value="PN">Pitcairn </asp:ListItem>
              <asp:ListItem Value="PL">Poland (Polska) </asp:ListItem>
              <asp:ListItem Value="PT">Portugal </asp:ListItem>
              <asp:ListItem Value="PR">Puerto Rico </asp:ListItem>
              <asp:ListItem Value="QA">Qatar (قطر) </asp:ListItem>
              <asp:ListItem Value="RE">Reunion </asp:ListItem>
              <asp:ListItem Value="RO">Romania (România) </asp:ListItem>
              <asp:ListItem Value="RU">Russia (Россия) </asp:ListItem>
              <asp:ListItem Value="RW">Rwanda </asp:ListItem>
              <asp:ListItem Value="SH">Saint Helena </asp:ListItem>
              <asp:ListItem Value="KN">Saint Kitts and Nevis </asp:ListItem>
              <asp:ListItem Value="LC">Saint Lucia </asp:ListItem>
              <asp:ListItem Value="PM">Saint Pierre and Miquelon </asp:ListItem>
              <asp:ListItem Value="VC">Saint Vincent and the Grenadines </asp:ListItem>
              <asp:ListItem Value="WS">Samoa </asp:ListItem>
              <asp:ListItem Value="SM">San Marino </asp:ListItem>
              <asp:ListItem Value="ST">São Tomé and Príncipe </asp:ListItem>
              <asp:ListItem Value="SA">Saudi Arabia (المملكة العربية السعودية) </asp:ListItem>
              <asp:ListItem Value="SN">Senegal (Sénégal) </asp:ListItem>
              <asp:ListItem Value="RS">Serbia (Србија) </asp:ListItem>
              <asp:ListItem Value="CS">Serbia and Montenegro (Србија и Црна Гора) </asp:ListItem>
              <asp:ListItem Value="SC">Seychelles </asp:ListItem>
              <asp:ListItem Value="SL">Sierra Leone </asp:ListItem>
              <asp:ListItem Value="SG">Singapore (Singapura) </asp:ListItem>
              <asp:ListItem Value="SK">Slovakia (Slovensko) </asp:ListItem>
              <asp:ListItem Value="SI">Slovenia (Slovenija) </asp:ListItem>
              <asp:ListItem Value="SB">Solomon Islands </asp:ListItem>
              <asp:ListItem Value="SO">Somalia (Soomaaliya) </asp:ListItem>
              <asp:ListItem Value="ZA">South Africa </asp:ListItem>
              <asp:ListItem Value="GS">South Georgia and the South Sandwich Islands </asp:ListItem>
              <asp:ListItem Value="KR">South Korea</asp:ListItem>
              <asp:ListItem Value="ES">Spain (España) </asp:ListItem>
              <asp:ListItem Value="LK">Sri Lanka </asp:ListItem>
              <asp:ListItem Value="SD">Sudan (السودان) </asp:ListItem>
              <asp:ListItem Value="SR">Suriname </asp:ListItem>
              <asp:ListItem Value="SJ">Svalbard and Jan Mayen </asp:ListItem>
              <asp:ListItem Value="SZ">Swaziland </asp:ListItem>
              <asp:ListItem Value="SE">Sweden (Sverige) </asp:ListItem>
              <asp:ListItem Value="CH">Switzerland (Schweiz) </asp:ListItem>
              <asp:ListItem Value="SY">Syria (سوريا) </asp:ListItem>
              <asp:ListItem Value="TW">Taiwan</asp:ListItem>
              <asp:ListItem Value="TJ">Tajikistan (Тоикистон) </asp:ListItem>
              <asp:ListItem Value="TZ">Tanzania </asp:ListItem>
              <asp:ListItem Value="TH">Thailand (ราชอาณาจักรไทย) </asp:ListItem>
              <asp:ListItem Value="TL">Timor-Leste </asp:ListItem>
              <asp:ListItem Value="TG">Togo </asp:ListItem>
              <asp:ListItem Value="TK">Tokelau </asp:ListItem>
              <asp:ListItem Value="TO">Tonga </asp:ListItem>
              <asp:ListItem Value="TT">Trinidad and Tobago </asp:ListItem>
              <asp:ListItem Value="TN">Tunisia (تونس) </asp:ListItem>
              <asp:ListItem Value="TR">Turkey (Türkiye) </asp:ListItem>
              <asp:ListItem Value="TM">Turkmenistan (Türkmenistan) </asp:ListItem>
              <asp:ListItem Value="TC">Turks and Caicos Islands </asp:ListItem>
              <asp:ListItem Value="TV">Tuvalu </asp:ListItem>
              <asp:ListItem Value="UG">Uganda </asp:ListItem>
              <asp:ListItem Value="UA">Ukraine (Україна) </asp:ListItem>
              <asp:ListItem Value="AE">United Arab Emirates (الإمارات العربيّة المتّحدة) </asp:ListItem>
              <asp:ListItem Value="GB">United Kingdom </asp:ListItem>
              <asp:ListItem Value="US">United States </asp:ListItem>
              <asp:ListItem Value="UM">United States minor outlying islands </asp:ListItem>
              <asp:ListItem Value="UY">Uruguay </asp:ListItem>
              <asp:ListItem Value="UZ">Uzbekistan (O'zbekiston) </asp:ListItem>
              <asp:ListItem Value="VU">Vanuatu </asp:ListItem>
              <asp:ListItem Value="VA">Vatican City (Città del Vaticano) </asp:ListItem>
              <asp:ListItem Value="VE">Venezuela </asp:ListItem>
              <asp:ListItem Value="VN">Vietnam (Việt Nam) </asp:ListItem>
              <asp:ListItem Value="VG">Virgin Islands, British </asp:ListItem>
              <asp:ListItem Value="VI">Virgin Islands, U.S. </asp:ListItem>
              <asp:ListItem Value="WF">Wallis and Futuna </asp:ListItem>
              <asp:ListItem Value="EH">Western Sahara (الصحراء الغربية) </asp:ListItem>
              <asp:ListItem Value="YE">Yemen (اليمن) </asp:ListItem>
              <asp:ListItem Value="ZM">Zambia </asp:ListItem>
              <asp:ListItem Value="ZW">Zimbabwe </asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbCountry"
              ErrorMessage="Country is required." ToolTip="Country is required.">*</asp:RequiredFieldValidator>
          </td>
        </tr>
*/

namespace App.Model
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RsDropDownList runat=server />")]
    public class RsDropDownList : DropDownList, IRsControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region Properties

        [Category("RafeySoft")]
        public virtual bool ShowDefaultItem
        {
            get { return UWeb.VsBool(ViewState, "ShowDefaultItem", false); }
            set { ViewState["ShowDefaultItem"] = value; }
        }

        [Category("RafeySoft")]
        public virtual bool SelectDefaultItem
        {
            get { return UWeb.VsBool(ViewState, "SelectDefaultItem", false); }
            set { ViewState["SelectDefaultItem"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string DefaultItemText
        {
            get { return UWeb.Vs(ViewState, "DefaultItemText", ""); }
            set { ViewState["DefaultItemText"] = value; }
        }

        [Category("RafeySoft")]
        public virtual string DefaultItemValue
        {
            get { return UWeb.Vs(ViewState, "DefaultItemValue", ""); }
            set { ViewState["DefaultItemValue"] = value; }
        } 
        #endregion

        #region InitControl
        private void InitControl(LayoutE layout, ItemAttribute ia)
        {
            Enabled = layout != LayoutE.View;
            Visible = true;

            ia.Attribute.Kvs.SetProperties(this);

            if (!ia.Attribute.HasValueSource)
            {
                DataValueField = "AttributeID";
                DataTextField = "Name";
                DataSource = ia.Attribute.Children.DataTable;
            }

            DataBind();

            AddDefaultItem();
        }
        #endregion

        #region IRsControl Members

        public virtual ItemAttribute GetValue(ItemAttributes list, ItemAttribute ia)
        {
            ia.Value1 = SelectedValue;

            list.Add(ia);

            return ia;
        }

        public virtual void SetValue(LayoutE layout, ItemAttribute ia)
        {
            InitControl(layout, ia);

            ListItem li = Items.FindByValue(ia.Value1);

            if (li != null)
            {
                li.Selected = true;
            }
        }

        public virtual void GetFilter(SearchQuery q, ItemAttribute ia)
        {
            GetFilter(q, ia, SelectedValue);
        }

        public virtual string GetText(ItemAttribute ia)
        {
            return RsDropDownList.GetTextControl(ia);
        }

        #endregion

        #region Helpers
        private void AddDefaultItem()
        {
            if (ShowDefaultItem)
            {
                Items.Insert(0, new ListItem(DefaultItemText, DefaultItemValue));
            }
        }

        public static void GetFilter(SearchQuery q, ItemAttribute ia, string selectedValue)
        {
            string pid = "";
            string filter = "";

            if (String.IsNullOrEmpty(selectedValue))
            {
                return;
            }

            if (ia.Attribute.HasValueSource)
            {
                RsTextBox.GetFilter(q, ia, selectedValue);
            }
            else
            {
                pid = q.NextParamId;
                filter = UStr.FilterInt32("AttributeID", pid);
                q.SetParam(pid, selectedValue);

                q.AppendFilter(filter + "\n");
            }
        }

        public static string GetTextControl(ItemAttribute ia)
        {
            if (ia.Attribute.HasValueSource)
            {
                DataTable table = ia.Attribute.Kvs.ToTable("DataSource", ia.Attribute.Kvs.ToString("DataValueField"), ia.Value1);

                return new BaseCollection(table).First.GetCol(ia.Attribute.Kvs.ToString("DataTextField"), "");
            }
            else
            {
                return ia.Attribute.Children.GetByID(BaseItem.ToInt32(ia.Value1)).Name;
            }
        }
        #endregion
    }
}
