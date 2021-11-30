using EFCoreConfiguration.Models;
using EFCoreConfiguration.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Task = System.Threading.Tasks.Task;

namespace Diploma.Hubs
{
    [Authorize]
    public class CommentsHub : Hub
    {
        private readonly TaskInfoRepository _repository;
        private readonly UserManager<User> _manager;
        public CommentsHub(
            TaskInfoRepository repository,
            UserManager<User> manager)
        {
            _repository = repository;
            _manager = manager;

        }

        public async Task Subscribe(int taskInfoId, string studentUserName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, studentUserName + taskInfoId);
        }

        public async Task Unsubscribe(int taskInfoId, string studentUserName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, studentUserName + taskInfoId);
        }
        public async Task Send(int taskInfoId, string studentUserName, string message)
        {
            var student = await _manager.FindByNameAsync(studentUserName);
            var user = await _manager.FindByNameAsync(Context.User.Identity.Name);
            var comment = new Comment
            {
                Sender = user,
                Receiver = user != student ? student : null,
                Message = message
            };

            await _repository.AddComment(taskInfoId, comment);
            await Clients.Group(student.UserName + taskInfoId).SendAsync("ReceiveMessage", user.UserName, message );
        }
    }
}
