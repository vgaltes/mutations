using System.Text;
using VGA.Mutations.Mutators;

namespace VGA.Mutations
{
    using System.Collections.Generic;

    public class MutationResult
    {
        public string MutationPerformed { get; set; }
        public List<TestResult> TestResults { get; set; }
        public string MutatorUsed { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("{0} using {1} executing these tests:", MutationPerformed,
                MutatorUsed));
            TestResults.ForEach(tr => builder.AppendLine(string.Format("\tTest {0} -> {1}", tr.TestName, tr.Killed ? "Killed" : "Survived" )));

            return builder.ToString();
        }
    }
}