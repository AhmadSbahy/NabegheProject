async function AddLike(id) {
	await fetch('/Course/AddLike', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify({ commentId: id, courseId: @Model.Id })
	})
		.then(response => response.json())
		.then(data => {
			if (data.success) {
				window.location.reload();
			} else {
				console.error("Failed to add like.");
			}
		})
		.catch(error => console.error('Error:', error));
}



function replyToComment(commentId) {
	document.getElementById('commentId').value = commentId; // تنظیم commentId
	document.getElementById('commentForm').classList.add('reply-mode');
	document.getElementById('commentText').style.backgroundColor = "#ffaf24";
	setTimeout(() => {
		document.getElementById('commentForm').classList.remove('reply-mode');
		document.getElementById('commentText').style.backgroundColor = '';
	}, 1000);// تغییر رنگ فرم برای حالت پاسخ
	document.getElementById('commentText').placeholder = 'پاسخ خود را وارد کنید ...';
}


document.getElementById('commentForm').addEventListener('submit', async function (e) {
	e.preventDefault();

	const courseId = document.getElementById('courseId').value;
	const commentId = document.getElementById('commentId').value;
	const commentText = document.getElementById('commentText').value.trim();

	if (!commentText) {
		Swal.fire({
			position: 'top-start',
			icon: 'warning',
			title: 'لطفاً یک متن وارد کنید.',
			showConfirmButton: false,
			timer: 1500
		});
		return;
	}

	try {
		const url = commentId ? '/Course/AddReply' : '/Course/AddComment'; // تعیین URL براساس حالت
		const data = commentId ? { commentId: parseInt(commentId), replyText: commentText } : { courseId: parseInt(courseId), commentText: commentText };

		const response = await fetch(url, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				'RequestVerificationToken': '@Html.AntiForgeryToken()'
			},
			body: JSON.stringify(data)
		});

		if (!response.ok) throw new Error('Server error');

		const result = await response.json();

		if (result.success) {
			Swal.fire({
				position: 'top-start',
				icon: 'success',
				title: 'نظر با موفقیت ثبت شد',
				showConfirmButton: false,
				timer: 1500
			}).then(() => {
				window.location.reload();
			});
			document.getElementById('commentText').value = '';
			document.getElementById('commentId').value = '';
			document.getElementById('commentForm').classList.remove('reply-mode');
			document.getElementById('commentText').placeholder = 'متن مورد نظر خود را وارد کنید ...';
		} else {
			Swal.fire({
				position: 'top-start',
				icon: 'error',
				title: 'خطا در ثبت نظر',
				showConfirmButton: false,
				timer: 1500
			});
		}
	} catch (error) {
		console.error('Error:', error);
		Swal.fire({
			position: 'top-start',
			icon: 'error',
			title: 'خطا در ارتباط با سرور',
			showConfirmButton: false,
			timer: 1500
		});
	}
});