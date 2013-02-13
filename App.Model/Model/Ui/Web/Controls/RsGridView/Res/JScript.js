function GridClass(GridClientID)
{
    this.GridviewID = GridClientID;
    this.GridviewObj = document.getElementById(GridClientID);

} 

GridClass.prototype.CheckSelectedRows=function()
{   
     if (this.GridviewObj != undefined)
     {
        var chkBoxes = this.GridviewObj.getElementsByTagName("input");
        var chkBoxId = new String();
        var isSelected = false;
         for(var i=0;i<chkBoxes.length;i++)
         {  
            chkBoxId = '';
            if(chkBoxes[i].type == 'checkbox')
            {
                chkBoxId = chkBoxes[i].getAttribute("id")
                if(chkBoxId.indexOf('chk')>0)
                {
                    if(chkBoxes[i].checked == true)
                    {
                        isSelected = true;
                        return true;
                    }
                }
            }
         }
         
        return false;
         
     }    
}
GridClass.prototype.CheckUnCheckRows=function(chkVal)
{
    
     if (this.GridviewObj != undefined)
     {
        var chkBoxes = this.GridviewObj.getElementsByTagName("input");
        var chkBoxId = new String();
         for(var i=0;i<chkBoxes.length;i++)
         {  
            chkBoxId = '';
            if(chkBoxes[i].type == 'checkbox')
            {
                chkBoxId = chkBoxes[i].getAttribute("id")
                if(chkBoxId.indexOf('chk')>0)
                {
                    chkBoxes[i].checked = chkVal;
                }
            }
         }
     }
}