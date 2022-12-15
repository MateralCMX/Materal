using MainDemo.CommandHandlers;
using Materal.Common;
using System.Reflection;
using System.Text;

namespace MainDemo
{
    public class CommandBus
    {
        private readonly Type[] commandHandlers;
        private const string commandHandlerSuffix = "CommandHandler";
        public CommandBus()
        {
            commandHandlers = Assembly.GetExecutingAssembly().GetTypes();
            commandHandlers = commandHandlers.Where(m => !m.IsAbstract && m.Name.EndsWith(commandHandlerSuffix) && m.IsAssignableTo(typeof(ICommandHandler))).ToArray();
        }
        public bool ExcuteCommand(string command, IServiceProvider services)
        {
            Type? hander = commandHandlers.FirstOrDefault(m => m.Name == $"{command}{commandHandlerSuffix}");
            if (hander == null) return HelperHandler(command); ;
            ConstructorInfo? constructorInfo = hander.GetConstructor(new[] { typeof(IServiceProvider) });
            if (constructorInfo == null) return HelperHandler(command);
            ICommandHandler commandHandler = (ICommandHandler)constructorInfo.Invoke(new[] { services });
            return commandHandler.Handler();
        }

        private bool HelperHandler(string command)
        {
            StringBuilder message = new();
            if(command != "Helper")
            {
                message.AppendLine("未识别命令。");
            }
            message.AppendLine("可接收命令：");
            int suffixLength = commandHandlerSuffix.Length;
            foreach (var item in commandHandlers)
            {
                message.AppendLine(item.Name[0..^suffixLength]);
            }
            ConsoleQueue.WriteLine(message.ToString());
            return true;
        }
    }
}
