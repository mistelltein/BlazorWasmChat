using BlazorWasmChat.Repositories;
using ChatModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWasmChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController(ChatRepo chatRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Chat>>> GetChatsAsync() =>
            Ok(await chatRepo.GetChatsAsync());
    }
}