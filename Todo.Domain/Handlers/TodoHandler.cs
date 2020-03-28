using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();

            //Fail Fast Validation
            if(command.Invalid)
                return new GenericCommandResult(
                    false, "Ops, parece que sua tarefa está errada",command.Notifications);

            //gera o TodoItem
             var todo = new TodoItem(command.Title, command.User, command.Date);

            //salva no banco
             _repository.Create(todo);

             return new GenericCommandResult(true,"Tarefa salva", todo);
        }
    }
}