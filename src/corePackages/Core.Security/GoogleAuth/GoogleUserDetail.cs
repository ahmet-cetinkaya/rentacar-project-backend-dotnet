namespace Core.Security.GoogleAuth;

public class GoogleUserDetail
{
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}