using InterviewCoach.Domain.Entities;
using InterviewCoach.Domain.Exceptions;

namespace InterviewCoach.Domain.Test
{
    public class DomainTests
    {
        private static readonly DateTime UtcNow = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc);
        private static readonly Guid UserId = Guid.NewGuid();

        [Fact]
        public void Technology_Create_SetsProperties()
        {
            var tech = Technology.Create("C#", "csharp", 1, UserId, UtcNow);

            Assert.Equal("C#", tech.Title);
            Assert.Equal("csharp", tech.Slug);
            Assert.Equal(1, tech.DisplayOrder);
            Assert.True(tech.IsActive);
            Assert.Equal(UserId, tech.CreatedBy);
            Assert.Equal(UtcNow, tech.CreatedUtcDate);
            Assert.Equal(UtcNow, tech.LastUtcModified);
            Assert.NotEqual(Guid.Empty, tech.Id);
        }

        [Theory]
        [InlineData(null, "slug")]
        [InlineData("", "slug")]
        [InlineData("   ", "slug")]
        [InlineData("title", null)]
        [InlineData("title", "")]
        [InlineData("title", "   ")]
        public void Technology_Create_Throws_On_Missing_Title_Or_Slug(string title, string slug)
        {
            Assert.Throws<ArgumentException>(() => Technology.Create(title, slug, 1, UserId, UtcNow));
        }

        [Fact]
        public void Technology_AddTopic_CreatesTopic_And_Prevents_Duplicate_Slug()
        {
            var tech = Technology.Create("Go", "go", 1, UserId, UtcNow);

            var topic = tech.AddTopic("Basics", "basics", null, 1, UserId, UtcNow);

            Assert.NotNull(topic);
            Assert.Equal("Basics", topic.Title);
            Assert.Equal("basics", topic.Slug);
            Assert.Equal(1, tech.Topics.Count);
            Assert.Contains(topic, tech.Topics);

            Assert.Throws<InvalidOperationException>(() =>
                tech.AddTopic("Basics 2", "basics", null, 2, UserId, UtcNow));
        }

        [Fact]
        public void Technology_AddTopic_Throws_When_Parent_Not_Found()
        {
            var tech = Technology.Create("Rust", "rust", 1, UserId, UtcNow);

            var missingParent = Guid.NewGuid();
            Assert.Throws<InvalidOperationException>(() =>
                tech.AddTopic("Advanced", "advanced", missingParent, 2, UserId, UtcNow));
        }

        [Fact]
        public void Technology_Update_Rename_Deactivate_Work()
        {
            var tech = Technology.Create("Old", "old", 1, UserId, UtcNow);
            var modifier = Guid.NewGuid();
            var later = UtcNow.AddMinutes(5);

            tech.Update("NewTitle", "new-slug", 5, modifier, later);
            Assert.Equal("NewTitle", tech.Title);
            Assert.Equal("new-slug", tech.Slug);
            Assert.Equal(5, tech.DisplayOrder);
            Assert.Equal(modifier, tech.LastModifiedBy);
            Assert.Equal(later, tech.LastUtcModified);

            tech.Rename("Renamed", modifier, later);
            Assert.Equal("Renamed", tech.Title);
            Assert.Equal(modifier, tech.LastModifiedBy);

            tech.Deactivate(modifier, later);
            Assert.False(tech.IsActive);
            Assert.Equal(modifier, tech.LastModifiedBy);
        }

        [Fact]
        public void Topic_Create_AddPage_And_Duplicate_Slug_Throws()
        {
            var techId = Guid.NewGuid();
            var topic = Topic.Create(techId, "T", "t", null, 1, UserId, UtcNow);

            var page = topic.AddPage("Intro", "intro", "summary", UserId, UtcNow);
            Assert.NotNull(page);
            Assert.Equal("Intro", page.Title);
            Assert.Equal("intro", page.Slug);
            Assert.Equal(1, topic.Pages.Count);
            Assert.Contains(page, topic.Pages);

            Assert.Throws<DomainException>(() =>
                topic.AddPage("Intro 2", "intro", "summary", UserId, UtcNow));
        }

        [Fact]
        public void Topic_Update_Rename_Deactivate_Work()
        {
            var techId = Guid.NewGuid();
            var topic = Topic.Create(techId, "OldTopic", "old-topic", null, 1, UserId, UtcNow);
            var modifier = Guid.NewGuid();
            var later = UtcNow.AddMinutes(7);

            topic.Update("Updated", "updated", 3, modifier, later);
            Assert.Equal("Updated", topic.Title);
            Assert.Equal("updated", topic.Slug);
            Assert.Equal(3, topic.DisplayOrder);
            Assert.Equal(modifier, topic.LastModifiedBy);

            topic.Rename("RenamedTopic", modifier, later);
            Assert.Equal("RenamedTopic", topic.Title);

            topic.Deactivate(modifier, later);
            Assert.False(topic.IsActive);
            Assert.Equal(modifier, topic.LastModifiedBy);
        }

