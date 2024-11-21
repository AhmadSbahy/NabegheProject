using Microsoft.EntityFrameworkCore;
using Nabeghe.Application.Services.Interfaces;
using Nabeghe.Domain.Enums.Blog;
using Nabeghe.Domain.Enums.Course;
using Nabeghe.Domain.Interfaces;
using Nabeghe.Domain.Models.Blog;
using Nabeghe.Domain.ViewModels.BlogComment;
using Nabeghe.Infra.Data.Repositories;

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

    public async Task DeleteBlogCommentLike(int userId, int commentId)
    {
	   await  _likeRepository.RemoveLikeAsync(userId, commentId);
    }

    public async Task<FilterBlogCommentViewModel> FilterBlogCommentAsync(FilterBlogCommentViewModel model)
    {
	    return await _commentRepository.FilterBlogCommentAsync(model);
    }

    public async Task<(bool Success, string Message, int StatusCode)> ConfirmComment(int commentId)
    {
		var comment = await _commentRepository.GetCommentByIdAsync(commentId);

		if (comment == null)
			return (false, "کامنت مشخص شده پیدا نشد.", 101);

		comment.Status = BlogCommentStatus.Confirmed;
		await _commentRepository.UpdateCommentAsync(comment);

		return (true, "کامنت مدنظر شما تایید شد.", 100);
	}

    public async Task<(bool Success, string Message, int StatusCode)> RejectComment(int commentId)
    {
		var comment = await _commentRepository.GetCommentByIdAsync(commentId);

		if (comment == null)
			return (false, "کامنت مشخص شده پیدا نشد.", 101);

		comment.Status = BlogCommentStatus.Rejected;
		await _commentRepository.UpdateCommentAsync(comment);

		return (true, "کامنت مدنظر شما رد شد.", 100);
	}
}