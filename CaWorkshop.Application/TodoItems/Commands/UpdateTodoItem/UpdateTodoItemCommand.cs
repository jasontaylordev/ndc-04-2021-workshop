﻿using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using MediatR;

namespace CaWorkshop.Application.TodoItems.Commands.UpdateTodoItem
{
    public partial class UpdateTodoItemCommand : IRequest
    {
        public long Id { get; set; }

        public int ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemCommandHandler
            : AsyncRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateTodoItemCommand request,
                CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            Guard.Against.NotFound(entity, request.Id);

            entity.ListId = request.ListId;
            entity.Title = request.Title;
            entity.Done = request.Done;
            entity.Priority = (PriorityLevel)request.Priority;
            entity.Note = request.Note;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
