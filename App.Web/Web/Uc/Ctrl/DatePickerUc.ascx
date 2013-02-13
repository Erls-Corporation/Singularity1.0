<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePickerUc.ascx.cs"
  Inherits="DatePickerUc" %>
<style type="text/css">
.plain {height:20px; border:1px solid gray; font:9pt arial; text-align:center;  vertical-align:middle;}
.vbtn {height:20px; width:18px; font:bold 10pt arial; border:2px solid silver;  vertical-align:middle;}
.vsel {height:18px; width:60px; border:1px solid gray; font:9pt arial; text-align:center; vertical-align:middle;}
</style>
<link rel='stylesheet' type='text/css' href='~/Web/Scripts/datepicker/demos/Classic/demo.css' />
<select name="arrDate_day" class="vsel" onchange="if(self.gfPop)gfPop.updateHidden(this)">

  <script type="text/javascript">document.write('<option value="-">-day-');for(var i=1;i<=31;i++) {document.write('<option value="'+i+'">'+i)}</script>

</select>
/
<select name="arrDate_mon" class="vsel" onchange="if(self.gfPop)gfPop.updateHidden(this)">

  <script type="text/javascript">document.write('<option value="-">-mon-');for(var i=1;i<=12;i++) {document.write('<option value="'+i+'">'+i)}</script>

</select>
/
<select name="arrDate_year" class="vsel" onchange="if(self.gfPop)gfPop.updateHidden(this)">

  <script type="text/javascript">document.write('<option value="-">-year-');for(var i=1980;i<=2030;i++) {document.write('<option value="'+i+'">'+i)}</script>

</select>
<input name="popcal" onclick="if(blur)blur();var fm=form;if(self.gfPop)gfPop.fStartPop(fm.arrDate,fm.arrDate, this);"
  type="button" value="..." class="vbtn" />
<input name="arrDate" type="hidden" value="" style="width: 54px" />
<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
  src="../Scripts/datepicker/demos/Classic/ipopeng.htm" scrolling="no" frameborder="0"
  style="visibility: visible; z-index: 999; position: absolute; top: -500px; left: -500px;">
</iframe>
