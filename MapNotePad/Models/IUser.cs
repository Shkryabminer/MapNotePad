namespace MapNotePad.Models
{
    public interface IUser : IEntity
    {        
        string FirstName { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        string LastName { get; set; }
    }
}
