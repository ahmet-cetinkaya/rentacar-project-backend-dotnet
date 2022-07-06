namespace Core.Security.MicrosoftAuth;

public class MicrosoftUserDetail
{
    public string dataContext { get; set; }
    public string DisplayName { get; set; }
    public string Surname { get; set; }
    public string GivenName { get; set; }
    public string Id { get; set; }
    public string UserPrincipalName { get; set; }
    public List<string> BusinessPhones { get; set; }
    public string JobTitle { get; set; }
    public string Mail { get; set; }
    public string MobilePhone { get; set; }
    public string OfficeLocation { get; set; }
    public string PreferredLanguage { get; set; }
}