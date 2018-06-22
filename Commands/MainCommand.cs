using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Services.Stores;

namespace TelegramBot.Commands
{
    public class MainCommand:ITelegramCommand
    {
        public string Name => "main";
        public bool IsProtected => true;
        private ITelegramBotClient _client;

        public MainCommand(ITelegramBotClient client)
        {
            _client = client;
        }

        public async Task ExecuteAsync(Message message)
        {
            var report = CurrencyInfoStore.GetLastPoloneix();
            await _client.SendTextMessageAsync(message.Chat.Id, report,ParseMode.Markdown);
        }
    }
}