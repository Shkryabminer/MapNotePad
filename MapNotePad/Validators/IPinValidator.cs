using MapNotePad.Models;

namespace MapNotePad.Validators

{
    public interface IPinValidator
    {
        bool PinModelIsValid(PinModel model);
    }
}
