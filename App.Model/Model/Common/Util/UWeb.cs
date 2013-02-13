// (c) Copyright RafeySoft, Karachi Pakistan. http://rafeysoft.com
// This source is subject to following License:
// http://rafeysoft.com/Web/Page/ItemEdit.aspx?sid=29&lid=4&cid=23&iid=2143
// Please write to info@rafeysoft.com for more information
// All other rights reserved.

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace App.Model
{
    #region MIME Types
    //MIME Types By Content Type
    //Type/sub-type 	Extension
    //application/envoy 	evy
    //application/fractals 	fif
    //application/futuresplash 	spl
    //application/hta 	hta
    //application/internet-property-stream 	acx
    //application/mac-binhex40 	hqx
    //application/msword 	doc
    //application/msword 	dot
    //application/octet-stream 	*
    //application/octet-stream 	bin
    //application/octet-stream 	class
    //application/octet-stream 	dms
    //application/octet-stream 	exe
    //application/octet-stream 	lha
    //application/octet-stream 	lzh
    //application/oda 	oda
    //application/olescript 	axs
    //application/pdf 	pdf
    //application/pics-rules 	prf
    //application/pkcs10 	p10
    //application/pkix-crl 	crl
    //application/postscript 	ai
    //application/postscript 	eps
    //application/postscript 	ps
    //application/rtf 	rtf
    //application/set-payment-initiation 	setpay
    //application/set-registration-initiation 	setreg
    //application/vnd.ms-excel 	xla
    //application/vnd.ms-excel 	xlc
    //application/vnd.ms-excel 	xlm
    //application/vnd.ms-excel 	xls
    //application/vnd.ms-excel 	xlt
    //application/vnd.ms-excel 	xlw
    //application/vnd.ms-outlook 	msg
    //application/vnd.ms-pkicertstore 	sst
    //application/vnd.ms-pkiseccat 	cat
    //application/vnd.ms-pkistl 	stl
    //application/vnd.ms-powerpoint 	pot
    //application/vnd.ms-powerpoint 	pps
    //application/vnd.ms-powerpoint 	ppt
    //application/vnd.ms-project 	mpp
    //application/vnd.ms-works 	wcm
    //application/vnd.ms-works 	wdb
    //application/vnd.ms-works 	wks
    //application/vnd.ms-works 	wps
    //application/winhlp 	hlp
    //application/x-bcpio 	bcpio
    //application/x-cdf 	cdf
    //application/x-compress 	z
    //application/x-compressed 	tgz
    //application/x-cpio 	cpio
    //application/x-csh 	csh
    //application/x-director 	dcr
    //application/x-director 	dir
    //application/x-director 	dxr
    //application/x-dvi 	dvi
    //application/x-gtar 	gtar
    //application/x-gzip 	gz
    //application/x-hdf 	hdf
    //application/x-internet-signup 	ins
    //application/x-internet-signup 	isp
    //application/x-iphone 	iii
    //application/x-javascript 	js
    //application/x-latex 	latex
    //application/x-msaccess 	mdb
    //application/x-mscardfile 	crd
    //application/x-msclip 	clp
    //application/x-msdownload 	dll
    //application/x-msmediaview 	m13
    //application/x-msmediaview 	m14
    //application/x-msmediaview 	mvb
    //application/x-msmetafile 	wmf
    //application/x-msmoney 	mny
    //application/x-mspublisher 	pub
    //application/x-msschedule 	scd
    //application/x-msterminal 	trm
    //application/x-mswrite 	wri
    //application/x-netcdf 	cdf
    //application/x-netcdf 	nc
    //application/x-perfmon 	pma
    //application/x-perfmon 	pmc
    //application/x-perfmon 	pml
    //application/x-perfmon 	pmr
    //application/x-perfmon 	pmw
    //application/x-pkcs12 	p12
    //application/x-pkcs12 	pfx
    //application/x-pkcs7-certificates 	p7b
    //application/x-pkcs7-certificates 	spc
    //application/x-pkcs7-certreqresp 	p7r
    //application/x-pkcs7-mime 	p7c
    //application/x-pkcs7-mime 	p7m
    //application/x-pkcs7-signature 	p7s
    //application/x-sh 	sh
    //application/x-shar 	shar
    //application/x-shockwave-flash 	swf
    //application/x-stuffit 	sit
    //application/x-sv4cpio 	sv4cpio
    //application/x-sv4crc 	sv4crc
    //application/x-tar 	tar
    //application/x-tcl 	tcl
    //application/x-tex 	tex
    //application/x-texinfo 	texi
    //application/x-texinfo 	texinfo
    //application/x-troff 	roff
    //application/x-troff 	t
    //application/x-troff 	tr
    //application/x-troff-man 	man
    //application/x-troff-me 	me
    //application/x-troff-ms 	ms
    //application/x-ustar 	ustar
    //application/x-wais-source 	src
    //application/x-x509-ca-cert 	cer
    //application/x-x509-ca-cert 	crt
    //application/x-x509-ca-cert 	der
    //application/ynd.ms-pkipko 	pko
    //application/zip 	zip
    //audio/basic 	au
    //audio/basic 	snd
    //audio/mid 	mid
    //audio/mid 	rmi
    //audio/mpeg 	mp3
    //audio/x-aiff 	aif
    //audio/x-aiff 	aifc
    //audio/x-aiff 	aiff
    //audio/x-mpegurl 	m3u
    //audio/x-pn-realaudio 	ra
    //audio/x-pn-realaudio 	ram
    //audio/x-wav 	wav
    //image/bmp 	bmp
    //image/cis-cod 	cod
    //image/gif 	gif
    //image/ief 	ief
    //image/jpeg 	jpe
    //image/jpeg 	jpeg
    //image/jpeg 	jpg
    //image/pipeg 	jfif
    //image/svg+xml 	svg
    //image/tiff 	tif
    //image/tiff 	tiff
    //image/x-cmu-raster 	ras
    //image/x-cmx 	cmx
    //image/x-icon 	ico
    //image/x-portable-anymap 	pnm
    //image/x-portable-bitmap 	pbm
    //image/x-portable-graymap 	pgm
    //image/x-portable-pixmap 	ppm
    //image/x-rgb 	rgb
    //image/x-xbitmap 	xbm
    //image/x-xpixmap 	xpm
    //image/x-xwindowdump 	xwd
    //message/rfc822 	mht
    //message/rfc822 	mhtml
    //message/rfc822 	nws
    //text/css 	css
    //text/h323 	323
    //text/html 	htm
    //text/html 	html
    //text/html 	stm
    //text/iuls 	uls
    //text/plain 	bas
    //text/plain 	c
    //text/plain 	h
    //text/plain 	txt
    //text/richtext 	rtx
    //text/scriptlet 	sct
    //text/tab-separated-values 	tsv
    //text/webviewhtml 	htt
    //text/x-component 	htc
    //text/x-setext 	etx
    //text/x-vcard 	vcf
    //video/mpeg 	mp2
    //video/mpeg 	mpa
    //video/mpeg 	mpe
    //video/mpeg 	mpeg
    //video/mpeg 	mpg
    //video/mpeg 	mpv2
    //video/quicktime 	mov
    //video/quicktime 	qt
    //video/x-la-asf 	lsf
    //video/x-la-asf 	lsx
    //video/x-ms-asf 	asf
    //video/x-ms-asf 	asr
    //video/x-ms-asf 	asx
    //video/x-msvideo 	avi
    //video/x-sgi-movie 	movie
    //x-world/x-vrml 	flr
    //x-world/x-vrml 	vrml
    //x-world/x-vrml 	wrl
    //x-world/x-vrml 	wrz
    //x-world/x-vrml 	xaf
    //x-world/x-vrml 	xof

    //Mime Types By File Extension
    //Extension 	Type/sub-type
    //    application/octet-stream
    //323 	text/h323
    //acx 	application/internet-property-stream
    //ai 	application/postscript
    //aif 	audio/x-aiff
    //aifc 	audio/x-aiff
    //aiff 	audio/x-aiff
    //asf 	video/x-ms-asf
    //asr 	video/x-ms-asf
    //asx 	video/x-ms-asf
    //au 	audio/basic
    //avi 	video/x-msvideo
    //axs 	application/olescript
    //bas 	text/plain
    //bcpio 	application/x-bcpio
    //bin 	application/octet-stream
    //bmp 	image/bmp
    //c 	text/plain
    //cat 	application/vnd.ms-pkiseccat
    //cdf 	application/x-cdf
    //cer 	application/x-x509-ca-cert
    //class 	application/octet-stream
    //clp 	application/x-msclip
    //cmx 	image/x-cmx
    //cod 	image/cis-cod
    //cpio 	application/x-cpio
    //crd 	application/x-mscardfile
    //crl 	application/pkix-crl
    //crt 	application/x-x509-ca-cert
    //csh 	application/x-csh
    //css 	text/css
    //dcr 	application/x-director
    //der 	application/x-x509-ca-cert
    //dir 	application/x-director
    //dll 	application/x-msdownload
    //dms 	application/octet-stream
    //doc 	application/msword
    //dot 	application/msword
    //dvi 	application/x-dvi
    //dxr 	application/x-director
    //eps 	application/postscript
    //etx 	text/x-setext
    //evy 	application/envoy
    //exe 	application/octet-stream
    //fif 	application/fractals
    //flr 	x-world/x-vrml
    //gif 	image/gif
    //gtar 	application/x-gtar
    //gz 	application/x-gzip
    //h 	text/plain
    //hdf 	application/x-hdf
    //hlp 	application/winhlp
    //hqx 	application/mac-binhex40
    //hta 	application/hta
    //htc 	text/x-component
    //htm 	text/html
    //html 	text/html
    //htt 	text/webviewhtml
    //ico 	image/x-icon
    //ief 	image/ief
    //iii 	application/x-iphone
    //ins 	application/x-internet-signup
    //isp 	application/x-internet-signup
    //jfif 	image/pipeg
    //jpe 	image/jpeg
    //jpeg 	image/jpeg
    //jpg 	image/jpeg
    //js 	application/x-javascript
    //latex 	application/x-latex
    //lha 	application/octet-stream
    //lsf 	video/x-la-asf
    //lsx 	video/x-la-asf
    //lzh 	application/octet-stream
    //m13 	application/x-msmediaview
    //m14 	application/x-msmediaview
    //m3u 	audio/x-mpegurl
    //man 	application/x-troff-man
    //mdb 	application/x-msaccess
    //me 	application/x-troff-me
    //mht 	message/rfc822
    //mhtml 	message/rfc822
    //mid 	audio/mid
    //mny 	application/x-msmoney
    //mov 	video/quicktime
    //movie 	video/x-sgi-movie
    //mp2 	video/mpeg
    //mp3 	audio/mpeg
    //mpa 	video/mpeg
    //mpe 	video/mpeg
    //mpeg 	video/mpeg
    //mpg 	video/mpeg
    //mpp 	application/vnd.ms-project
    //mpv2 	video/mpeg
    //ms 	application/x-troff-ms
    //mvb 	application/x-msmediaview
    //nws 	message/rfc822
    //oda 	application/oda
    //p10 	application/pkcs10
    //p12 	application/x-pkcs12
    //p7b 	application/x-pkcs7-certificates
    //p7c 	application/x-pkcs7-mime
    //p7m 	application/x-pkcs7-mime
    //p7r 	application/x-pkcs7-certreqresp
    //p7s 	application/x-pkcs7-signature
    //pbm 	image/x-portable-bitmap
    //pdf 	application/pdf
    //pfx 	application/x-pkcs12
    //pgm 	image/x-portable-graymap
    //pko 	application/ynd.ms-pkipko
    //pma 	application/x-perfmon
    //pmc 	application/x-perfmon
    //pml 	application/x-perfmon
    //pmr 	application/x-perfmon
    //pmw 	application/x-perfmon
    //pnm 	image/x-portable-anymap
    //pot, 	application/vnd.ms-powerpoint
    //ppm 	image/x-portable-pixmap
    //pps 	application/vnd.ms-powerpoint
    //ppt 	application/vnd.ms-powerpoint
    //prf 	application/pics-rules
    //ps 	application/postscript
    //pub 	application/x-mspublisher
    //qt 	video/quicktime
    //ra 	audio/x-pn-realaudio
    //ram 	audio/x-pn-realaudio
    //ras 	image/x-cmu-raster
    //rgb 	image/x-rgb
    //rmi 	audio/mid
    //roff 	application/x-troff
    //rtf 	application/rtf
    //rtx 	text/richtext
    //scd 	application/x-msschedule
    //sct 	text/scriptlet
    //setpay 	application/set-payment-initiation
    //setreg 	application/set-registration-initiation
    //sh 	application/x-sh
    //shar 	application/x-shar
    //sit 	application/x-stuffit
    //snd 	audio/basic
    //spc 	application/x-pkcs7-certificates
    //spl 	application/futuresplash
    //src 	application/x-wais-source
    //sst 	application/vnd.ms-pkicertstore
    //stl 	application/vnd.ms-pkistl
    //stm 	text/html
    //svg 	image/svg+xml
    //sv4cpio 	application/x-sv4cpio
    //sv4crc 	application/x-sv4crc
    //swf 	application/x-shockwave-flash
    //t 	application/x-troff
    //tar 	application/x-tar
    //tcl 	application/x-tcl
    //tex 	application/x-tex
    //texi 	application/x-texinfo
    //texinfo 	application/x-texinfo
    //tgz 	application/x-compressed
    //tif 	image/tiff
    //tiff 	image/tiff
    //tr 	application/x-troff
    //trm 	application/x-msterminal
    //tsv 	text/tab-separated-values
    //txt 	text/plain
    //uls 	text/iuls
    //ustar 	application/x-ustar
    //vcf 	text/x-vcard
    //vrml 	x-world/x-vrml
    //wav 	audio/x-wav
    //wcm 	application/vnd.ms-works
    //wdb 	application/vnd.ms-works
    //wks 	application/vnd.ms-works
    //wmf 	application/x-msmetafile
    //wps 	application/vnd.ms-works
    //wri 	application/x-mswrite
    //wrl 	x-world/x-vrml
    //wrz 	x-world/x-vrml
    //xaf 	x-world/x-vrml
    //xbm 	image/x-xbitmap
    //xla 	application/vnd.ms-excel
    //xlc 	application/vnd.ms-excel
    //xlm 	application/vnd.ms-excel
    //xls 	application/vnd.ms-excel
    //xlt 	application/vnd.ms-excel
    //xlw 	application/vnd.ms-excel
    //xof 	x-world/x-vrml
    //xpm 	image/x-xpixmap
    //xwd 	image/x-xwindowdump
    //z 	application/x-compress
    //zip 	application/zip
    #endregion

    #region enum SearchTypeE

    public enum AccountRequestTypeE
    {
        Unknown = 0,
        ActivateEmail = 1,
        ActivateAccount = 2
    } 
    #endregion

    #region Data Members
    public delegate void SearchClickEventHandler(object sender, SearchQuery q);
    #endregion

    //[System.Diagnostics.DebuggerStepThrough]
    public class UWeb
    {
        #region MapPath
        public static string MapPath(string url)
        {
            try
            {
                return HttpContext.Current.Server.MapPath(url);
            }
            catch
            {
                return "";
            }
        }

        public static bool UrlExists(string url)
        {
            return UFile.Exists(UWeb.MapPath(url));
        } 

        #endregion

        #region Qv
        public static int QvInt32(StateBag v, string key)
        {
            return QvInt32(v, key, 0);
        }

        public static int QvInt32(StateBag v, string key, int defaultValue)
        {
            if (v[key] == null)
            {
                return QsInt32(key, defaultValue);
            }

            return VsInt32(v, key, defaultValue);
        }

        public static string Qv(StateBag v, string key)
        {
            return Qv(v, key, "");
        }

        public static string Qv(StateBag v, string key, string defaultValue)
        {
            if (v[key] == null)
            {
                return Qs(key, defaultValue).ToString();
            }

            return Vs(v, key, defaultValue);
        }

        #endregion

        #region Session
        public static int SInt32(string name)
        {
            return SInt32(name, 0);
        }

        public static int SInt32(string name, int defaultValue)
        {
            return BaseItem.ToInt32(UWeb.GetS(name, defaultValue));
        }

        public static string S(string name)
        {
            return UWeb.GetS(name, "").ToString();
        }

        public static object GetS(string name, object defaultValue)
        {
            if (HttpContext.Current.Session[name] == null)
            {
                return defaultValue;
            }

            return HttpContext.Current.Session[name];
        }

        #endregion

        #region QueryString
        #region Qs
        public static int QsInt32(string name)
        {
            return QsInt32(name, 0);
        }

        public static int QsInt32(string name, int defaultValue)
        {
            return BaseItem.ToInt32(UWeb.Qs(name, defaultValue));
        }

        public static string Qs(string name)
        {
            return UWeb.Qs(name, "").ToString();
        }

        public static object Qs(string name, object defaultValue)
        {
            if (HttpContext.Current == null)
            {
                return defaultValue;
            }
            
            if (HttpContext.Current.Request.QueryString[name] == null)
            {
                return defaultValue;
            }

            return HttpContext.Current.Request.QueryString[name];
        } 
        #endregion

        #region Parse
        public static NameValueCollection ParseQs(string url)
        {
            Uri uri = new Uri(url);

            return HttpUtility.ParseQueryString(uri.Query);
        }
        
        #endregion
        #endregion
        
        #region Rf
        public static int RfInt32(string name)
        {
            return RfInt32(name, 0);
        }

        public static int RfInt32(string name, int defaultValue)
        {
            return BaseItem.ToInt32(UWeb.GetRf(name, defaultValue));
        }

        public static string Rf(string name)
        {
            return UWeb.GetRf(name, "").ToString();
        }

        public static object GetRf(string name, object defaultValue)
        {
            if (HttpContext.Current == null)
            {
                return defaultValue;
            }

            if (HttpContext.Current.Request.Form[name] == null)
            {
                return defaultValue;
            }

            return HttpContext.Current.Request.Form[name];
        }
        #endregion

        #region ViewState

        #region Core
        public static void SetVs(StateBag v, string key, object value)
        {
            v[key] = value;
        }

        public static string Vs(StateBag v, string key)
        {
            return Vs(v, key, "");
        }

        public static string Vs(StateBag v, string key, string defaultValue)
        {
            return GetVs(v, key, defaultValue).ToString();
        }

        public static object GetVs(StateBag v, string key, object defaultValue)
        {
            if (v[key] == null)
            {
                return defaultValue;
            }

            return v[key];
        }
        
        #endregion

        #region Int32

        public static int VsInt32(StateBag v, string key)
        {
            return VsInt32(v, key, 0);
        }

        public static int VsInt32(StateBag v, string key, int defaultValue)
        {
            return BaseItem.ToInt32(GetVs(v, key, defaultValue).ToString());
        }
        #endregion

        #region Bool

        public static bool VsBool(StateBag v, string key)
        {
            return VsBool(v, key, false);
        }

        public static bool VsBool(StateBag v, string key, bool defaultValue)
        {
            return BaseItem.ToBool(GetVs(v, key, defaultValue).ToString());
        }
        #endregion

        #region DataTable

        public static DataTable VsTable(StateBag v, string key)
        {
            return VsTable(v, key, null);
        }

        public static DataTable VsTable(StateBag v, string key, DataTable defaultValue)
        {
            return (DataTable) GetVs(v, key, defaultValue);
        }
        #endregion

        public static V Vs<V>(StateBag v, string key, V defaultValue)
        {
            if (v[key] == null)
            {
                return defaultValue;
            }

            return (V)v[key];
        }

        #endregion

        #region Identity
        public static System.Security.Principal.IPrincipal Principal
        {
            get { return HttpContext.Current.User; }
        }

        public static System.Security.Principal.IIdentity Identity
        {
            get { return UWeb.Principal.Identity; }
        }

        public static string UserName
        {
            get { return UWeb.Identity.Name == "" ? "Anonymous" : UWeb.Identity.Name; }
        }

        public static int UserID
        {
            get { return HttpContext.Current == null ? 1 : User.GetUser(null, UWeb.UserName).ID; }
        }

        public static bool IsAuthenticated
        {
            get { return UWeb.Identity.IsAuthenticated; }
        }
        #endregion

        #region GetUrl

        public static string GetUrlServiceSearch(object sid, SearchTypeE st)
        {
            return UWeb.GetUrl("~/Web/Page/Default.aspx", "sid", sid, "st", st.ToString("d"));
        }

        public static string GetUrlItemSearch(string searchText)
        {
            return UWeb.GetUrl("~/Web/Page/ItemSearch.aspx", "q", searchText);
        }

        public static string GetUrlItemSearch(object sid, object cid, SearchTypeE st)
        {
            return UWeb.GetUrl("~/Web/Page/ItemSearch.aspx", "sid", sid, "cid", cid, "st", st.ToString("d"));
        }

        public static string GetUrlItemEdit(object sid, LayoutE lid, object cid, object iid, RsOneItemTable tableName)
        {
            return UWeb.GetUrl("~/Web/Page/ItemEdit.aspx", "sid", sid, "lid", lid.ToString("d"), "cid", cid, "iid", iid, "tn", tableName.ToString("d"));
        }

        public static string GetUrlItemEdit(object sid, LayoutE lid, object cid, object iid)
        {
            return UWeb.GetUrl("~/Web/Page/ItemEdit.aspx", "sid", sid, "lid", lid.ToString("d"), "cid", cid, "iid", iid);
        }

        public static string GetUrl(string url, params object[] pv)
        {
            if (pv == null || pv.Length == 0)
            {
                return url;
            }

            if (pv.Length % 2 != 0)
            {
                throw new Exception("Length of parameter-value collection should be an even number. Incorrect parameter-value collection [" + UStr.GetString(pv) + "]");
            }

            bool hasParam = url.Contains("?");

            for (int i = 0; i < pv.Length; i += 2)
            {
                if (pv.GetValue(i) != null)
                {
                    string px = pv.GetValue(i).ToString() + "=";

                    if (pv.GetValue(i + 1) == null)
                    {
                        px += "";
                    }
                    else
                    {
                        px += pv.GetValue(i + 1).ToString();
                    }

                    if (hasParam)
                    {
                        url += "&" + px;
                    }
                    else
                    {
                        hasParam = true;
                        url += "?" + px;
                    }
                }
            }

            return url;
        }

        public static string GetUrlActivationEmail(string userName)
        {
            return UWeb.GetUrl("http://RafeySoft.com/Web/Page/Account/Register.aspx", "rt", AccountRequestTypeE.ActivateEmail.ToString("d"), "rid", UCrypto.ToBase64(userName));
        }

        public static string GetUrlActivateAccount(string userName)
        {
            return UWeb.GetUrl("http://RafeySoft.com/Web/Page/Account/Register.aspx", "rt", AccountRequestTypeE.ActivateAccount.ToString("d"), "rid", UCrypto.ToBase64(userName));
        } 
        #endregion

        #region Redirect
        public static void Redirect(string url, params object[] pv)
        {
            HttpContext.Current.Response.Redirect(GetUrl(url, pv));
        } 
        #endregion

        #region Exists
        public static bool Exists(string url)
        {
            return (File.Exists(UWeb.MapPath(url)));
        }

        public static string GetFileName(string url)
        {
            return UFile.GetFileName(UWeb.MapPath(url));
        }

        public static string GetFileNameNoExtension(string url)
        {
            return UFile.GetFileNameNoExtension(UWeb.MapPath(url));
        }

        public static string GetExtension(string url)
        {
            return UFile.GetExtension(UWeb.MapPath(url));
        }
        #endregion

        #region GetPostBackControl
        public static Control GetPostBackControl(Page page)
        {
            Control postbackControlInstance = null;

            string postbackControlName = page.Request.Params.Get("__EVENTTARGET");

            if (postbackControlName != null && postbackControlName != string.Empty)
            {
                postbackControlInstance = page.FindControl(postbackControlName);
            }
            else
            {
                // handle the Button control postbacks
                for (int i = 0; i < page.Request.Form.Keys.Count; i++)
                {
                    postbackControlInstance = page.FindControl(page.Request.Form.Keys[i]);

                    if (postbackControlInstance is System.Web.UI.WebControls.Button)
                    {
                        return postbackControlInstance;
                    }
                }
            }

            // handle the ImageButton postbacks
            if (postbackControlInstance == null)
            {
                for (int i = 0; i < page.Request.Form.Count; i++)
                {
                    if ((page.Request.Form.Keys[i].EndsWith(".x")) || (page.Request.Form.Keys[i].EndsWith(".y")))
                    {
                        postbackControlInstance = page.FindControl(page.Request.Form.Keys[i].Substring(0, page.Request.Form.Keys[i].Length - 2));

                        return postbackControlInstance;
                    }
                }
            }

            return postbackControlInstance;
        }    
        #endregion

        #region GridView

        public static string ToString(System.Web.UI.WebControls.GridViewRowEventArgs e, string columnName)
        {
            return ((System.Data.DataRowView)(e.Row.DataItem)).Row[columnName].ToString();
        }

        public static int ToInt32(System.Web.UI.WebControls.GridViewRowEventArgs e, string columnName)
        {
            return BaseItem.ToInt32(ToString(e, columnName));
        }

        public static bool ToBool(System.Web.UI.WebControls.GridViewRowEventArgs e, string columnName)
        {
            return BaseItem.ToBool(ToString(e, columnName));
        }
        #endregion

        #region DataList

        public static string ToString(System.Web.UI.WebControls.DataListItemEventArgs e, string columnName)
        {
            return ((System.Data.DataRowView)(e.Item.DataItem)).Row[columnName].ToString();
        }

        public static int ToInt32(System.Web.UI.WebControls.DataListItemEventArgs e, string columnName)
        {
            return BaseItem.ToInt32(ToString(e, columnName));
        } 
        #endregion

        #region Server Control
        #region Res
        public static string ResNameCtrl(Control c, string resourceName)
        {
            return ResNameCtrl(c.GetType(), resourceName);
        }

        public static string ResNameCtrl(Type t, string resourceName)
        {
            return "App.Model.Model.Ui.Web.Controls." + t.Name + ".Res." + resourceName;
        }

        public static string ResUrl(Control c, string resourceName)
        {
            return c.Page.ClientScript.GetWebResourceUrl(c.GetType(), ResNameCtrl(c, resourceName));
        }

        public static string ResText(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(resourceName))
            {
                StreamReader sr = new StreamReader(s);

                return sr.ReadToEnd();
            }
        }

        public static void ResJs(Control c, string resourceName)
        {
            c.Page.ClientScript.RegisterClientScriptInclude(c.GetType(), resourceName, ResUrl(c, resourceName));
        }
        #endregion

        #region GetUniqueName
        public static string GetUniqueName(Control c, string baseName)
        {
            return c.ID + "_" + baseName;
        }

        public static string GetUniqueName(Type t, System.Web.UI.Control parent)
        {
            return GetUniqueName(t.Name, parent);
        }

        public static string GetUniqueName(string baseName, Control parent)
        {
            int count = 1;

            while (parent.FindControl(baseName + count.ToString()) != null)
            {
                count++;
            }
            return baseName + count.ToString();
        }
        #endregion 
        
        #region File Type
        public static bool IsImage(string url)
        {
            switch (UWeb.GetExtension(url))
            {
                case ".bmp":
                case ".hlp":
                case ".img":
                case ".jpg":
                case ".jpeg":
                case ".pic":
                case ".png":
                case ".tif":
                case ".tiff":
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsMovie(string url)
        {
            switch (UWeb.GetExtension(url))
            {
                case ".mov":
                case ".flv":
                case ".wmv":
                case ".mpeg":
                case ".rm":
                case ".ram":
                    return true;
                default:
                    return false;
            }
        }

        #endregion
        #endregion

        public static void Error(HtmlTextWriter w, Exception ex)
        {
            Literal l = new Literal();

            l.Text = App.Model.AppException.GetError(ex);

            l.RenderControl(w);
        }
    }
}
