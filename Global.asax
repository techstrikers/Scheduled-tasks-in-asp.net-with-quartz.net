<%@ Application Language="C#" %>
<%@ Import Namespace="QuartZExample" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        JobScheduler.Start();
    }
  
</script>
