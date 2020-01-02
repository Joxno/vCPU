using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Interface
{
    public interface IParseRule
    {
        Either<IParseState> Match(IParseState State);
    }
}
