using System;
using System.Net.Mime;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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

        private static bool problem = false;
        async private static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"Received a '{message.Text}' message in chat {message.Chat.Id}.");
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {
                new KeyboardButton[] { "Cообщить о проблеме", "Задонатить разработчикам" },
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
                return;
            }
            if (replyKeyboardMarkup.Keyboard.ElementAt(0).ElementAt(1).Text == update.Message.Text)
            {
                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Реквизиты:\n2202 2017 4364 6693\nСбербанк",
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
        }


        async private static Task Error(ITelegramBotClient botClient, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}