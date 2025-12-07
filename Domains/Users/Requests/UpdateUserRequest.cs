namespace HomeApi.Domains.Users.Requests;

public class UpdateUserRequest
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}