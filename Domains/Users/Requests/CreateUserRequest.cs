namespace HomeApi.Domains.Users.Requests;

public class CreateUserRequest
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}