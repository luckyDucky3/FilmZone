using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
namespace TGBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("6675853481:AAHnkqbJ4zRgUMAzhfnhqtLS_Wik6q596ho");

            using CancellationTokenSource cts = new();

            // // Начало приема не блокирует вызывающий поток. Получение выполняется в пуле потоков.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // получать все типы обновлений, кроме обновлений, связанных с участниками чата
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            // Отправьте запрос на отмену, чтобы остановить бота
            cts.Cancel();

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                // Обрабатывать только обновления сообщений: https://core.telegram.org/bots/api#message
                if (update.Message is not { } message)
                    return;
                // Обрабатывайте только текстовые сообщения
                if (message.Text is not { } messageText)
                    return;

                var chatId = message.Chat.Id;

                Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                //Текст полученного эхо-сообщения
                Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "You said:\n" + messageText,
                    cancellationToken: cancellationToken);
            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

            Console.ReadLine();
        }
    }
}