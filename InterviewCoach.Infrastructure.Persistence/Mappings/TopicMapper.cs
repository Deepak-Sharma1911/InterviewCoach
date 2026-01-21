using InterviewCoach.Infrastructure.Persistence.Database.Entities;

namespace InterviewCoach.Infrastructure.Persistence.Mappings
{
    public static class TopicMapper
    {
        //Mapping DomainToEntity and EntityToDomain can be added here in future
        public static Topic ToEntityTopic(this Domain.Entities.Topic domainTopic)
        {
            return new Topic
            {
                Id = domainTopic.Id,
                ParentTopicId = domainTopic.ParentTopicId,
                Title = domainTopic.Title,
                Slug = domainTopic.Slug,
                DisplayOrder = domainTopic.DisplayOrder,
                IsActive = domainTopic.IsActive,
                CreatedBy = domainTopic.CreatedBy,
                CreatedUtcDate = domainTopic.CreatedUtcDate,
                LastModifiedBy = domainTopic.LastModifiedBy,
                LastUtcModified = domainTopic.LastUtcModified
            };
        }

        public static Domain.Entities.Topic ToDomainTopic(this Topic entityTopic)
        {
            var domainTopic = Domain.Entities.Topic.Create(entityTopic.Title,
                slug: entityTopic.Slug,
                displayOrder: entityTopic.DisplayOrder,
                parentTopicId: entityTopic.ParentTopicId,
                createdBy: entityTopic.CreatedBy,
                utcNow: entityTopic.CreatedUtcDate);
            // Manually set properties that are not set by the factory method
            domainTopic.GetType().GetProperty("Id")!.SetValue(domainTopic, entityTopic.Id);
            domainTopic.GetType().GetProperty("ParentTopicId")!.SetValue(domainTopic, entityTopic.ParentTopicId);
            domainTopic.GetType().GetProperty("IsActive")!.SetValue(domainTopic, entityTopic.IsActive);
            domainTopic.GetType().GetProperty("LastModifiedBy")!.SetValue(domainTopic, entityTopic.LastModifiedBy);
            domainTopic.GetType().GetProperty("LastUtcModified")!.SetValue(domainTopic, entityTopic.LastUtcModified);
            return domainTopic;
        }
    }
}
