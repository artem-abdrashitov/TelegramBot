using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public interface ITelegramCommand
    {
        string Name { get; }
        bool IsProtected { get; }
        Task ExecuteAsync(Message message);
    }
}