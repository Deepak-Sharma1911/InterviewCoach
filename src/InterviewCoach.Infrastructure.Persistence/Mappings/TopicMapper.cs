using InterviewCoach.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewCoach.Infrastructure.Persistence.Mappings
{
    public static class TopicMapper
    {
        public static Topic ToEntityTopic(this TopicDomain.Topic domainTopic)
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
            var topic = Domain.Entities.Topic.Rehydrate(
                entityTopic.Id,
                entityTopic.Title,
                entityTopic.Slug,
                entityTopic.DisplayOrder,
                entityTopic.ParentTopicId,
                entityTopic.IsActive ?? true,
                entityTopic.CreatedBy,
                entityTopic.CreatedUtcDate,
                entityTopic.LastModifiedBy,
                entityTopic.LastUtcModified);

            foreach (Page pageEf in entityTopic.Pages)
            {
                topic.RehydratePage(PageMapper.ToDomainPage(pageEf));
            }

            return topic;
        }
    }
}
