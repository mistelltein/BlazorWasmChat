using BlazorWasmChat.Repositories;
using ChatModels;
using Microsoft.AspNetCore.SignalR;

namespace BlazorWasmChat.ChatHubs
{
    public class ChatHub(ChatRepo chatRepo) : Hub
    {
        public async Task SendMessage(Chat chat)
        {
            await chatRepo.SaveChatAsync(chat);
            await Clients.All.SendAsync("ReceiveMessage", chat);
        }
    }
}