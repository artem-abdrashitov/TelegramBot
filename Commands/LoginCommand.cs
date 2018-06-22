using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Data;
using User = TelegramBot.Models.DataBase.User;

namespace TelegramBot.Commands
{
    public class LoginCommand : ITelegramCommand
    {
        private readonly ITelegramBotClient _client;
        private readonly IRepository<User> _userRepository;

        public LoginCommand(ITelegramBotClient client, IRepository<User> userRepository)
        {
            _client = client;
            _userRepository = userRepository;
        }

        public string Name => "login";
        public bool IsProtected => false;

        public async Task ExecuteAsync(Message message)
        {
            // security
            if (_userRepository.GetItem(x => x.TelegramUserId == message.Chat.Id) != null)
            {
                await _client.SendTextMessageAsync(message.Chat.Id, "Invalid command!");
                return;
            }

            if (message.Text.Replace($"/{Name}", string.Empty).Trim() == "password")
            {
                _userRepository.Create(new User
                {
                    TelegramUserId = message.Chat.Id
                });


                await _client.SendTextMessageAsync(message.Chat.Id, "Welcome!");

            }
            else
            {
                await _client.SendTextMessageAsync(message.Chat.Id, "Unknown password");
            }

        }
    }
}