using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Runtime.InteropServices.JavaScript.JSType;

using var cts = new CancellationTokenSource();

var bot = new TelegramBotClient("7678371646:AAF6pJOH6mR8KORaIZm2lj3K7JDMyrAgITA", cancellationToken: cts.Token);
var me = await bot.GetMe();
bot.OnMessage += OnMessage;
bot.OnError += OnError;
bot.OnUpdate += OnUpdate;

Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel();

IReplyMarkup Menu()
{
    var Keyboard = new List<List<KeyboardButton>>()
    {
        new List<KeyboardButton> {new KeyboardButton{Text = "Лошадка" }, new KeyboardButton{Text = "Котик" },
        new KeyboardButton{Text = "Собачка" }, new KeyboardButton{Text = "Попугай" } }
    };
    return new ReplyKeyboardMarkup(Keyboard);
}

async Task OnMessage(Message msg, UpdateType type)
{
    await bot.SendMessage(msg.Chat.Id, msg.Text, replyMarkup: Menu());

    switch (msg.Text)
    {
        case "Лошадка":
            {
                var message = await bot.SendPhoto(msg.Chat.Id, "https://welcometoural.ru/storage/9298/poni-1.jpeg");
            }
            break;

        case "Котик":
            {
                var message = await bot.SendPhoto(msg.Chat.Id, "https://i.pinimg.com/originals/7e/1b/fd/7e1bfd1191112533fe9872ef47398823.jpg");
            }
            break;

        case "Собачка":
            {
                var message = await bot.SendPhoto(msg.Chat.Id, "https://i.pinimg.com/736x/5b/7c/a6/5b7ca64e140fa4c92898599732d551a9.jpg");
            }
            break;

        case "Попугай":
            {
                var message = await bot.SendPhoto(msg.Chat.Id, "https://avatars.mds.yandex.net/i?id=85593004364d46367452a6b9dbc102fe_l-4314243-images-thumbs&n=13");
            }
            break;
    }
}

async Task OnUpdate(Update update)
{
    if (update is { CallbackQuery: { } query })
    {
        await bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
        await bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
    }
}

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception);
}
