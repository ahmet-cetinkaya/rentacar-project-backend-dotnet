namespace Application.Features.UserOperationClaims.Dtos;

public class CreatedUserOperationClaimDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}