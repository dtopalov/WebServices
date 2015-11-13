namespace Strings
{
    using System;
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class StringsService : IStringsService
    {
        public int StringContainsOtherString(string first, string second)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second) || first.Length > second.Length)
            {
                return 0;
            }

            int lastFoundIndex;

            if (second.IndexOf(first, StringComparison.Ordinal) > -1)
            {
                lastFoundIndex = second.IndexOf(first, StringComparison.Ordinal);
            }
            else
            {
                return 0;
            }

            lastFoundIndex = second.IndexOf(first, lastFoundIndex, StringComparison.Ordinal) + 1;

            var countOfOccurences = 1;

            while (second.IndexOf(first, lastFoundIndex, StringComparison.Ordinal) > -1)
            {
                lastFoundIndex = second.IndexOf(first, lastFoundIndex, StringComparison.Ordinal) + 1;
                countOfOccurences++;
            }

            return countOfOccurences;
        }
    }
}
