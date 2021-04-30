using CaWorkshop.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CaWorkshop.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator
        : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandValidator(
            IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .MaximumLength(280)
                .NotEmpty()
                .MustAsync(BeUniqueTitle)
                    .WithErrorCode("UniqueTitle")
                    .WithMessage("The specified 'Title' already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title,
            CancellationToken cancellationToken)
        {
            return await _context.TodoLists
                .AllAsync(l => l.Title != title, cancellationToken);
        }
    }
}
