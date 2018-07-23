namespace Sitecore.Support.XA.Foundation.Multisite.LinkManagers
{
  using Sitecore.Links;

  public class LocalizableLinkProvider : Sitecore.XA.Foundation.Multisite.LinkManagers.LocalizableLinkProvider
  {
    protected override LinkProvider.LinkBuilder CreateLinkBuilder(UrlOptions options) =>
        new LocalizableLinkBuilder(options);
  }
}