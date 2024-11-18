using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabeghe.Domain.ViewModels.CourseEpisode
{
	public class CourseEpisodeViewModel
	{
		public int Id { get; set; }

		public int CourseId { get; set; }

		public string EpisodeTitle { get; set; }

		public TimeSpan EpisodeTime { get; set; }

		public string EpisodeFileName { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
