namespace InterviewCoach.Application.Wrappers.ReadModels
{
    public sealed record TopicTreeItem(
     Guid Id,
     string Title,
     string Slug,
     IReadOnlyList<TopicTreeItem> Children,
     IReadOnlyList<PageLinkItem> Pages);

    public sealed record PageLinkItem(
        Guid Id,
        string Title,
        string Slug);
}
