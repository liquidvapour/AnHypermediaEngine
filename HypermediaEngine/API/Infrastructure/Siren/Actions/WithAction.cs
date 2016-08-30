using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;
using Action = Siren.Action;

namespace HypermediaEngine.API.Infrastructure.Siren.Actions
{
    public interface IHavingActionOverride<T> : IWithAction<T> where T : ApiAction
    {
        void Apply(Action action);
    }

    public partial class WithAction<T> : IHavingActionOverride<T> where T : ApiAction
    {
        private readonly Action<Action> _actionOverrides;

        private WithAction(Action<Action> actionOverrides)
        {
            _actionOverrides = actionOverrides;
        }

        public static IHavingActionOverride<T> Property(Action<Action> actionOverrides)
        {
            return new WithAction<T>(actionOverrides);
        }

        public void Apply(Action action)
        {
            if (_actionOverrides != null)
                _actionOverrides.Invoke(action);
        }
    }
}