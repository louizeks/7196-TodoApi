using System;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Tests.Repositories
{
    public class FakeTodoRepository : ITodoRepository
    {
        public void Create(TodoItem item)
        {            
        }

        public void Update(TodoItem item)
        {            
        }

        public TodoItem GetById(Guid id, string user)
        {
            return new TodoItem ("Titulo","", DateTime.Now);
        }
    }

}