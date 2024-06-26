﻿namespace MultaqaTech.Core.Services.Contract.BlogPostContracts;

public interface IBlogPostCommentService
{
    Task<BlogPostComment?> CreateBlogPostCommentAsync(BlogPostComment blogPostComment);

    Task<IReadOnlyList<BlogPostComment>> ReadAllBlogPostCommentsAsync(BlogPostCommentSpeceficationsParams speceficationsParams);

    Task<BlogPostComment?> ReadByIdAsync(int blogPostCommentId);

    Task<BlogPostComment?> UpdateBlogPostComment(int blogPostCommentId, BlogPostComment updatedBlogPostComment);

    Task<bool> DeleteBlogPostComment(BlogPostComment blogPostComment);

    Task<int> GetCountAsync(BlogPostCommentSpeceficationsParams speceficationsParams);
}