using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Tests.CommandTests
{
    [TestClass]
    public class CreateTodoCommandTests
    {
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo da tarfa","luiz fernando", DateTime.Now);
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("","", DateTime.Now);

        public CreateTodoCommandTests()
        {   
            _validCommand.Validate();
            _invalidCommand.Validate();
        }

        [TestMethod]
        public void Dado_um_comnando_invalido()
        {     
                
            Assert.AreEqual(_invalidCommand.Valid ,false);

        }        
        [TestMethod]
        public void Dado_um_comnando_valido()
        {     
            Assert.AreEqual(_validCommand.Valid,true);
        }
    }
}
