using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFramework;

public class IdConverter : ValueConverter<Guid, int>
{
    public IdConverter()
        : base(
            id => Convert.ToInt32(id.ToByteArray()),
            id => Guid.NewGuid()
            )
    {
    }
}