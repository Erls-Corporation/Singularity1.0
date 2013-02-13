<%@ Control Language="C#" ClassName="SearchGoogleUc" CodeFile="SearchGoogleUc.ascx.cs"
    Inherits="SearchGoogleUc" %>
<form action="http://rafeysoft.com/SearchResult.aspx" id="cse-search-box">
  <div>
    <input type="hidden" name="cx" value="partner-pub-6388462253213296:pqvied-v3k7" />
    <input type="hidden" name="cof" value="FORID:9" />
    <input type="hidden" name="ie" value="ISO-8859-1" />
    <input type="text" class="stb" name="q" size="40" />
    <input type="submit" class="btn" style="width:75px;" name="sa" value="Search" />
  </div>
</form>
<br /><br />
<script type="text/javascript" src="http://www.google.com.pk/coop/cse/brand?form=cse-search-box&amp;lang=en"></script>

<div id="cse-search-results"></div>
<script type="text/javascript">
  var googleSearchIframeName = "cse-search-results";
  var googleSearchFormName = "cse-search-box";
  var googleSearchFrameWidth = 800;
  var googleSearchDomain = "www.google.com.pk";
  var googleSearchPath = "/cse";
</script>
<script type="text/javascript" src="http://www.google.com/afsonline/show_afs_search.js"></script>

