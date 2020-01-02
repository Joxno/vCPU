using System;
using System.Collections.Generic;
using System.Linq;
using Core.Architecture.vCPU.Assembler.Interface;
using Core.Architecture.vCPU.Assembler.Models;
using Core.Architecture.vCPU.Assembler.Utility;
using Core.Utility;

namespace Core.Architecture.vCPU.Assembler.Rules
{
    using CreateExprFunc = Func<IEnumerable<Token>, IEnumerable<IExpression>, IExpression>;
    public class CreateExpressionRule : IParseRule
    {
        private readonly IEnumerable<CreateExprFunc> m_CreateExpression = null;
        private readonly IParseRule m_Rule = null;

        public CreateExpressionRule(
            IParseRule Rule,
            CreateExprFunc CreateExpression)
        { 
            m_Rule = Rule;
            m_CreateExpression = new List<CreateExprFunc> { CreateExpression };
        }

        public CreateExpressionRule(
            IParseRule Rule,
            IEnumerable<CreateExprFunc> CreateFunctions
        )
        {
            m_Rule = Rule;
            m_CreateExpression = CreateFunctions;
        }

        public Either<IParseState> Match(IParseState State)
        {
            var t_NewState = m_Rule.Match(State);

            if (t_NewState.HasError())
                return t_NewState;

            return _CreateExpression(State, t_NewState.Value);
        }

        private Either<IParseState> _CreateExpression(IParseState OldState, IParseState NewState)
        {
            var t_Attempt = m_CreateExpression
                .Select(F => _AttemptCreate(F, OldState, NewState))
                .FirstOrDefault(E => !E.HasError());

            return t_Attempt == null ? 
                new Exception("Unable to match any expression creation function") : 
                t_Attempt.HasError() ?
                    t_Attempt.Error :
                    new Either<IParseState>(
                        NewState.ToCopy(NewState.Tokens, OldState.Expressions.Append(t_Attempt.Value))
                    );
        }

        private Either<IExpression> _AttemptCreate(CreateExprFunc CreateFunc, 
            IParseState OldState, IParseState NewState)
        {
            return Try.Call(() => CreateFunc(
                OldState.DiffTokens(NewState),
                NewState.DiffExpressions(OldState))
            );
        }
    }
}