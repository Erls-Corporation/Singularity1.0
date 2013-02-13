<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
      GlobalAsax.Application_Start(sender, e);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
      GlobalAsax.Application_End(sender, e);
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
      GlobalAsax.Application_Error(sender, e);
    }

    void Session_Start(object sender, EventArgs e) 
    {
      GlobalAsax.Session_Start(sender, e);
    }

    void Session_End(object sender, EventArgs e) 
    {
      GlobalAsax.Session_End(sender, e);
    }
    
    void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
    {
      GlobalAsax.FormsAuthentication_OnAuthenticate(sender, args);
    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
      GlobalAsax.Application_BeginRequest(sender, e);
    }
</script>
