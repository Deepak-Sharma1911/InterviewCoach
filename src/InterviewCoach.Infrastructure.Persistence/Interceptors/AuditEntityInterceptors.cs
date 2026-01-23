using InterviewCoach.Application.Abstractions;
using InterviewCoach.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterviewCoach.Infrastructure.Persistence.Interceptors
{
    public class AuditEntityInterceptors : SaveChangesInterceptor
    {
        private readonly ICurrentUser _currentUser;
        public AuditEntityInterceptors(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        override public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            (bool flowControl, InterceptionResult<int> value) = ProcessAuditEntitiesBeforeSave(eventData, result);
            if (!flowControl)
            {
                return value;
            }
            return base.SavingChanges(eventData, result);
        }
        override public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            (bool flowControl, InterceptionResult<int> value) = ProcessAuditEntitiesBeforeSave(eventData, result);
            if (!flowControl)
            {
                return new ValueTask<InterceptionResult<int>>(value);
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        private (bool flowControl, InterceptionResult<int> value) ProcessAuditEntitiesBeforeSave(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null)
                return (flowControl: false, value: base.SavingChanges(eventData, result));

            var entries = context.ChangeTracker.Entries<IAuditableEntity>().Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.HasChangedOwnedEntities());

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.MarkCreated(_currentUser.UserId, DateTime.UtcNow);
                }
                foreach (PropertyEntry item in entry.Properties)
                {
                    if (!Equals(item.CurrentValue, item.OriginalValue))
                    {
                        Console.WriteLine($"Value changed from {item.OriginalValue} to {item.CurrentValue}");
                    }
                }
                entity.MarkModified(_currentUser.UserId, DateTime.UtcNow);
            }
            return (flowControl: true, value: default);
        }

    }
    public static class EntityEntryExtensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entity)
        {
            return entity.References.Any(x => x.TargetEntry != null &&
                                 x.TargetEntry.Metadata.IsOwned() &&
                                 (x.TargetEntry.State == EntityState.Added || x.TargetEntry.State == EntityState.Modified));

        }
    }
}
