using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Infrastructure.Persistence;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidatorTests : TestFixture
    {
        private readonly ApplicationDbContext _context;

        public CreateTodoListCommandValidatorTests()
        {
            _context = Context;
        }

        [Fact]
        public void ShouldBeValid_WhenListTitleIsUnique()
        {
            var command = new CreateTodoListCommand
            {
                Title = "Bucket List"
            };

            var validator = new CreateTodoListCommandValidator(_context);

            var result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(p => p.Title);
        }

        [Fact]
        public void ShouldBeInvalid_WhenTitleIsNotUnique()
        {
            var command = new CreateTodoListCommand
            {
                Title = "Todo List"
            };

            var validator = new CreateTodoListCommandValidator(_context);

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(p => p.Title)
                .WithErrorCode("UniqueTitle");
        }
    }
}
