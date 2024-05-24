using System;
using System.Net.Mime;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using GigaChatAdapter;
using GigaChatAdapter.Completions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TGBot.PrivateClass;

namespace TGBot
{
    internal class Program
    {
        // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
        private static ITelegramBotClient _botClient;
        // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
        private static ReceiverOptions _receiverOptions;
        static async Task Main(string[] args)
        {

            _botClient = new TelegramBotClient(PrivateClass.PrivateClass.token);
            _receiverOptions = new ReceiverOptions // Также присваем значение настройкам бота
            {
                AllowedUpdates = new[] // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
                {
                    UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
                },
                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
                ThrowPendingUpdates = true,
            };
            _botClient.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        //аутентификационные данные из личного кабинета для Gigachat
        static string authenticationData = PrivateClass.PrivateClass.authenticationData;

        private static bool problem = false;
        private static bool sendMessageToAi = false;
        private static Completion completion = new Completion();
        static ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "Cообщить о проблеме", "Задонатить разработчикам" },
            new KeyboardButton[] { "Спросить совета у ИИ" },
        })
        {
            ResizeKeyboard = true
        };

        async private static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"Received a '{message.Text}' message in chat {message.Chat.Id}.");
            //botClient.AnswerCallbackQueryAsync(
            //    callbackQueryId: update.CallbackQuery.Id,
            //    text: "test"
            //    );

            if (replyKeyboardMarkup.Keyboard.ElementAt(0).ElementAt(0).Text == message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Введите Вашу проблему:",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                problem = true;
                return;
            }
            if (replyKeyboardMarkup.Keyboard.ElementAt(0).ElementAt(1).Text == message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    parseMode: ParseMode.Html,
                    text: "Реквизиты:\n" +
                          "Банк: Сбербанк\n" +
                          "<a href=\"https://online.sberbank.ru/CSAFront/index.do\">Перейти в Сбербанк Онлайн</a>\n" +
                          "<tg-emoji emoji-id=\"5368324170671202286\">👇👇👇👇👇</tg-emoji>",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    parseMode: ParseMode.Html,
                    text: "<span class=\"tg-spoiler\">2202 2017 4364 6693</span>",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                return;
            }
            if (problem)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Спасибо, Вашу проблему обязательно разберут!",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                problem = false;
                return;
            }

            if (replyKeyboardMarkup.Keyboard.ElementAt(1).ElementAt(0).Text == message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:
                    "Здесь вы можете спросить совета у ИИ, какой фильм вам больше подойдет на основе самых разных предпочтений. Это могут быть отрывки " +
                    "фильма, которые вы помните или просто необычные запросы и пожелания. Обрабатывается только текст.",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                sendMessageToAi = true;
                return;
            }

            if (sendMessageToAi)
            {
                SendMessageToAI(botClient, update, cancellationToken);
            }
        }
        //ф-ция для обработки запросов пользователей гигачатом
        async private static Task SendMessageToAI(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Authorization auth = new Authorization(authenticationData, GigaChatAdapter.Auth.RateScope.GIGACHAT_API_PERS);
            GigaChatAdapter.Auth.AuthorizationResponse authResult = await auth.SendRequest();
            if (authResult.AuthorizationSuccess)
            {
                
                //completion.History = new List<GigaChatMessage>();
                Console.WriteLine(completion.History);
                if (update.Message.Text == null)
                    return;
                var prompt = update.Message.Text;
                await auth.UpdateToken();
                var result = await completion.SendRequest(auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken, prompt);
                if (result.RequestSuccessed)
                {
                    Console.WriteLine();
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text:
                        result.GigaChatCompletionResponse.Choices.LastOrDefault().Message.Content,
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    //Console.WriteLine(result.ErrorTextIfFailed);
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text:
                        result.ErrorTextIfFailed,
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                }
            }

            
        }
        private static Task Error(ITelegramBotClient botClient, Exception ex, CancellationToken cancellationToken)
        {
            var ErrorMessage = ex switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => ex.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
