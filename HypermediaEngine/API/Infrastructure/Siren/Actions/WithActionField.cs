using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HypermediaEngine.API.Infrastructure.Requests.Actions;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Siren.Actions
{
    public interface IHavingActionFieldOverride<T> : IWithAction<T> where T : ApiAction
    {
        IHavingActionFieldOverride<T> Having(Action<Field> @override);
        string GetFieldName();
        void Apply(Field field);
    }

    public partial class WithAction<T> : IHavingActionFieldOverride<T> where T : ApiAction
    {
        private readonly Expression<Func<T, object>> _fieldSelector;
        private readonly IList<Action<Field>> _fieldOverrides;

        private WithAction()
        {
            _fieldOverrides = new List<Action<Field>>();
        }

        private WithAction(Expression<Func<T, object>> fieldSelector)
            : this()
        {
            _fieldSelector = fieldSelector;
        }

        public static IHavingActionFieldOverride<T> Field(Expression<Func<T, object>> fieldSelector)
        {
            return new WithAction<T>(fieldSelector);
        }

        public IHavingActionFieldOverride<T> Having(Action<Field> @override)
        {
            _fieldOverrides.Add(@override);
            return this;
        }

        public string GetFieldName()
        {
            if (_fieldSelector == null)
                return null;

            var unaryExpression = _fieldSelector.Body as UnaryExpression;
            if (unaryExpression != null)
                return (unaryExpression.Operand as MemberExpression).Member.Name;

            return (_fieldSelector.Body as MemberExpression).Member.Name;
        }

        public void Apply(Field field)
        {
            foreach (var @override in _fieldOverrides)
                @override.Invoke(field);
        }
    }
}