function RsTextBox_onfocus(txt, css, text) {
	if(txt.value == text)
	{
		txt.value = '';
	}
	txt.className = css;
  txt.style.color = '';
}

function RsTextBox_onblur(txt, css, text) {
	if(txt.value == '')
	{
		txt.value = text; 
		txt.className = css;
	}
}

function RsTextBox_onkeypress(e, btnId) {
 var keyCode 

  if(e && e.which){ 
    e = e;
    keyCode = e.which ;
  }
  else {
    e = event;
    keyCode = e.keyCode;
  }

  if(keyCode == 13){
    var btn = document.getElementById(btnId);
    
    if (btn == null) {
      alert('Button id [' + btnId + '] not found on form.');
    } 
    else {
      btn.click();
    }
    
    return false;
  }
  else {
    return true;
  }
}
