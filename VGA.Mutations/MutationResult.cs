using VGA.Mutations.Mutators;

namespace VGA.Mutations
{
    using System.Collections.Generic;

    public class MutationResult
    {
        public string MutationPerformed { get; set; }
        public List<TestResult> TestResults { get; set; }
        public string MutatorUsed { get; set; }
    }
}