using Sitecore.Links;
using Sitecore.Sites;
using Sitecore.Web;
using System;

namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  public class LocalizableLinkBuilder : LinkProvider.LinkBuilder
  {
    public LocalizableLinkBuilder(UrlOptions options)
      : base(options)
    {
    }

    protected override string GetServerUrlElement(SiteInfo siteInfo)
    {
      return GetServerUrlElement(siteInfo, this.AlwaysIncludeServerUrl);
    }

    public string GetServerUrlElement(SiteInfo siteInfo, bool alwaysIncludeServerUrl)
    {
      SiteContext site = Context.Site;
      string str = (site != null) ? site.Name : string.Empty;
      string str2 = alwaysIncludeServerUrl ? WebUtil.GetServerUrl() : string.Empty;
      if ((siteInfo == null) || siteInfo.Name.Equals(str, StringComparison.OrdinalIgnoreCase))
      {
        return str2;
      }
      string hostName = WebUtil.GetHostName();
      string[] values = new string[] { siteInfo.TargetHostName, IsHostNameUnique(siteInfo) ? siteInfo.HostName : null, hostName };
      string str4 = StringUtil.GetString(values);
      if ((str4 == string.Empty) || (str4.IndexOf('*') >= 0))
      {
        return str2;
      }
      if (str4.Contains("|"))
      {
        str4 = str4.Substring(0, str4.IndexOf('|'));
      }
      string scheme = WebUtil.GetScheme();
      string[] textArray2 = new string[] { siteInfo.Scheme, scheme, "http" };
      string str6 = StringUtil.GetString(textArray2);
      if (str6 == string.Empty)
      {
        return str2;
      }
      int port = WebUtil.GetPort();
      int @int = MainUtil.GetInt(siteInfo.Port, port);
      if ((str4.Equals(hostName, StringComparison.OrdinalIgnoreCase) && (@int == port)) && str6.Equals(scheme, StringComparison.OrdinalIgnoreCase))
      {
        return str2;
      }
      string str7 = str6 + "://" + str4;
      if ((@int > 0) && (@int != 80))
      {
        str7 = str7 + ":" + @int;
      }
      return str7;
    }

    public static bool IsHostNameUnique(SiteInfo siteInfo)
    {
      char[] anyOf = new char[] { '*' };
      return (siteInfo.HostName.IndexOfAny(anyOf) < 0);
    }
  }
}
