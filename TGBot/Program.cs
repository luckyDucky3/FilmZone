using System;
using System.Net.Mime;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using GigaChatAdapter;

namespace TGBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("6675853481:AAHnkqbJ4zRgUMAzhfnhqtLS_Wik6q596ho");
            botClient.StartReceiving(Update, Error);
            
            Console.ReadLine();
        }

        //аутентификационные данные из личного кабинета для Gigachat
        static string authData = "ZWYyOWM3YTQtNjAxOS00NWRmLTkzOTItNTk2YjhiZjAwNDczOjI2OTM1ZTM3LTZjNWMtNDIxYy1iZDFjLTdkMGY5ZWIzZjJlYw==";

        private static bool problem = false;
        async private static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"Received a '{message.Text}' message in chat {message.Chat.Id}.");
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Cообщить о проблеме", "Задонатить разработчикам" },
                new KeyboardButton[] { "Спросить совета у ИИ" },
            })
            {
                ResizeKeyboard = true
            };

            if (replyKeyboardMarkup.Keyboard.ElementAt(0).ElementAt(0).Text == update.Message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Введите Вашу проблему:",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                problem = true;
            }
            if (replyKeyboardMarkup.Keyboard.ElementAt(0).ElementAt(1).Text == update.Message.Text)
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

            if (replyKeyboardMarkup.Keyboard.ElementAt(1).ElementAt(0).Text == update.Message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:
                    "Здесь вы можете спросить совета у ИИ, какой фильм вам больше подойдет на основе самых разных предпочтений. Это могут быть отрывки " +
                    "фильма, которые вы помните или просто необычные запросы и пожелания. Обрабатывается только текст.",
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                Authorization auth = new Authorization(authData, GigaChatAdapter.Auth.RateScope.GIGACHAT_API_PERS);
                GigaChatAdapter.Auth.AuthorizationResponse authResult = await auth.SendRequest();
                if (authResult.AuthorizationSuccess)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text:
                        "Введите ваш запрос:",
                        replyMarkup: replyKeyboardMarkup,
                        cancellationToken: cancellationToken);
                    async Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                    {
                        Completion completion = new Completion();
                        if (update.Message.Text == null)
                            return;
                        var prompt = update.Message.Text;
                        await auth.UpdateToken();
                        var result = await completion.SendRequest(auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken, prompt);
                        if (result.RequestSuccessed)
                        {
                            Console.WriteLine();
                            await botClient.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text:
                                result.GigaChatCompletionResponse.Choices.LastOrDefault().Message.Content,
                                replyMarkup: replyKeyboardMarkup,
                                cancellationToken: cancellationToken);
                        }
                        else
                        {
                            Console.WriteLine(result.ErrorTextIfFailed);
                        }
                    }

                }
            }
        }


        async private static Task Error(ITelegramBotClient botClient, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}