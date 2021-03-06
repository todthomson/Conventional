using System;
using System.Linq;

namespace Conventional.Conventions
{
    public class PropertiesMustHavePublicGettersConventionSpecification : ConventionSpecification
    {
        protected override string FailureMessage => "All properties must have public getters";

        public override ConventionResult IsSatisfiedBy(Type type)
        {
            var toInspect = type.GetProperties().Where(p => p.CanWrite);

            var failures = toInspect.Where(subject => subject.GetGetMethod() == null || subject.GetGetMethod().IsPublic == false).ToArray();

            if (failures.Any())
            {
                var failureMessage =
                    BuildFailureMessage(failures.Aggregate(string.Empty,
                        (s, info) => s + "\t- " + info.Name + Environment.NewLine));

                return ConventionResult.NotSatisfied(type.FullName, failureMessage);
            }

            return ConventionResult.Satisfied(type.FullName);
        }
    }
}