        [Fact]
        public void Page_AddSection_Prevents_Duplicate_DisplayOrder_And_Publish_Behavior()
        {
            var topicId = Guid.NewGuid();
            var page = Page.Create(topicId, "P", "p", "s", UserId, UtcNow);

            page.AddSection(PageSectionType.Text, "Sec1", "content", 1, UserId, UtcNow);
            Assert.Single(page.Sections);
            var section = page.Sections.First();
            Assert.Equal("Sec1", section.Title);
            Assert.Equal(1, section.DisplayOrder);
            Assert.Throws<InvalidOperationException>(() =>
                page.AddSection(PageSectionType.Text, "Sec2", "content", 1, UserId, UtcNow));

            var emptyPage = Page.Create(topicId, "Empty", "empty", "s", UserId, UtcNow);
            Assert.Throws<InvalidOperationException>(() => emptyPage.Publish(UserId, UtcNow));

            page.Publish(UserId, UtcNow);
            Assert.True(page.IsPublished);
            Assert.Equal(UserId, page.LastModifiedBy);
            Assert.Equal(UtcNow, page.LastUtcModified);
        }

        [Fact]
        public void Page_RemoveSection_And_UpdateSection_Throws_When_NotFound_And_SoftDeletes()
        {
            var topicId = Guid.NewGuid();
            var page = Page.Create(topicId, "P2", "p2", "s", UserId, UtcNow);

            page.AddSection(PageSectionType.Code, "SecA", "body", 1, UserId, UtcNow);
            var section = page.Sections.First();

            var later = UtcNow.AddMinutes(3);
            page.UpdateSection(section.Id, "UpdatedTitle", "UpdatedBody", 2, UserId, later);
            var updated = page.Sections.First(s => s.Id == section.Id);
            Assert.Equal("UpdatedTitle", updated.Title);
            Assert.Equal("UpdatedBody", updated.Content);
            Assert.Equal(2, updated.DisplayOrder);
            Assert.Equal(UserId, updated.LastModifiedBy);
            Assert.Equal(later, updated.LastUtcModified);

            var removedAt = UtcNow.AddMinutes(4);
            page.RemoveSection(section.Id, UserId, removedAt);
            var removed = page.Sections.First(s => s.Id == section.Id);
            Assert.False(removed.IsActive);
            Assert.Equal(UserId, removed.LastModifiedBy);
            Assert.Equal(removedAt, removed.LastUtcModified);
            Assert.Equal(UserId, page.LastModifiedBy);
            Assert.Equal(removedAt, page.LastUtcModified);

            var missing = Guid.NewGuid();
            Assert.Throws<DomainException>(() => page.RemoveSection(missing, UserId, UtcNow));
            Assert.Throws<DomainException>(() => page.UpdateSection(missing, "x", "y", 1, UserId, UtcNow));
        }

        [Fact]
        public void Page_SoftDeletePage_Works()
        {
            var topicId = Guid.NewGuid();
            var page = Page.Create(topicId, "Del", "del", "s", UserId, UtcNow);
            Assert.True(page.IsActive);

            var modifier = Guid.NewGuid();
            var later = UtcNow.AddMinutes(10);
            page.SoftDeletePage(modifier, later);

            Assert.False(page.IsActive);
            Assert.Equal(modifier, page.LastModifiedBy);
            Assert.Equal(later, page.LastUtcModified);
        }

        [Fact]
        public void PageSection_Update_And_SoftDelete_Works()
        {
            var pageId = Guid.NewGuid();
            var section = typeof(PageSection)
                .GetMethod("Create", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, new object[] { pageId, PageSectionType.Text, "T", "C", 1, UserId, UtcNow }) as PageSection;

            Assert.NotNull(section);
            var sec = section!;
            sec.Update("NewT", "NewC", 2, UserId, UtcNow);
            Assert.Equal("NewT", sec.Title);
            Assert.Equal("NewC", sec.Content);
            Assert.Equal(2, sec.DisplayOrder);
            Assert.Equal(UserId, sec.LastModifiedBy);
            Assert.Equal(UtcNow, sec.LastUtcModified);

            var later = UtcNow.AddMinutes(2);
            sec.SoftDeletePageSection(UserId, later);
            Assert.False(sec.IsActive);
            Assert.Equal(UserId, sec.LastModifiedBy);
            Assert.Equal(later, sec.LastUtcModified);
        }
    }
}