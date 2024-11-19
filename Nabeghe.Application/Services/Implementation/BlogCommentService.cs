using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;

namespace Nabeghe.Application.Services.Implementation;

public class BlogCommentService : IBlogCommentService
{
    private readonly IBlogCommentRepository _commentRepository;
    private readonly IBlogCommentReplyRepository _replyRepository;
    private readonly IBlogCommentLikeRepository _likeRepository;

    public BlogCommentService(IBlogCommentRepository commentRepository,
        IBlogCommentReplyRepository replyRepository,
        IBlogCommentLikeRepository likeRepository)
    {
        _commentRepository = commentRepository;
        _replyRepository = replyRepository;
        _likeRepository = likeRepository;
    }

    public async Task<IEnumerable<BlogComment>> GetBlogCommentsAsync(int blogId)
    {
        return await _commentRepository.GetCommentsByBlogIdAsync(blogId);
    }

    public async Task AddCommentAsync(BlogComment comment)
    {
        await _commentRepository.AddCommentAsync(comment);
    }

    public async Task AddReplyToCommentAsync(BlogCommentReply reply)
    {
        await _replyRepository.AddReplyAsync(reply);
    }

    public async Task LikeCommentAsync(BlogCommentLike like)
    {
        await _likeRepository.AddLikeAsync(like);
    }

    public bool IsUserLikedBlogComment(int userId, int commentId)
    {
	    return _likeRepository.IsUserLikedBlogComment(userId, commentId);
	}

    public void DeleteBlogCommentLike(int userId, int commentId)
    {
	    _likeRepository.RemoveLikeAsync(userId, commentId);
    }
}