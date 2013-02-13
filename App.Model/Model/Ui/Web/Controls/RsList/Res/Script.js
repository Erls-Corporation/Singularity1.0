// Invoked to check/uncheck all when header checkbox is clicked
// ---------------------------------------------------------------
function CheckAll(me)
{
    // Assume the ID of this element is controlID_HeaderButton 
    // (i.e. GridView1_HeaderButton)

    var index = me.name.indexOf('_');  
    var prefix = me.name.substr(0,index); 

    // Looks for the right checkbox
    for(i=0; i<document.forms[0].length; i++) 
    { 
        var o = document.forms[0][i]; 
        if (o.type == 'checkbox') 
        { 
            if (me.name != o.name) 
            {
                if (o.name.substring(0, prefix.length) == prefix) 
                {
                    // Needs to set the state of the header to any child 
                    // checkbox. To avoid passing too many parameters, 
                    // sets state by simulating the click. To ensure that 
                    // the click sets the checkbox to the "me.checked" 
                    // state, it must first the current value. 
                    o.checked = !me.checked; 
                    o.click(); 
                }
            }
        } 
    } 
}

// Invoked to properly style the checkbox on a row
// ---------------------------------------------------------
function ApplyStyle(me, selectedForeColor, selectedBackColor, foreColor, backColor, bold, checkBoxHeaderId) 
{ 
    var td = me.parentNode; 
    if (td == null) 
        return; 
        
    var tr = td.parentNode;
    if (me.checked)
    { 
       tr.style.fontWeight = 700;
       tr.style.color = selectedForeColor; 
       tr.style.backgroundColor = selectedBackColor; 
    } 
    else 
    { 
       document.getElementById(checkBoxHeaderId).checked = false;
       tr.style.fontWeight = bold; 
       tr.style.color = foreColor; 
       tr.style.backgroundColor = backColor; 
    } 
}

