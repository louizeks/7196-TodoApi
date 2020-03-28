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
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
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

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            //Fail Fast Validation
             command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false, "Ops, parece que sua tarefa está errada",command.Notifications);

            //Recupera o TodoItem 
             var todo = _repository.GetById(command.Id, command.User);

            //Altera o título
            todo.UpdateTitle(command.Title);

            //salva no banco
             _repository.Update(todo);

             return new GenericCommandResult(true,"Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
             //Fail Fast Validation
             command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false, "Ops, parece que sua tarefa está errada",command.Notifications);

            //Recupera o TodoItem 
             var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsDone();

            //salva no banco
             _repository.Update(todo);

             return new GenericCommandResult(true,"Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
             //Fail Fast Validation
             command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(
                    false, "Ops, parece que sua tarefa está errada",command.Notifications);

            //Recupera o TodoItem 
             var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsUndone();

            //salva no banco
             _repository.Update(todo);

             return new GenericCommandResult(true,"Tarefa salva", todo);
        }
    }
}
