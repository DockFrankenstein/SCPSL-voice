namespace ConsoleSystem.Logic
{
    public static class ConsoleArgumentSorter
    {
        //TODO: clean up this mess
        //This is one of the oldest peaces of qASIC code, it should get replaced one day by a cleaner solution
        public static List<string> SortCommand(string? cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd)) return new List<string>();

            List<string> args = new List<string>();

            bool isAdvanced = false;
            string currentString = "";

            for (int i = 0; i < cmd.Length; i++)
            {
                if (isAdvanced)
                {
                    if (cmd[i] == '"' && (cmd.Length > i + 1 && cmd[i + 1] == ' ' || cmd.Length > i))
                    {
                        isAdvanced = false;
                        continue;
                    }
                    currentString += cmd[i];
                    continue;
                }
                if (cmd[i] == ' ')
                {
                    args.Add(currentString);
                    currentString = "";
                    continue;
                }
                if (cmd[i] == '"' && (i != 0 && cmd[i - 1] == ' ' || i == 0) && currentString == "")
                {
                    isAdvanced = true;
                    continue;
                }
                currentString += cmd[i];
            }
            args.Add(currentString);
            return args;
        }
    }
}