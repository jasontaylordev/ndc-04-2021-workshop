﻿using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaWorkshop.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : AsyncRequestHandler<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateTodoListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(request.Id);

            Guard.Against.NotFound(entity, request.Id);

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
