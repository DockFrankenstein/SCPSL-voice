using ConsoleSystem;
using ConsoleSystem.Logic;
using System.Reflection;

using static ConsoleSystem.ConsoleLogger;

Console.Title = "SL voice controller";

ConsoleController.Initialize();

Log("Invoking initialization...", ConsoleColor.Gray);

List<MethodInfo> initializeMethods = TypeFinder.FindAttributesList<InitializeAttribute>();

for (int i = 0; i < initializeMethods.Count; i++)
{
    if (initializeMethods[i].GetParameters().Count() != 0)
    {
        LogError($"Cannot run method {initializeMethods[i]}: method cannot contain generic parameters!");
        continue;
    }

    if (!initializeMethods[i].IsStatic)
    {
        LogError($"Cannot run method {initializeMethods[i]}: method must be static!");
        continue;
    }

    initializeMethods[i].Invoke(null, null);
}

ConsoleController.Start();

new System.Speech.Synthesis.SpeechSynthesizer().Speak("Ready");